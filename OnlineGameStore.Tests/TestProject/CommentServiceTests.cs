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

        [Test]
        public async Task Add_New_Comment()
        {
            var gameId = GamesTestData.FirstGame.Id;
            var comments = await _commentService.GetAllCommentsForGame(GamesTestData.FirstGame.Id);
            var parentCommentId = comments.First();
            var newAnswer = new CommentModel
            {
                Name = "Answer",
                Body = "This is answer"
            };
            await _commentService.AddAnswerToCommentAsync(gameId, parentCommentId.Id, newAnswer);
            var commentsWithAnswer = await _commentService.GetAllCommentsForGame(GamesTestData.FirstGame.Id)
                .ContinueWith(x => x.Result.FirstOrDefault(c => c.Id == parentCommentId.Id));
            var answer = commentsWithAnswer.Answers.FirstOrDefault(x=>x.Body == newAnswer.Body);

            MultiplyAssertion.For(() =>
            {
                answer.Should().NotBeNull();
                answer.Body.Should().Be(newAnswer.Body);
            });
        }
    }
}