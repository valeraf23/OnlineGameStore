using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using ObjectsComparator.Comparator;
using OnlineGameStore.Common.Either;
using OnlineGameStore.Common.Errors;
using OnlineGameStore.Data.Dtos;
using OnlineGameStore.Data.Helpers;
using OnlineGameStore.Data.Repository;
using OnlineGameStore.Data.Services.Interfaces;

namespace OnlineGameStore.Tests.TestProject
{
    [TestFixture]
    public class GameServiceTests
    {
        private readonly IGameService _gameService = DataBaseFixture.Instance.GameService;
        private readonly PublisherRepository _publisherRepository = DataBaseFixture.Instance.PublisherRepository;

        [Test]
        public async Task Can_Get_Game_By_Key()
        {
            var entity = GamesTestData.FirstGame;

            var expected = entity.ToModel<GameModel>();
            var actually = await _gameService.GetGameByIdAsync(entity.Id);

            var result = expected.GetDistinctions(actually,
                pr => pr.Equals("Comments") || pr.EndsWith("Id") || pr.EndsWith("ParentGenre"));
            result.Should().BeEmpty();
        }

        [Test]
        public async Task Can_Create_New_Game()
        {
            var publisherId = _publisherRepository.GetAllAsync().GetAwaiter().GetResult().First().Id;
            var entity = GamesTestData.SecondGame;
            var expected = new GameForCreationModel
            {
                Name = entity.Name,
                Description = entity.Description,
                PlatformTypesId = entity.GamePlatformType.Select(x => x.PlatformTypeId).ToArray(),
                GenresId = entity.GameGenre.Select(x => x.Genre.Id).ToArray(),
                PublisherId = publisherId
            };

            var ee = Mapper.Map<GameModel>(expected);
            var res = await _gameService.SaveSafe(ee);
            res.Should().BeOfType<Right<Error, GameModel>>();
        }

        [Test]
        public async Task Can_Create_New_Game_Safe()
        {
            var res = await _gameService.SaveSafe(Mapper.Map<GameModel>(new GameForCreationModel()));
            var actually = res.OnSuccess(created => created.Name)
                .OnFailure(er => er.ToString(), error => error is UnprocessableError)
                .OnFailure(_ => "InternalServerError");
            actually.Should().Be("Name: You should fill out a Name.\r\nDescription: You should fill out a Description.\r\n");
        }

        [Test]
        public async Task Can_Get_All_Games()
        {
            var games = await _gameService.GetGamesAsync();
            games.Count().Should().BeGreaterThan(1);
        }

        [Test]
        public async Task Can_Delete_Game()
        {
            _gameService.DeleteGameById(GamesTestData.FourthGame.Id);
            var games = await _gameService.GetGamesAsync();
            var isGameExist = games.Any(x => x.Name == GamesTestData.FourthGame.Name);
            isGameExist.Should().BeFalse("Game was not deleted");
        }

        [Test]
        public async Task Get_Games_By_Genre()
        {
            var expected = GuidsManager.Get[31];
            var actually = await _gameService.GetGamesByGenreAsync(expected);
            GamesTestData.FirstGame.Name.Should().BeEquivalentTo(actually.First().Name);
        }

        [Test]
        public async Task Get_Games_By_PlatformTypes()
        {
            var expected = GuidsManager.Get[35];
            var actually = await _gameService.GetGamesByPlatformTypesAsync(expected);
            GamesTestData.FirstGame.Name.Should().BeEquivalentTo(actually.First().Name);
        }

    }
}