using System;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using OnlineGameStore.Data.Data;
using OnlineGameStore.Data.Dtos;
using OnlineGameStore.Data.Helpers;
using OnlineGameStore.Data.Repository;
using OnlineGameStore.Data.Services;
using OnlineGameStore.Data.Services.Implementations;
using OnlineGameStore.Data.Services.Interfaces;

namespace OnlineGameStore.Tests.TestProject
{
    public class DatabaseFixture : IDisposable
    {
        private readonly DbContextOptions<OnlineGameContext> _options;
        public readonly IGameService GameService;
        public readonly ICommentService CommentService;
        private static readonly AutoResetEvent WaitHandler = new AutoResetEvent(true);
        private static bool IsDone { get; set; }

        public DatabaseFixture()
        {
         
            _options = new DbContextOptionsBuilder<OnlineGameContext>()
                .UseInMemoryDatabase().Options;

            SeedInMemoryStore();
            var context = new OnlineGameContext(_options);
            var repository = new GameRepository(context);
            var repository1 = new CommentRepository(context);
            GameService = new GameService(repository,new DefaultValidatorStrategy<GameModel>());
            CommentService = new CommentService(repository1);
        }

        public void Dispose()
        {
        }

        private void SeedInMemoryStore()
        {
            WaitHandler.WaitOne();

            try
            {
                if (IsDone) return;
                MapperHelper.InitMapperConf();
                using (var context = new OnlineGameContext(_options))
                {
                    context.Database.EnsureCreated();
                    context.Games.AddRange(GamesTestData.FirstGame, GamesTestData.ThirdGame,
                        GamesTestData.FourthGame);
                   
                    context.SaveChanges();
                }

                IsDone = true;
            }
            finally
            {
                WaitHandler.Set();
            }
        }
    }
}