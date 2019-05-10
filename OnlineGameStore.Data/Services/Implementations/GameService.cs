using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineGame.DataAccess.Interfaces;
using OnlineGameStore.Data.Dtos;
using OnlineGameStore.Data.Helpers;
using OnlineGameStore.Data.Services.Interfaces;
using OnlineGameStore.Domain.Entities;

namespace OnlineGameStore.Data.Services.Implementations
{

    public class GameService : BaseService<GameModel, Game>, IGameService
    {
        public GameService(IRepository<Game> repositoryInstance, IValidatorStrategy<GameModel> validator)
            : base(repositoryInstance, validator)
        {
        }

        public void DeleteGameById(Guid id)
        {
            var game = Repository.GetAsync(id).GetAwaiter().GetResult();
            Repository.Delete(game);
        }

        public async Task<GameModel> GetGameByIdAsync(Guid id) => (await Repository.GetAsync(id)).ToModel<GameModel>();

        public async Task<IEnumerable<GameModel>> GetGamesAsync()
        {
            var games = await Repository.GetAllAsync();
            return games.ToModel<GameModel>();
        }

        public async Task<IEnumerable<GameModel>> GetGamesByGenreAsync(Guid genreId)
        {
            var games = await Repository.GetAsync(x => x.GameGenre.Any(g => g.GenreId == genreId));
            return games.ToModel<GameModel>();
        }

        public async Task<IEnumerable<GameModel>> GetGamesByPlatformTypesAsync(Guid genreId)
        {
            var games = await Repository.GetAsync(x => x.GamePlatformType.Any(g => g.PlatformTypeId == genreId));
            return games.ToModel<GameModel>();
        }
    }
}