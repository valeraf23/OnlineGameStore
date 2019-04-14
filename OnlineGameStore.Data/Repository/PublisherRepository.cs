using System.Threading;
using Microsoft.EntityFrameworkCore;
using OnlineGame.DataAccess;
using OnlineGameStore.Data.Data;
using OnlineGameStore.Domain.Entities;

namespace OnlineGameStore.Data.Repository
{
    public class PublisherRepository : OnlineGameRepositoryBase<Publisher, OnlineGameContext>
    {
        public PublisherRepository(OnlineGameContext context) : base(context)
        {
        }

        public PublisherRepository(OnlineGameContext context, CancellationTokenSource cancellation) : base(context,
            cancellation)
        {
        }

        protected override DbSet<Publisher> EntityDbSet => Context.Publishers;
    }
}