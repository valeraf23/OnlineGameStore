using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.Common.Either;
using OnlineGameStore.Common.Errors;
using OnlineGameStore.Data.Dtos;

namespace OnlineGameStore.Data.Services.Interfaces
{
    public interface IGameService
    {
        void DeleteGameById(Guid id);
        Task<GameModel> GetGameByIdAsync(Guid id);
        Task<IEnumerable<GameModel>> GetGamesAsync();
        Task<IEnumerable<GameModel>> GetGamesByGenreAsync(Guid genreId);
        Task<IEnumerable<GameModel>> GetGamesByPlatformTypesAsync(Guid genreId);
        Task<Either<Error, GameModel>> SaveSafe(GameModel obj);
        Task<Either<Error, GameModel>> UpdateSafe(Guid id,GameModel obj);
        Task<bool> IsGameOwner(Guid gameId, Guid publisherId);
    }
}