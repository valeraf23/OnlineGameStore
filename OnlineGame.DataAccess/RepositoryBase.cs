using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineGame.DataAccess.Interfaces;

namespace OnlineGame.DataAccess
{
    public abstract class RepositoryBase<TEntity, TDbContext> :
        IDisposable where TEntity : class, IGuidIdentity
        where TDbContext : DbContext
    {
        private CancellationTokenSource _cancellationTokenSource;

        protected RepositoryBase(
            TDbContext context) =>
            Context = context ?? throw new ArgumentNullException(nameof(context), "context is null.");

        protected RepositoryBase(
            TDbContext context, CancellationTokenSource cancellationTokenSource) : this(context) =>
            _cancellationTokenSource = cancellationTokenSource;

        protected TDbContext Context { get; private set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (Context != null)
                {


                    Context.Dispose();
                    Context = null;
                }

                if (_cancellationTokenSource != null)
                {
                    _cancellationTokenSource.Dispose();
                    _cancellationTokenSource = null;
                }
            }
        }

        protected void AddToDbSet(DbSet<TEntity> dbSet, TEntity item)
        {
            if (item == null) return;
            if (Context.Entry(item).State == EntityState.Modified)
            {
                dbSet.Update(item);
            }
            else
            {
                if (item.Id == Guid.Empty)
                {
                    item.Id = Guid.NewGuid();
                }

                dbSet.Add(item);
            }
        }

        public async Task<bool> SaveChangesAsync()
        {
            try
            {
                return await Context.SaveChangesAsync() > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
          
        }
    }
}