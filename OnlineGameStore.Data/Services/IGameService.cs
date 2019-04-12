using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.Common.Either;
using OnlineGameStore.Common.Errors;
using OnlineGameStore.Data.Dtos;

namespace OnlineGameStore.Data.Services
{
    public interface IGameService
    {
        Task<bool> AddAsync(GameModel saveThis);
        void DeleteGameById(Guid id);
        Task<GameModel> GetGameByIdAsync(Guid id);
        Task<IEnumerable<GameModel>> GetGamesAsync();
        Task<IEnumerable<GameModel>> GetGamesByGenreAsync(Guid genreId);
        Task<IEnumerable<GameModel>> GetGamesByPlatformTypesAsync(Guid genreId);
        Task<Either<Error, GameModel>> SaveSafe(GameModel obj);
    }
}