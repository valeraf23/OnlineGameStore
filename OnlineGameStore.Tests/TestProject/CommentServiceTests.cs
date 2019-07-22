using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using OnlineGameStore.Data.Dtos;
using OnlineGameStore.Data.Services.Interfaces;

namespace OnlineGameStore.Tests.TestProject
{
    [TestFixture]
    public class CommentServiceTests
    {
        private readonly ICommentService _commentService = DataBaseFixture.Instance.CommentService;

        [Test]
        public async Task Get_All_Comments_By_Game_Key()
        {
            var actually = (await _commentService.GetAllCommentsForGame(GamesTestData.FirstGame.Id)).Count();
            var expected = GamesTestData.FirstGame.Comments.Count;
            expected.Should().Be(actually);
        }

//
//        [Test]
//        public async Task Add_Comment_To_Game()
//        {
//            var expected = new CommentModel
//            {
//                Name = "Some_Comment",
//                Body = "Some_body"
//            };
//
//            await _commentService.AddCommentToGame(GamesTestData.ThirdGame.Id, expected);
//
//            var actual =
//                (await _commentService.GetCommentsForGame(GamesTestData.ThirdGame.Id, x => x.Name == expected.Name))
//                .FirstOrDefault();
//            Assert.NotNull(actual);
//            expected.Name.Should().BeEquivalentTo(actual.Name);
//            expected.Body.Should().BeEquivalentTo(actual.Body);
//        }
    }
}