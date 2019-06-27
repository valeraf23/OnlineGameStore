using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.Common.Either;
using OnlineGameStore.Common.Errors;
using OnlineGameStore.Data.Dtos;

namespace OnlineGameStore.Data.Services.Interfaces
{
    public interface IPlatformTypeService
    {
        Task<IEnumerable<PlatformTypeModel>> GetAllAsync();

        Task<Either<Error, PlatformTypeModel>> GetByIdAsync(Guid id);
        Task<Either<Error, PlatformTypeModel>> SaveSafe(PlatformTypeModel obj);
    }
}