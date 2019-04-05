using System.Linq;
using System.Threading.Tasks;
using OnlineGameStore.Data.Helpers;
using OnlineGameStore.Data.Repository;
using OnlineGameStore.Domain.Entities;
using Xunit;

namespace OnlineGameStore.Tests.TestProject
{
    public class OnlineGameModelTests : IClassFixture<DatabaseFixture>
    {
        public OnlineGameModelTests(DatabaseFixture fixture) => _service = fixture.Service;

        private readonly OnlineGameRepository _service;

        [Fact]
        public async Task Can_Get_Game_By_Key()
        {
            var expected = GamesTestData.FirstGame;
            var actually = await _service.GetGame(expected.Id);

            Assert.Equal(expected.Description, actually.Description);
            Assert.Equal(expected.Name, actually.Name);
            Assert.Equal(expected.Publisher.Name, actually.Publisher.Name);
        }

        [Fact]
        public async void Get_All_Comments_By_Game_Key()
        {
            var actually = (await _service.GetAllCommentsForGame(GamesTestData.FirstGame.Id)).Count();
            var expected = GamesTestData.FirstGame.Comments.Count;
            Assert.Equal(expected, actually);
        }

        [Fact]
        public async void Can_Create_New_Game()
        {
            var expected = GamesTestData.SecondGame;
            await _service.AddGame(expected);
            var isSaved = await _service.SaveChangesAsync();
            var actually = await _service.GetGame(expected.Id);

            Assert.True(isSaved, "New Game was not saved");
            Assert.Equal(expected.Description, actually.Description);
            Assert.Equal(expected.Name, actually.Name);
            Assert.Equal(expected.Publisher.Name, actually.Publisher.Name);
        }

        [Fact]
        public async void Can_Get_All_Games()
        {
            var games = await _service.GetGames();
            Assert.True(games.Count() > 1);
        }

        [Fact]
        public async void Can_Delete_Game()
        {
            var game = await _service.GetGame(GamesTestData.FourthGame.Id);
            _service.DeleteGame(game);
            await _service.SaveChangesAsync();
            var games = await _service.GetGames();
            var isGameExist = games.Any(x => x.Id == GamesTestData.FourthGame.Id);
            Assert.False(isGameExist, "Game was not deleted");
        }

        [Fact]
        public async void Add_Comment_To_Game()
        {
            var expected = new Comment
            {
                Name = "Some_Comment",
                Body = "Some_body"
            };

            await _service.AddComment(GamesTestData.ThirdGame.Id, expected);
            await _service.SaveChangesAsync();
            var actual = (await _service.GetGame(GamesTestData.ThirdGame.Id)).Comments.FirstOrDefault();
            Assert.NotNull(actual);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.Body, actual.Body);
        }

        [Fact]
        public async void Get_Games_By_Genre()
        {
            var expected = GuidsManager.Get[3];
            var actually = await _service.GetGamesByGenre(expected);
            Assert.Equal(GamesTestData.FirstGame.Name, actually.First().Name);
        }

        [Fact]
        public async void Get_Games_By_PlatformTypes()
        {
            var expected = GuidsManager.Get[1];
            var actually = await _service.GetGamesByPlatformTypes(expected);
            Assert.Equal(GamesTestData.FirstGame.Name, actually.First().Name);
        }
    }
}