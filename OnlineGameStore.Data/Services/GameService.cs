using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using OnlineGame.DataAccess;
using OnlineGameStore.Common.Either;
using OnlineGameStore.Common.Errors;
using OnlineGameStore.Data.Dtos;
using OnlineGameStore.Data.Helpers;
using OnlineGameStore.Domain.Entities;

namespace OnlineGameStore.Data.Services
{

    public class GameService : IGameService
    {
        private readonly IRepository<Game> _repository;
        private readonly IValidatorStrategy<GameModel> _validator;

        public GameService(IRepository<Game> repositoryInstance, IValidatorStrategy<GameModel> validator)
        {
            _repository = repositoryInstance ??
                          throw new ArgumentNullException(nameof(repositoryInstance),
                              "personRepositoryInstance is null.");
            _validator = validator;
        }

        public void DeleteGameById(Guid id)
        {
            var game = _repository.GetAsync(id).GetAwaiter().GetResult();
            _repository.Delete(game);
        }

        public async Task<GameModel> GetGameByIdAsync(Guid id) => (await _repository.GetAsync(id)).ToModel<GameModel>();

        public async Task<IEnumerable<GameModel>> GetGamesAsync()
        {
            var games = await _repository.GetAllAsync();
            return games.ToModel<GameModel>();
        }

        public async Task<IEnumerable<GameModel>> GetGamesByGenreAsync(Guid genreId)
        {
            var games = await _repository.GetAsync(x => x.GameGenre.Any(g => g.GenreId == genreId));
            return games.ToModel<GameModel>();
        }

        public async Task<IEnumerable<GameModel>> GetGamesByPlatformTypesAsync(Guid genreId)
        {
            var games = await _repository.GetAsync(x => x.GamePlatformType.Any(g => g.PlatformTypeId == genreId));
            return games.ToModel<GameModel>();
        }

        public async Task<Either<Error, GameModel>> SaveSafe(GameForCreationModel obj)
        {
            var gameModel = Mapper.Map<GameModel>(obj);
            return !_validator.IsValid(gameModel)
                ? new UnprocessableError()
                : (await _repository.SaveAsync(gameModel.ToEntity<Game>())).Map(e => e.ToModel<GameModel>());
        }
    }
}