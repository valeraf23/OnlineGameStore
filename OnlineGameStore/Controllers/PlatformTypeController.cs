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
    [Route("api/platformType")]
    [ApiController]
    [Authorize]
    public class PlatformTypeController : ControllerBase
    {
        private readonly IPlatformTypeService _platformTypeRepository;

        public PlatformTypeController(IPlatformTypeService platformTypeRepository)
        {
            _platformTypeRepository = platformTypeRepository
                                      ?? throw new ArgumentNullException(nameof(platformTypeRepository));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _platformTypeRepository.GetAllAsync());

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetPlatformType(Guid id) =>
            (await _platformTypeRepository.GetByIdAsync(id)).NoneIfNull()
            .Map<IActionResult>(Ok).Reduce(NotFound);

        [HttpPost]
        public async Task<IActionResult> AddPlatformType(PlatformTypeModel publisher) =>
            (await _platformTypeRepository.SaveSafe(publisher))
            .Map(created => (IActionResult) Ok(created))
            .Reduce(_ => BadRequest(), error => error is ArgumentNullError)
            .Reduce(error => error.ToObjectResult(), error => error != null)
            .Reduce(_ => ModelState.ToObjectResult());
    }
}
