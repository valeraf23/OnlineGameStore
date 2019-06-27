using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.Common.Either;
using OnlineGameStore.Common.Errors;
using OnlineGameStore.Data.Dtos;

namespace OnlineGameStore.Data.Services.Interfaces
{
    public interface IGenreService
    {
        Task<IEnumerable<GenreModel>> GetAllAsync();

        Task<Either<Error, GenreModel>> GetByIdAsync(Guid id);
        Task<Either<Error, GenreModel>> SaveSafe(GenreModel obj);
    }
}