using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OnlineGameStore.Api.Filters;
using OnlineGameStore.Common.Either;
using OnlineGameStore.Common.Errors;
using OnlineGameStore.Common.Optional.Extensions;
using OnlineGameStore.Data.Dtos;
using OnlineGameStore.Data.Services;

namespace OnlineGameStore.Api.Controllers
{
    [Route("api/games")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;
        private readonly IMapper _mapper;

        public GameController(IGameService gameService, IMapper mapper)
        {
            _gameService = gameService
                           ?? throw new ArgumentNullException(nameof(gameService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<IActionResult> GetGames() => Ok(await _gameService.GetGamesAsync());

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetGame(Guid id) =>
            (await _gameService.GetGameByIdAsync(id)).NoneIfNull()
            .Map<IActionResult>(Ok).Reduce(NotFound);

        [HttpPost]
        [AssignPublisherId]
        public async Task<IActionResult> CreateGame([FromBody] GameForCreationModel game) =>
            (await _gameService.SaveSafe(game))
            .Map(created => (IActionResult) Ok(created))
            .Reduce(_ => UnprocessableEntityError(ModelState), error => error is UnprocessableError)
            .Reduce(_ => UnprocessableEntityError(ModelState), error => error is SaveError)
            .Reduce(_ => InternalServerError());

        private IActionResult InternalServerError() =>
            StatusCode(StatusCodes.Status500InternalServerError);

        private IActionResult UnprocessableEntityError(ModelStateDictionary modelState) =>
            new Helpers.UnprocessableEntityObjectResult(modelState);
    }
}
