using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using OnlineGame.DataAccess;
using OnlineGameStore.Common.Either;
using OnlineGameStore.Common.Errors;
using OnlineGameStore.Data.Dtos;
using OnlineGameStore.Data.Helpers;
using OnlineGameStore.Data.Services.Interfaces;
using OnlineGameStore.Domain.Entities;

namespace OnlineGameStore.Data.Services.Implementations
{
    public class PublisherService : IPublisherService
    {
        private readonly IRepository<Publisher> _repository;
        private readonly IValidatorStrategy<PublisherModel> _validator;

        public PublisherService(IRepository<Publisher> repositoryInstance, IValidatorStrategy<PublisherModel> validator)
        {
            _repository = repositoryInstance ??
                          throw new ArgumentNullException(nameof(repositoryInstance),
                              "personRepositoryInstance is null.");
            _validator = validator;
        }

        public void DeleteId(Guid id)
        {
            var entity = GetByIdAsync(id).GetAwaiter().GetResult();
            _repository.Delete(entity.ToEntity<Publisher>());
        }

        public async Task<PublisherModel> GetByIdAsync(Guid id) => (await _repository.GetAsync(x => x.Id == id))
            .FirstOrDefault().ToModel<PublisherModel>();

        public async Task<IEnumerable<PublisherModel>> GetAllAsync() => (await _repository.GetAllAsync()).ToModel<PublisherModel>();

        public async Task<Either<Error, PublisherModel>> SaveSafe(PublisherModel obj)
        {
            var model = Mapper.Map<PublisherModel>(obj);
            return !_validator.IsValid(model)
                ? new UnprocessableError()
                : (await _repository.SaveAsync(model.ToEntity<Publisher>())).Map(e => e.ToModel<PublisherModel>());
        }
    }
}