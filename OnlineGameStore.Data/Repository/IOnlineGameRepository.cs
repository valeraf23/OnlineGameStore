using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.Domain.Entities;

namespace OnlineGameStore.Data.Repository
{
    public interface IOnlineGameRepository
    {
        Task AddGame(Game gameToAdd);
        Task UpdateGame(Game game);
        Task<Game> GetGame(int gameId);
        Task<IEnumerable<Game>> GetGames();
        Task DeleteGame(int gameId);
        Task AddComment(int gameId, Comment comment);
        Task<IEnumerable<Comment>> GetAllCommentsForGame(int gameId);
        Task<IEnumerable<Game>> GetGamesByGenre(int genreId);
        Task<IEnumerable<Game>> GetGamesByPlatformTypes(int genreId);
    }
}