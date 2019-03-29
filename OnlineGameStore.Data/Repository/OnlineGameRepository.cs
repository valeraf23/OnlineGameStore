using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using OnlineGameStore.Data.Data;
using OnlineGameStore.Domain.Entities;

namespace OnlineGameStore.Data.Repository
{
    public class OnlineGameRepository : IOnlineGameRepository, IDisposable
    {
        private OnlineGameContext _context;
        private CancellationTokenSource _cancellationTokenSource;

        public OnlineGameRepository(OnlineGameContext context)
        {
            _context = context;
        }

        public async Task AddGame(Game gameToAdd)
        {
            if (gameToAdd == null)
            {
                throw new ArgumentNullException(nameof(gameToAdd));
            }

            await _context.AddAsync(gameToAdd);
        }

        public Task UpdateGame(Game game)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Game> GetGame(int gameId)
        {
            return await Get().FirstOrDefaultAsync(x => x.Id == gameId);
        }

        public async Task<IEnumerable<Game>> GetGames()
        {
            var games = await Get().ToListAsync();
            return games;
        }

        public Task DeleteGame(int gameId)
        {
            throw new System.NotImplementedException();
        }

        public Task AddComment(int gameId, Comment comment)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Comment>> GetAllCommentsForGame(int gameId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<Game>> GetGamesByGenre(int genreId)
        {
            var games = await Get().Where(x => x.GameGenre.Any(g => g.GenreId == genreId)).ToArrayAsync();
            return games;
        }

        public Task<IEnumerable<Game>> GetGamesByPlatformTypes(int genreId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }

        private IIncludableQueryable<Game, List<Genre>> Get()
        {
            var games =
                _context.Games
                    .Include(s => s.Publisher)
                    .Include(s => s.Comments).ThenInclude(x =>
                        x.Answers.Where(a => a.ParentComment == null).Select(f => new Comment
                            {Id = f.Id, Name = f.Name, ParentId = f.ParentId, Answers = f.Answers}).ToList())
                    .Include(c => c.GamePlatformType).ThenInclude(p => p.PlatformType)
                    .Include(c => c.GameGenre).ThenInclude(p => p.Genre).ThenInclude(c =>
                        c.SubGenres.Where(p => p.ParentGenre == null).Select(f => new Genre
                            {Id = f.Id, Name = f.Name, ParentId = f.ParentId, SubGenres = f.SubGenres}).ToList());
            return games;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                    _context = null;
                }

                if (_cancellationTokenSource != null)
                {
                    _cancellationTokenSource.Dispose();
                    _cancellationTokenSource = null;
                }
            }
        }
    }
}
