using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.Common.Either;
using OnlineGameStore.Common.Errors;

namespace OnlineGame.DataAccess.Interfaces
{
    public interface IRepository<T> where T : IGuidIdentity
    {
        Task<T> GetAsync(Guid gameId);
        Task<IEnumerable<T>> GetAsync(Func<T, bool> predicate);
        Task<IEnumerable<T>> GetAllAsync();
        void Delete(T entity);
        Task<Either<Error,T>> SaveAsync(T entity);
    }
}