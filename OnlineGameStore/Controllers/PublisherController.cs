using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OnlineGame.DataAccess.Interfaces;
using OnlineGameStore.Common.Either;
using OnlineGameStore.Common.Optional.Extensions;
using OnlineGameStore.Data.Dtos;
using OnlineGameStore.Domain.Entities;

namespace OnlineGameStore.Api.Controllers
{
    [Route("api/publishers")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly IRepository<Publisher> _publisherRepository;

        public PublisherController(IRepository<Publisher> publisherRepository, IMapper mapper)
        {
            _publisherRepository = publisherRepository
                                   ?? throw new ArgumentNullException(nameof(publisherRepository));
        }

        [HttpGet]
        public async Task<IActionResult> GetPublishers() => Ok(await _publisherRepository.GetAllAsync());

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetPublisher(Guid id) =>
            (await _publisherRepository.GetAsync(id)).NoneIfNull()
            .Map<IActionResult>(Ok).Reduce(NotFound);

        [HttpPost]
        public async Task<IActionResult> AddPublisher(PublisherForCreateModel publisher)
        {
           return (await _publisherRepository.SaveAsync(Mapper.Map<Publisher>(publisher))).When(true)
                .Map(created => (IActionResult)Ok(created))
                .Reduce(UnprocessableEntityError(ModelState));
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePublisher(Guid id, [FromBody] PublisherForCreateModel publisher)
        {
            var existPublisher = _publisherRepository.GetAsync(id).GetAwaiter().GetResult();
            if (existPublisher == null)
            {
                return NotFound();
            }

            Mapper.Map(publisher, existPublisher);
            return _publisherRepository.SaveAsync(existPublisher).GetAwaiter().GetResult()
                .Map(x => (IActionResult) NoContent())
                .Reduce(_ => StatusCode(StatusCodes.Status500InternalServerError));
        }


        [HttpPatch("{id}")]
        public IActionResult PartiallyUpdatePublisher(Guid id,
            [FromBody] JsonPatchDocument<PublisherForCreateModel> publisher)
        {
            var existPublisher = _publisherRepository.GetAsync(id).GetAwaiter().GetResult();
            if (existPublisher == null)
            {
                return NotFound();
            }

            var publisherPatch = Mapper.Map<PublisherForCreateModel>(existPublisher);
            publisher.ApplyTo(publisherPatch);
            Mapper.Map(publisherPatch, existPublisher);
            return _publisherRepository.SaveAsync(existPublisher).GetAwaiter().GetResult()
                .Map(x => (IActionResult) NoContent())
                .Reduce(_ => StatusCode(StatusCodes.Status500InternalServerError));
        }

        private IActionResult UnprocessableEntityError(ModelStateDictionary modelState) =>
            new UnprocessableEntityObjectResult(modelState);
    }
}
