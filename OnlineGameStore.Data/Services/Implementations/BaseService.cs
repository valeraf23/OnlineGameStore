using System;
using System.Threading.Tasks;
using AutoMapper;
using OnlineGame.DataAccess.Interfaces;
using OnlineGameStore.Common.Either;
using OnlineGameStore.Common.Errors;
using OnlineGameStore.Data.Dtos;
using OnlineGameStore.Data.Helpers;

namespace OnlineGameStore.Data.Services.Implementations
{
    public abstract class BaseService<TModel, TEntity> where TModel : IModel where TEntity : IGuidIdentity
    {
        protected readonly IRepository<TEntity> Repository;
        private readonly IValidatorStrategy<TModel> _validator;

        protected BaseService(IRepository<TEntity> repositoryInstance, IValidatorStrategy<TModel> validator)
        {
            Repository = repositoryInstance ??
                         throw new ArgumentNullException(nameof(repositoryInstance),
                             "personRepositoryInstance is null.");
            _validator = validator;
        }

        public virtual async Task<Either<Error, TModel>> SaveSafe(TModel gameModel) =>
            await _validator.GetResults(gameModel)
                .Map<Task<Either<Error, TModel>>>(async validationResult =>
                    await validationResult.GetUnprocessableError())
                .Reduce(async () => await Save(gameModel));

        public virtual async Task<Either<Error, TModel>> UpdateSafe(Guid id, TModel gameModel) =>
            await _validator.GetResults(gameModel)
                .Map<Task<Either<Error, TModel>>>(async validationResult =>
                    await validationResult.GetUnprocessableError())
                .Reduce(async () => await Update(id, gameModel));

        private async Task<Either<Error, TModel>> Save(TModel gameModel) =>
            (await Repository.SaveAsync(gameModel.ToEntity<TEntity>()))
            .Map(e => e.ToModel<TModel>());

        private async Task<Either<Error, TModel>> Update(Guid id, TModel gameModel)
        {
            var existEntity = Repository.GetAsync(id).GetAwaiter().GetResult();
            if (existEntity == null) return new NotFoundError(id);
            Mapper.Map(gameModel, existEntity);
            existEntity.Id = id;
            return (await Repository.SaveAsync(existEntity))
                .Map(e => e.ToModel<TModel>());
        }
    }
}