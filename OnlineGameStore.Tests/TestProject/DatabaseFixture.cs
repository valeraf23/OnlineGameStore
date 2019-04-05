using System;
using Microsoft.EntityFrameworkCore;
using OnlineGameStore.Data.Data;
using OnlineGameStore.Data.Repository;

namespace OnlineGameStore.Tests.TestProject
{
    public class DatabaseFixture : IDisposable
    {
        private readonly DbContextOptions<OnlineGameContext> _options;
        public readonly OnlineGameRepository Service;

        public DatabaseFixture()
        {
            _options = new DbContextOptionsBuilder<OnlineGameContext>()
                .UseInMemoryDatabase().Options;
            SeedInMemoryStore();
            var context = new OnlineGameContext(_options);
            Service = new OnlineGameRepository(context);
        }

        public void Dispose()
        {
        }

        private void SeedInMemoryStore()
        {
            using (var context = new OnlineGameContext(_options))
            {
                context.Games.AddRange(GamesTestData.FirstGame, GamesTestData.ThirdGame,
                    GamesTestData.FourthGame);
                context.SaveChanges();
            }
        }
    }
}