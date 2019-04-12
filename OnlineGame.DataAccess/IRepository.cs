using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineGame.DataAccess
{
    public interface IRepository<T> where T : IGuidIdentity
    {
        Task<T> GetAsync(Guid gameId);
        Task<IEnumerable<T>> GetAsync(Func<T, bool> predicate);
        Task<IEnumerable<T>> GetAllAsync();
        void Delete(T entity);
        Task<bool> SaveAsync(T entity);
        Task<bool> SaveChangesAsync();
    }
}