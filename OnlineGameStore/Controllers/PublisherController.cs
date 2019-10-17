using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using OnlineGameStore.Api.Helpers;
using OnlineGameStore.Api.Services;
using OnlineGameStore.Common.Either;
using OnlineGameStore.Common.Errors;
using OnlineGameStore.Common.Optional.Extensions;
using OnlineGameStore.Data.Dtos;
using OnlineGameStore.Data.Services.Interfaces;

namespace OnlineGameStore.Api.Controllers
{
    [Route("api/publishers")]
    [ApiController]
    [Authorize]
    public class PublisherController : ControllerBase
    {
        private readonly IPublisherService _publisherRepository;
        private readonly IUserInfoService _userInfoService;

        public PublisherController(IPublisherService publisherRepository, IUserInfoService userInfoService)
        {
            _publisherRepository = publisherRepository
                                   ?? throw new ArgumentNullException(nameof(publisherRepository));
            _userInfoService = userInfoService
                               ?? throw new ArgumentNullException(nameof(userInfoService));
        }

        [HttpGet]
        public async Task<IActionResult> GetPublishers() => Ok(await _publisherRepository.GetAllAsync());

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetPublisher(Guid id) =>
            (await _publisherRepository.GetByIdAsync(id)).NoneIfNull()
            .Map<IActionResult>(Ok).Reduce(NotFound);

        [HttpGet]
        [Route("available/{id}")]
        public async Task<IActionResult> IsAvailable(Guid id) =>
            (await _publisherRepository.GetByIdAsync(id)).NoneIfNull()
            .Map<IActionResult>(m=>Ok(true))
            .Reduce(Ok(false));

        [HttpPost("create")]
        public async Task<IActionResult> AddPublisher(PublisherModel publisher) =>
            (await _publisherRepository.SaveSafe(publisher))
            .Map(created => (IActionResult) Ok(created))
            .Reduce(_ => BadRequest(), error => error is ArgumentNullError)
            .Reduce(error => error.ToObjectResult(), error => error != null)
            .Reduce(_ => ModelState.ToObjectResult());

        [HttpPut("{id}")]
        public IActionResult UpdatePublisher(Guid id, [FromBody] PublisherForCreateModel publisher)
        {
            var existPublisher = _publisherRepository.GetByIdAsync(id).GetAwaiter().GetResult();
            if (existPublisher == null)
            {
                return NotFound();
            }

            Mapper.Map(publisher, existPublisher);
            return _publisherRepository.SaveSafe(existPublisher).GetAwaiter().GetResult()
                .Map(x => (IActionResult) NoContent())
                .Reduce(_ => BadRequest(), error => error is ArgumentNullError)
                .Reduce(error => error.ToObjectResult(), error => error != null)
                .Reduce(_ => ModelState.ToObjectResult());
        }

        [HttpPatch("{id}")]
        public IActionResult PartiallyUpdatePublisher(Guid id,
            [FromBody] JsonPatchDocument<PublisherForCreateModel> publisher)
        {
            var existPublisher = _publisherRepository.GetByIdAsync(id).GetAwaiter().GetResult();
            if (existPublisher == null)
            {
                return NotFound();
            }

            var publisherPatch = Mapper.Map<PublisherForCreateModel>(existPublisher);
            publisher.ApplyTo(publisherPatch);
            Mapper.Map(publisherPatch, existPublisher);
            return _publisherRepository.SaveSafe(existPublisher).GetAwaiter().GetResult()
                .Map(x => (IActionResult) NoContent())
                .Reduce(_ => BadRequest(), error => error is ArgumentNullError)
                .Reduce(error => error.ToObjectResult(), error => error != null)
                .Reduce(_ => ModelState.ToObjectResult());
        }

        [HttpGet("AuthContext")]
        public async Task<IActionResult> GetAuthContext()
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(id, out var userId)) return Forbid();
            var profile = await _publisherRepository.GetByIdAsync(userId) ?? new PublisherModel
            {
                Id = userId,
                Name = _userInfoService.Name
            };

            var context = new AuthContext
            {
                UserProfile = profile,
                Claims = User.Claims.Select(c => new SimpleClaim {Type = c.Type, Value = c.Value}).ToList()
            };
            return Ok(context);

        }
    }
}
