using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ObjectsComparator.Comparator;
using OnlineGameStore.Common.Either;
using OnlineGameStore.Common.Errors;
using OnlineGameStore.Data.Dtos;
using OnlineGameStore.Data.Helpers;
using OnlineGameStore.Data.Services;
using OnlineGameStore.Data.Services.Interfaces;
using Xunit;

namespace OnlineGameStore.Tests.TestProject
{
    public class GameServiceTests : IClassFixture<DatabaseFixture>
    {
        public GameServiceTests(DatabaseFixture fixture) => _service = fixture.GameService;

        private readonly IGameService _service;

        [Fact]
        public async Task Can_Get_Game_By_Key()
        {
            var entity = GamesTestData.FirstGame;

            var expected = entity.ToModel<GameModel>();
            var actually = await _service.GetGameByIdAsync(entity.Id);

            var result = expected.GetDistinctions(actually,
                pr => pr.Equals("Comments") || pr.EndsWith("Id") || pr.EndsWith("ParentGenre"));
            Assert.Empty(result);
        }

        [Fact]
        public async void Can_Create_New_Game()
        {
            var entity = GamesTestData.SecondGame;
            var expected = new GameForCreationModel
            {
                Name = entity.Name,
                Description = entity.Description,
                PlatformTypesId = entity.GamePlatformType.Select(x => x.PlatformTypeId).ToArray(),
                GenresId = entity.GameGenre.Select(x => x.Genre.Id).ToArray(),
                PublisherId = Guid.NewGuid()
            };

            var ee = Mapper.Map<GameModel>(expected);
            await _service.SaveSafe(ee);

            var actually =
                (await _service.GetGamesAsync()).FirstOrDefault(x =>
                    x.Description == GamesTestData.SecondGame.Description);

            var result = GamesTestData.SecondGame.ToModel<GameModel>().GetDistinctions(actually,
                pr => pr.Equals("Comments") || pr.StartsWith("PlatformTypes") || pr.Equals("Publisher.Name") || pr.EndsWith("Id") || pr.EndsWith("ParentGenre"));
            Assert.Empty(result);
        }

        [Fact]
        public async void Can_Create_New_Game_Safe()
        {
            var res = await _service.SaveSafe(Mapper.Map<GameModel>(new GameForCreationModel()));
            const string expected = "UnprocessableError";
            var actually = res.Map(created => created.Name)
                .Reduce(_ => expected, error => error is UnprocessableError)
                .Reduce(_ => "InternalServerError");

            Assert.Equal(expected, actually);
        }

        [Fact]
        public async void Can_Get_All_Games()
        {
            var games = await _service.GetGamesAsync();
            Assert.True(games.Count() > 1);
        }

        [Fact]
        public async void Can_Delete_Game()
        {
            _service.DeleteGameById(GamesTestData.FourthGame.Id);
            var games = await _service.GetGamesAsync();
            var isGameExist = games.Any(x => x.Name == GamesTestData.FourthGame.Name);
            Assert.False(isGameExist, "Game was not deleted");
        }

        [Fact]
        public async void Get_Games_By_Genre()
        {
            var expected = GuidsManager.Get[3];
            var actually = await _service.GetGamesByGenreAsync(expected);
            Assert.Equal(GamesTestData.FirstGame.Name, actually.First().Name);
        }

        [Fact]
        public async void Get_Games_By_PlatformTypes()
        {
            var expected = GuidsManager.Get[1];
            var actually = await _service.GetGamesByPlatformTypesAsync(expected);
            Assert.Equal(GamesTestData.FirstGame.Name, actually.First().Name);
        }
    }
}