using System.Linq;
using OnlineGameStore.Data.Dtos;
using OnlineGameStore.Data.Services;
using Xunit;

namespace OnlineGameStore.Tests.TestProject
{
    public class CommentServiceTests : IClassFixture<DatabaseFixture>
    {
        public CommentServiceTests(DatabaseFixture fixture) => _service = fixture.CommentService;

        private readonly ICommentService _service;

        [Fact]
        public async void Get_All_Comments_By_Game_Key()
        {
            var actually = (await _service.GetAllCommentsForGame(GamesTestData.FirstGame.Id)).Count();
            var expected = GamesTestData.FirstGame.Comments.Count;
            Assert.Equal(expected, actually);
        }


        [Fact]
        public async void Add_Comment_To_Game()
        {
            var expected = new CommentModel
            {
                Name = "Some_Comment",
                Body = "Some_body"
            };

             await _service.AddCommentToGame(GamesTestData.ThirdGame.Id, expected);

            var actual = (await _service.GetCommentsForGame(GamesTestData.ThirdGame.Id, x => x.Name == expected.Name))
                .FirstOrDefault();
            Assert.NotNull(actual);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.Body, actual.Body);
        }
    }
}