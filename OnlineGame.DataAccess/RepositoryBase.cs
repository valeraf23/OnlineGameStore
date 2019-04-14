﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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

        protected void VerifyItemIsAddedOrAttachedToDbSet(DbSet<TEntity> dbSet, TEntity item)
        {
            if (item == null) return;
            if (item.Id == Guid.Empty)
            {
                item.Id = Guid.NewGuid();
                dbSet.Add(item);
            }
            else
            {
                var entry = Context.Entry(item);

                if (entry.State == EntityState.Detached) dbSet.Attach(item);

                entry.State = EntityState.Modified;
            }
        }
        public async Task<bool> SaveChangesAsync()
        {
            return await Context.SaveChangesAsync() > 0;
        }
    }
}