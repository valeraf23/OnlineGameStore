using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineGameStore.Data.Data;
using OnlineGameStore.Domain.Entities;

namespace OnlineGameStore.Data.Repository
{
    public class OnlineGameRepository : IOnlineGameRepository, IDisposable
    {
        private CancellationTokenSource _cancellationTokenSource;
        private OnlineGameContext _context;

        public OnlineGameRepository(OnlineGameContext context) =>
            _context = context ?? throw new ArgumentNullException(nameof(context));

        public async Task AddGame(Game game)
        {
            if (game == null) throw new ArgumentNullException(nameof(game));
            game.Id = Guid.NewGuid();
            _context.Games.Add(game);
            await _context.Games.AddAsync(game);
        }

        public async Task<Game> GetGame(Guid gameId) => (await GetGames(x => x.Id == gameId)).FirstOrDefault();

        public void DeleteGame(Game game)
        {
            if (game == null) throw new ArgumentNullException(nameof(game));

            _context.Games.Remove(game);
        }

        public async Task AddComment(Guid gameId, Comment comment)
        {
            if (gameId == default(Guid))
                throw new ArgumentOutOfRangeException($"{nameof(gameId)} should not be greater default");

            if (comment.Id == Guid.Empty)
            {
                comment.Id = Guid.NewGuid();
                FillIdForComments(comment.Answers);
            }

            var game = await GetGame(gameId);
            game.Comments.Add(comment);
            await _context.Comments.AddAsync(comment);
        }

        public async Task<IEnumerable<Game>> GetGamesByGenre(Guid genreId) =>
            await GetGames(x => x.GameGenre.Any(g => g.GenreId == genreId));

        public async Task<IEnumerable<Game>> GetGamesByPlatformTypes(Guid genreId) =>
            await GetGames(x => x.GamePlatformType.Any(g => g.PlatformTypeId == genreId));

        public Task<IEnumerable<Comment>> GetAllCommentsForGame(Guid id) =>
            GetAllComments(comment => comment.GameId == id);

        public async Task<IEnumerable<Game>> GetGames() => await GetGames(game => true);

        private static void FillIdForComments(ICollection<Comment> answers)
        {
            if (!answers.Any()) return;
            foreach (var answer in answers)
            {
                answer.Id = Guid.NewGuid();
                FillIdForComments(answer.Answers);
            }
        }

        public async Task<bool> SaveChangesAsync() => await _context.SaveChangesAsync() > 0;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private async Task<IEnumerable<Comment>> GetAllComments(Func<Comment, bool> predicate) =>
            await _context.Comments.Where(x => x.ParentComment == null && predicate(x))
                .Include(x => x.Answers).ToListAsync();

        private async Task<IEnumerable<Game>> GetGames(Func<Game, bool> predicate)
        {
            var allGames =
                _context.Games
                    .Include(game => game.Publisher)
                    .Include(game => game.GameGenre)
                    .ThenInclude(gameGenre => gameGenre.Genre)
                    .ThenInclude(genre => genre.SubGenres)
                    .Include(game => game.GamePlatformType)
                    .ThenInclude(gamePlatformType => gamePlatformType.PlatformType);

            foreach (var game in allGames) game.Comments = (await GetAllCommentsForGame(game.Id)).ToList();

            return allGames.Where(predicate);
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