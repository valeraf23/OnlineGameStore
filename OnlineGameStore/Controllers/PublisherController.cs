using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OnlineGame.DataAccess;
using OnlineGameStore.Api.Filters;
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
        private readonly IMapper _mapper;

        public PublisherController(IRepository<Publisher> publisherRepository, IMapper mapper)
        {
            _publisherRepository = publisherRepository
                                   ?? throw new ArgumentNullException(nameof(publisherRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<IActionResult> GetPublishers() => Ok(await _publisherRepository.GetAllAsync());

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetPublisher(Guid id) =>
            (await _publisherRepository.GetAsync(id)).NoneIfNull()
            .Map<IActionResult>(Ok).Reduce(NotFound);

        [HttpPost]
        [AssignPublisherId]
        public async Task<IActionResult> AddPublisher([FromBody] PublisherForCreateModel publisher) =>
            (await _publisherRepository.SaveAsync(_mapper.Map<Publisher>(publisher))).When(true)
            .Map(created => (IActionResult) Ok(created))
            .Reduce(UnprocessableEntityError(ModelState));
     
        private IActionResult UnprocessableEntityError(ModelStateDictionary modelState) =>
            new UnprocessableEntityObjectResult(modelState);
    }
}
