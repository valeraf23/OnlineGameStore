using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineGame.DataAccess.Interfaces;
using OnlineGameStore.Data.Dtos;
using OnlineGameStore.Data.Helpers;
using OnlineGameStore.Data.Services.Interfaces;
using OnlineGameStore.Domain.Entities;

namespace OnlineGameStore.Data.Services.Implementations
{
    public class PublisherService : BaseService<PublisherModel, Publisher>, IPublisherService
    {
        public PublisherService(IRepository<Publisher> repositoryInstance,
            IValidatorStrategy<PublisherModel> validator) :
            base(repositoryInstance, validator)
        {

        }

        public void DeleteId(Guid id)
        {
            var entity = GetByIdAsync(id).GetAwaiter().GetResult();
            Repository.Delete(entity.ToEntity<Publisher>());
        }

        public async Task<PublisherModel> GetByIdAsync(Guid id) => (await Repository.GetAsync(x => x.Id == id))
            .FirstOrDefault().ToModel<PublisherModel>();

        public async Task<IEnumerable<PublisherModel>> GetAllAsync() =>
            (await Repository.GetAllAsync()).ToModel<PublisherModel>();

    }
}