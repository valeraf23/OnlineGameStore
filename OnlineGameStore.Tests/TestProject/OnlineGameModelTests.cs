using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using OnlineGameStore.Data.Data;
using OnlineGameStore.Domain.Entities;
using Xunit;

namespace OnlineGameStore.Tests
{
    public class OnlineGameModelTests
    {
        private DbContextOptions<OnlineGameContext> _options;

        public OnlineGameModelTests()
        {
            _options = new DbContextOptionsBuilder<OnlineGameContext>()
                .UseInMemoryDatabase().Options;
            SeedInMemoryStore();
        }

        [Fact]
        public void TwoPlusTwoEqualsFour()
        {
            var context = new OnlineGameContext(_options);
            var game = context.Games.First();
            Assert.Equal(3, 3);
        }

        private void SeedInMemoryStore()
        {
            using (var context = new OnlineGameContext(_options))
            {
                    context.Games.AddRange(
                        new Game()
                        {
                            Id = 1,
                            Name = "Test",
                            Description = "Test_Description",
                            GameGenre = new List<GameGenre>()
                            {
                                new GameGenre()
                                {
                                    GenreId = 2,
                                    Genre = new Genre()
                                    {
                                        Id = 3,
                                        Name = "test_Genre"
                                    }
                                }
                            },
                            Publisher = new Publisher()
                            {
                                Name = "VF"
                            },
                            GamePlatformType = new List<GamePlatformType>
                            {
                                new GamePlatformType
                                {
                                    PlatformType = new PlatformType
                                    {
                                        Type = "test_PlatformType"
                                    }
                                }
                            }
                        }
                    );
                    context.SaveChanges();
                }
            }
        }
    }

