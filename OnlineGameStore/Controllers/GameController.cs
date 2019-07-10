using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using OnlineGameStore.Api.Filters;
using OnlineGameStore.Api.Helpers;
using OnlineGameStore.Common.Either;
using OnlineGameStore.Common.Errors;
using OnlineGameStore.Common.Optional;
using OnlineGameStore.Common.Optional.Extensions;
using OnlineGameStore.Data.Dtos;
using OnlineGameStore.Data.Helpers;
using OnlineGameStore.Data.Services.Interfaces;

namespace OnlineGameStore.Api.Controllers
{
    [Route("api/games")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IGameService _gameService;
        private readonly GameControllerHelper _gameControllerHelper;
        private readonly ITypeHelperService _typeHelperService;

        public GameController(IGameService gameService, ICommentService commentService,
            ITypeHelperService typeHelperService, LinkGenerator linkGenerator)
        {
            _gameService = gameService
                           ?? throw new ArgumentNullException(nameof(gameService));
            _commentService = commentService
                              ?? throw new ArgumentNullException(nameof(commentService));
            _typeHelperService = typeHelperService
                                 ?? throw new ArgumentNullException(nameof(typeHelperService));
            _gameControllerHelper = new GameControllerHelper(linkGenerator);
        }

        [HttpGet(Name = "GetGames")]
        public async Task<IActionResult> GetGames([FromQuery] GameResourceParameters gameResourceParameters)
        {
            if (!_typeHelperService.TypeHasProperties<GameModel>
                (gameResourceParameters.Fields))
                return BadRequest();

            var filteredModels = await GetGameModels(gameResourceParameters);
            var pages = PagedList<GameModel>.Create(filteredModels.ToList(), gameResourceParameters.PageNumber,
                gameResourceParameters.PageSize);
            var paginationMetadata =
                _gameControllerHelper.GetPaginationMetadata(pages, gameResourceParameters, HttpContext);
            Response.Headers.Add("X-Pagination", paginationMetadata);

            return Ok(pages.ShapeData(gameResourceParameters.Fields));
        }

        [HttpGet]
        [Route("{id}", Name = "GetGame")]
        public async Task<IActionResult> GetGame(Guid id) =>
            (await GetGameById(id))
            .Map<IActionResult>(Ok).Reduce(NotFound);

        [HttpPost]
        [AssignPublisherId]
        public async Task<IActionResult> CreateGame(GameForCreationModel game) =>
            (await _gameService.SaveSafe(Mapper.Map<GameModel>(game)))
            .Map(GetRoute)
            .Reduce(_ => BadRequest(), error => error is ArgumentNullError)
            .Reduce(error => error.ToObjectResult(), error => error != null)
            .Reduce(_ => ModelState.ToObjectResult());

        [HttpPut("{id}")]
        [AssignPublisherId]
        public IActionResult UpdateGame(Guid id, GameForCreationModel game) =>
            _gameService.UpdateSafe(id, Mapper.Map<GameModel>(game)).GetAwaiter().GetResult()
                .Map(x => (IActionResult) NoContent())
                .Reduce(_ => BadRequest(), error => error is ArgumentNullError)
                .Reduce(error => error.ToObjectResult(), error => error != null)
                .Reduce(_ => ModelState.ToObjectResult());

        [HttpPatch("{id}")]
        public IActionResult PartiallyUpdatePublisher(Guid id, JsonPatchDocument<PublisherForCreateModel> publisher)
        {
            var existPublisher = _gameService.GetGameByIdAsync(id).GetAwaiter().GetResult();
            if (existPublisher == null) return NotFound();

            var publisherPatch = Mapper.Map<PublisherForCreateModel>(existPublisher);
            publisher.ApplyTo(publisherPatch);
            return _gameService.UpdateSafe(id, Mapper.Map<GameModel>(publisherPatch)).GetAwaiter().GetResult()
                .Map(x => (IActionResult) NoContent())
                .Reduce(_ => BadRequest(), error => error is ArgumentNullError)
                .Reduce(error => error.ToObjectResult(), error => error != null)
                .Reduce(_ => ModelState.ToObjectResult());
        }

        [HttpPost("{id}/comments")]
        public async Task<IActionResult> AddCommentToGame(Guid id, [FromBody] CommentModel model) =>
            (await _commentService.AddCommentToGame(id, model))
            .Map(GetRoute)
            .Reduce(_ => BadRequest(), error => error is ArgumentNullError)
            .Reduce(error => error.ToObjectResult(), error => error != null)
            .Reduce(_ => ModelState.ToObjectResult());

        [HttpPost("{id}/comments/{commentId}")]
        public async Task<IActionResult> AddAnswerToComment(Guid id, Guid commentId, [FromBody] CommentModel model) =>
            (await _commentService.AddAnswerToComment(id, commentId, model))
            .Map(x => (IActionResult) Ok(x))
            .Reduce(_ => BadRequest(), error => error is ArgumentNullError)
            .Reduce(error => error.ToObjectResult(), error => error != null)
            .Reduce(_ => ModelState.ToObjectResult());

        [HttpGet]
        [Route("{id}/download")]
        public async Task<IActionResult> DownloadGame(Guid id)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "StaticFiles", "e.PDF");

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }

            memory.Position = 0;
            return File(memory, GetContentType(path), Path.GetFileName(path));
        }

        [HttpGet("{id}/comments")]
        public async Task<IActionResult> GetComments(Guid id, [FromQuery] string searchQuery)
        {
            Func<CommentModel, bool> func = c => true;
            if (!string.IsNullOrEmpty(searchQuery))
            {
                func = c => IsCommentContainsSearchQuery(c, searchQuery);
            }

            var result = await _commentService.GetCommentsForGame(id, func);
            return Ok(result);
        }

        private async Task<Option<GameModel>> GetGameById(Guid id) =>
            (await _gameService.GetGameByIdAsync(id)).NoneIfNull()
            .Map(g =>
            {
                g.Comments = _commentService.GetAllCommentsForGame(id).GetAwaiter().GetResult().ToArray();
                return g;
            });

        private IActionResult GetRoute(IModel model) => CreatedAtRoute("GetGame", new {model.Id}, null);

        private async Task<IList<GameModel>> GetGameModels(GameResourceParameters gameResourceParameters)
        {
            var games = await _gameService.GetGamesAsync();
            var gamesWithComments = games.Select(g => GetGameById(g.Id)).ToList();
            var results = await Task.WhenAll(gamesWithComments);
            var gameModels = results.SelectOptional(option => option).AsQueryable()
                .ApplySort(gameResourceParameters.OrderBy).ToList();
            return _gameControllerHelper.ApplyFilters(gameModels, gameResourceParameters).ToList();
        }

        private static Dictionary<string, string> GetMimeTypes() =>
            new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},

            };

        private static string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private static bool IsCommentContainsSearchQuery(CommentModel model, string query)
        {
            if (model.Body.Contains(query))
            {
                return true;
            }

            return model.Answers.Any() && model.Answers.Any(a => IsCommentContainsSearchQuery(a, query));
        }
    }
}