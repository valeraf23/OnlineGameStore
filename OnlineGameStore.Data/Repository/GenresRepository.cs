using System.Threading;
using Microsoft.EntityFrameworkCore;
using OnlineGame.DataAccess;
using OnlineGameStore.Data.Data;
using OnlineGameStore.Domain.Entities;

namespace OnlineGameStore.Data.Repository
{
    public class GenresRepository : OnlineGameRepositoryBase<Genre, OnlineGameContext>
    {
        public GenresRepository(OnlineGameContext context) : base(context)
        {
        }

        public GenresRepository(OnlineGameContext context, CancellationTokenSource cancellation) : base(context,
            cancellation)
        {
        }

        protected override DbSet<Genre> EntityDbSet => Context.Genres;
    }
}