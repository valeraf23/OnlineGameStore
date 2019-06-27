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
    public class GenreService : BaseService<GenreModel, Genre>, IGenreService
    {
        public GenreService(IRepository<Genre> repositoryInstance,
            IValidatorStrategy<GenreModel> validator) :
            base(repositoryInstance, validator)
        {

        }

        public async Task<Either<Error, GenreModel>> GetByIdAsync(Guid id) =>
            (await Repository.GetAsync(x => x.Id == id))
            .FirstOrDefault().ToModel<GenreModel>();

        public async Task<IEnumerable<GenreModel>> GetAllAsync() =>
            (await Repository.GetAllAsync()).ToModel<GenreModel>();

    }
}