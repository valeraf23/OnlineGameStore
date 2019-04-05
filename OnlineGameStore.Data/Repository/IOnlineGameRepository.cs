using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.Domain.Entities;

namespace OnlineGameStore.Data.Repository
{
    public interface IOnlineGameRepository
    {
        Task AddGame(Game game);
        Task<Game> GetGame(Guid gameId);
        Task<IEnumerable<Game>> GetGames();
        void DeleteGame(Game game);
        Task AddComment(Guid gameId, Comment comment);
        Task<IEnumerable<Comment>> GetAllCommentsForGame(Guid gameId);
        Task<IEnumerable<Game>> GetGamesByGenre(Guid genreId);
        Task<IEnumerable<Game>> GetGamesByPlatformTypes(Guid genreId);
        Task<bool> SaveChangesAsync();
    }
}