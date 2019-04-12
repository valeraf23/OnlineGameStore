using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineGame.DataAccess;
using OnlineGameStore.Data.Data;
using OnlineGameStore.Domain.Entities;

namespace OnlineGameStore.Data.Repository
{
    public class GameRepository : OnlineGameRepositoryBase<Domain.Entities.Game, OnlineGameContext>
    {
        public GameRepository(OnlineGameContext context) : base(context)
        {
        }

        public GameRepository(OnlineGameContext context, CancellationTokenSource cancellation) : base(context,
            cancellation)
        {
        }

        protected override DbSet<Game> EntityDbSet => Context.Games;

        public override async Task<IEnumerable<Game>> GetAllAsync() => await GetAsync(game => true);


        public override async Task<IEnumerable<Game>> GetAsync(Func<Game, bool> predicate)
        {
            var allGames =
                Context.Games
                    .Include(game => game.Publisher)
                    .Include(game => game.GameGenre)
                    .ThenInclude(gameGenre => gameGenre.Genre)
                    .ThenInclude(genre => genre.SubGenres)
                    .Include(game => game.GamePlatformType)
                    .ThenInclude(gamePlatformType => gamePlatformType.PlatformType).ToListAsync();

            //  foreach (var game in allGames) game.Comments = (await GetAllCommentsForGame(game.Id)).ToList();

            return (await allGames).Where(predicate);
        }
    }
}