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

        public async Task<bool> AddAsync(GameModel saveThis)
        {
           return await _repository.SaveAsync(saveThis.ToEntity<Game>());
        }

        public void DeleteGameById(Guid id)
        {
            var game = _repository.GetAsync(id).GetAwaiter().GetResult();
            _repository.Delete(game);
        }

        public async Task<GameModel> GetGameByIdAsync(Guid id)
        {
            return (await _repository.GetAsync(id)).ToModel<GameModel>();
        }

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
            if (!_validator.IsValid(gameModel)) return new UnprocessableError();

            if (await AddAsync(gameModel))
            {
                return gameModel;
            }
            return new SaveError("Creating a game failed on save.");
        }
    }
}