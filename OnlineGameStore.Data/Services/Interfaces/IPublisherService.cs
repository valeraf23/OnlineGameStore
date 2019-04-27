using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.Common.Either;
using OnlineGameStore.Common.Errors;
using OnlineGameStore.Data.Dtos;

namespace OnlineGameStore.Data.Services.Interfaces
{
    public interface IPublisherService
    {
        void DeleteId(Guid id);
        Task<PublisherModel> GetByIdAsync(Guid id);
        Task<IEnumerable<PublisherModel>> GetAllAsync();
        Task<Either<Error, PublisherModel>> SaveSafe(PublisherModel obj);
    }
}