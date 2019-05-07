using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineGameStore.Common.Either;
using OnlineGameStore.Common.Errors;

namespace OnlineGame.DataAccess
{
    public abstract class OnlineGameRepositoryBase<TEntity, TDbContext> :
        RepositoryBase<TEntity, TDbContext>, IRepository<TEntity>
        where TEntity : class, IGuidIdentity
        where TDbContext : DbContext
    {
        protected OnlineGameRepositoryBase(
            TDbContext context) : base(context)
        {
        }

        protected OnlineGameRepositoryBase(
            TDbContext context, CancellationTokenSource cancellationTokenSource) : base(context,
            cancellationTokenSource)
        {
        }

        protected abstract DbSet<TEntity> EntityDbSet { get; }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync() => await EntityDbSet.ToListAsync();

        public virtual void Delete(TEntity deleteThis)
        {
            if (deleteThis == null)
                throw new ArgumentNullException(nameof(deleteThis), "deleteThis is null.");

            var entry = Context.Entry(deleteThis);

            if (entry.State == EntityState.Detached) EntityDbSet.Attach(deleteThis);

            EntityDbSet.Remove(deleteThis);

            Context.SaveChanges();
        }

        public virtual async Task<TEntity> GetAsync(Guid gameId) =>
            (await GetAsync(x => x.Id == gameId)).FirstOrDefault();

        public virtual async Task<IEnumerable<TEntity>> GetAsync(Func<TEntity, bool> predicate) =>
            await EntityDbSet.Where(e => predicate(e)).ToListAsync();

        public async Task<Either<Error, TEntity>> SaveAsync(TEntity saveThis)
        {
            if (saveThis == null)
                return new ArgumentNullError("saveThis is null.");

            AddToDbSet(EntityDbSet, saveThis);
            if (await SaveChangesAsync())
                return saveThis;
            return new SaveError();
        }
    }
}