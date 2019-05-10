using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using OnlineGameStore.Api.Helpers;
using OnlineGameStore.Common.Either;
using OnlineGameStore.Common.Errors;
using OnlineGameStore.Common.Optional.Extensions;
using OnlineGameStore.Data.Dtos;
using OnlineGameStore.Data.Services.Interfaces;

namespace OnlineGameStore.Api.Controllers
{
    [Route("api/publishers")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly IPublisherService _publisherRepository;

        public PublisherController(IPublisherService publisherRepository)
        {
            _publisherRepository = publisherRepository
                                   ?? throw new ArgumentNullException(nameof(publisherRepository));
        }

        [HttpGet]
        public async Task<IActionResult> GetPublishers() => Ok(await _publisherRepository.GetAllAsync());

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetPublisher(Guid id) =>
            (await _publisherRepository.GetByIdAsync(id)).NoneIfNull()
            .Map<IActionResult>(Ok).Reduce(NotFound);

        [HttpPost]
        public async Task<IActionResult> AddPublisher(PublisherForCreateModel publisher) =>
            (await _publisherRepository.SaveSafe(Mapper.Map<PublisherModel>(publisher)))
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
    }
}
