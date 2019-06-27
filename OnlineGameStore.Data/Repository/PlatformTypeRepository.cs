using System.Threading;
using Microsoft.EntityFrameworkCore;
using OnlineGame.DataAccess;
using OnlineGameStore.Data.Data;
using OnlineGameStore.Domain.Entities;

namespace OnlineGameStore.Data.Repository
{
    public class PlatformTypeRepository : OnlineGameRepositoryBase<PlatformType, OnlineGameContext>
    {
        public PlatformTypeRepository(OnlineGameContext context) : base(context)
        {
        }

        public PlatformTypeRepository(OnlineGameContext context, CancellationTokenSource cancellation) : base(context,
            cancellation)
        {
        }

        protected override DbSet<PlatformType> EntityDbSet => Context.PlatformTypes;
    }
}