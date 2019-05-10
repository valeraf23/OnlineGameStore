using System;
using System.Threading.Tasks;
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
                    await validationResult.GetUnprocessableError()).Reduce(
                    async () => await Save(gameModel));

        private async Task<Either<Error, TModel>> Save(TModel gameModel) =>
            (await Repository.SaveAsync(gameModel.ToEntity<TEntity>())).Map(e =>
                e.ToModel<TModel>());
    }
}