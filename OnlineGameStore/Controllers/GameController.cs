using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OnlineGameStore.Api.Filters;
using OnlineGameStore.Common.Either;
using OnlineGameStore.Common.Errors;
using OnlineGameStore.Common.Optional.Extensions;
using OnlineGameStore.Data.Dtos;
using OnlineGameStore.Data.Services.Interfaces;

namespace OnlineGameStore.Api.Controllers
{
    [Route("api/games")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;

        public GameController(IGameService gameService, ICommentService commentService, IMapper mapper)
        {
            _gameService = gameService
                           ?? throw new ArgumentNullException(nameof(gameService));
            _commentService = commentService
                              ?? throw new ArgumentNullException(nameof(commentService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<IActionResult> GetGames()
        {
            var games = await _gameService.GetGamesAsync();
            var gamesWithComments = games.Select(g => GetGame(g.Id)).ToList();
            var results = await Task.WhenAll(gamesWithComments);
            return Ok(results);
        }

        [HttpGet]
        [Route("{id}", Name = "GetGame")]
        public async Task<IActionResult> GetGame(Guid id)
        {
            var comments = await _commentService.GetAllCommentsForGame(id);
            return (await _gameService.GetGameByIdAsync(id)).NoneIfNull()
                .Map<IActionResult>(g =>
                {
                    g.Comments = comments.ToArray();
                    return Ok(g);
                }).Reduce(NotFound);
        }

        [HttpPost]
        [AssignPublisherId]
        public async Task<IActionResult> CreateGame(GameForCreationModel game) =>
            (await _gameService.SaveSafe(Mapper.Map<GameModel>(game)))
            .Map(created => GetRoute(created))
            .Reduce(_ => UnprocessableEntityError(ModelState), error => error is UnprocessableError)
            .Reduce(_ => UnprocessableEntityError(ModelState), error => error is SaveError)
            .Reduce(_ => BadRequest(), error => error is ArgumentNullError)
            .Reduce(_ => InternalServerError());

        [HttpPut("{id}")]
        public IActionResult UpdateGame(Guid id, GameForCreationModel game)
        {
            var existGame = _gameService.GetGameByIdAsync(id).GetAwaiter().GetResult();
            if (existGame == null)
            {
                return NotFound();
            }

            Mapper.Map(game, existGame);
            return _gameService.SaveSafe(existGame).GetAwaiter().GetResult()
                .Map(x => (IActionResult) NoContent())
                .Reduce(_ => UnprocessableEntityError(ModelState), error => error is UnprocessableError)
                .Reduce(_ => UnprocessableEntityError(ModelState), error => error is SaveError)
                .Reduce(_ => BadRequest(), error => error is ArgumentNullError)
                .Reduce(_ => InternalServerError());
        }

        [HttpPatch("{id}")]
        public IActionResult PartiallyUpdatePublisher(Guid id, JsonPatchDocument<PublisherForCreateModel> publisher)
        {
            var existPublisher = _gameService.GetGameByIdAsync(id).GetAwaiter().GetResult();
            if (existPublisher == null)
            {
                return NotFound();
            }

            var publisherPatch = Mapper.Map<PublisherForCreateModel>(existPublisher);
            publisher.ApplyTo(publisherPatch);
            Mapper.Map(publisherPatch, existPublisher);
            return _gameService.SaveSafe(existPublisher).GetAwaiter().GetResult()
                .Map(x => (IActionResult) NoContent())
                .Reduce(_ => UnprocessableEntityError(ModelState), error => error is UnprocessableError)
                .Reduce(_ => UnprocessableEntityError(ModelState), error => error is SaveError)
                .Reduce(_ => BadRequest(), error => error is ArgumentNullError)
                .Reduce(_ => InternalServerError());
        }

        private IActionResult InternalServerError() =>
            StatusCode(StatusCodes.Status500InternalServerError);

        private IActionResult UnprocessableEntityError(ModelStateDictionary modelState) =>
            new Helpers.UnprocessableEntityObjectResult(modelState);

        private IActionResult GetRoute(IModel model) => CreatedAtRoute("GetGame", new {model.Id}, null);
    }
}
