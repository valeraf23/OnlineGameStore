using System;
using Microsoft.EntityFrameworkCore;
using OnlineGameStore.Data.Data;
using OnlineGameStore.Data.Dtos;
using OnlineGameStore.Data.Helpers;
using OnlineGameStore.Data.Repository;
using OnlineGameStore.Data.Services.Implementations;
using OnlineGameStore.Data.Services.Interfaces;

namespace OnlineGameStore.Tests.TestProject
{
    public sealed class DataBaseFixture
    {
        private static readonly Lazy<DataBaseFixture>
            Lazy =
                new Lazy<DataBaseFixture>
                    (() => new DataBaseFixture());

        public static DataBaseFixture Instance => Lazy.Value;

        private DataBaseFixture()
        {
            Database();
        }
        private DbContextOptions<OnlineGameContext> _options;
        public IGameService GameService;
        public ICommentService CommentService;
        public PublisherRepository PublisherRepository;

        public void Database()
        {

//            _options = new DbContextOptionsBuilder<OnlineGameContext>()
//                .UseInMemoryDatabase().Options;

            SeedInMemoryStore();
            var context = new OnlineGameContext(_options);
            var gameRepository = new GameRepository(context);
            var commentRepository = new CommentRepository(context);
            PublisherRepository = new PublisherRepository(context);
            GameService = new GameService(gameRepository, new DefaultValidatorStrategy<GameModel>());
            CommentService = new CommentService(commentRepository);
        }

        private void SeedInMemoryStore()
        {
            MapperHelper.InitMapperConf();
            using (var context = new OnlineGameContext(_options))
            {
                context.Database.EnsureCreated();
                context.Games.AddRange(GamesTestData.FirstGame, GamesTestData.ThirdGame,
                    GamesTestData.FourthGame);

                context.SaveChanges();
            }
        }
    }
}