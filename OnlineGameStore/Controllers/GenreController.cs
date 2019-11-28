using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineGameStore.Api.Helpers;
using OnlineGameStore.Common.Either;
using OnlineGameStore.Common.Errors;
using OnlineGameStore.Common.Optional.Extensions;
using OnlineGameStore.Data.Dtos;
using OnlineGameStore.Data.Services.Interfaces;

namespace OnlineGameStore.Api.Controllers
{
    [Route("api/genres")]
    [ApiController]
    [Authorize]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService _genreRepository;

        public GenreController(IGenreService platformTypeRepository)
        {
            _genreRepository = platformTypeRepository
                               ?? throw new ArgumentNullException(nameof(platformTypeRepository));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _genreRepository.GetAllAsync());

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetGenre(Guid id) =>
            (await _genreRepository.GetByIdAsync(id)).NoneIfNull()
            .Map<IActionResult>(Ok).Reduce(NotFound);

        [HttpPost]
        public async Task<IActionResult> AddGenre(GenreModel publisher) =>
            (await _genreRepository.SaveSafe(publisher))
            .OnSuccess(created => (IActionResult)Ok(created))
            .OnFailure(_ => BadRequest(), error => error is ArgumentNullError)
            .OnFailure(error => error.ToObjectResult(), error => error != null)
            .OnFailure(_ => ModelState.ToObjectResult());
    }
}