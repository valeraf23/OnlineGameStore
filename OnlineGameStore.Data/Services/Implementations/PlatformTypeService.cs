using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineGame.DataAccess.Interfaces;
using OnlineGameStore.Common.Either;
using OnlineGameStore.Common.Errors;
using OnlineGameStore.Data.Dtos;
using OnlineGameStore.Data.Helpers;
using OnlineGameStore.Data.Services.Interfaces;
using OnlineGameStore.Domain.Entities;

namespace OnlineGameStore.Data.Services.Implementations
{
    public class PlatformTypeService : BaseService<PlatformTypeModel, PlatformType>, IPlatformTypeService
    {
        public PlatformTypeService(IRepository<PlatformType> repositoryInstance,
            IValidatorStrategy<PlatformTypeModel> validator) :
            base(repositoryInstance, validator)
        {

        }

        public async Task<Either<Error, PlatformTypeModel>> GetByIdAsync(Guid id) =>
            (await Repository.GetAsync(x => x.Id == id))
            .FirstOrDefault().ToModel<PlatformTypeModel>();

        public async Task<IEnumerable<PlatformTypeModel>> GetAllAsync() =>
            (await Repository.GetAllAsync()).ToModel<PlatformTypeModel>();

    }
}