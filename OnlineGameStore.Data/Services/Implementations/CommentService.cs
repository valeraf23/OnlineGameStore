using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using OnlineGame.DataAccess.Interfaces;
using OnlineGameStore.Common.Either;
using OnlineGameStore.Common.Errors;
using OnlineGameStore.Data.Dtos;
using OnlineGameStore.Data.Helpers;
using OnlineGameStore.Data.Services.Interfaces;
using OnlineGameStore.Domain.Entities;

namespace OnlineGameStore.Data.Services.Implementations
{
    public class CommentService : ICommentService
    {
        private readonly IRepository<Comment> _repository;

        public CommentService(IRepository<Comment> repositoryInstance)
        {
            _repository = repositoryInstance ??
                          throw new ArgumentNullException(nameof(repositoryInstance),
                              "personRepositoryInstance is null.");
        }

        public async Task<IEnumerable<CommentModel>> GetAllCommentsForGame(Guid gameId)
        {
            var comments = await _repository.GetAsync(comment => comment.GameId == gameId);
            return Mapper.Map<IEnumerable<CommentModel>>(comments);
        }

        public async Task<Either<Error, CommentModel>> AddCommentToGame(Guid gameId, CommentModel comment)
        {
            var entity = comment.ToEntity<Comment>();
            entity.GameId = gameId;
            return (await _repository.SaveAsync(entity)).Map(e => e.ToModel<CommentModel>());
        }

        public async Task<Either<Error, CommentModel>> AddAnswerToComment(Guid commentId, CommentModel comment)
        {
            var entity = comment.ToEntity<Comment>();
            entity.ParentId = commentId;
            return (await _repository.SaveAsync(entity)).Map(e => e.ToModel<CommentModel>());
        }

        public async Task<IEnumerable<CommentModel>> GetCommentsForGame(Guid gameId,
            Func<CommentModel, bool> predicate)
        {
            var comments = await GetAllCommentsForGame(gameId);
            return comments.Where(predicate);
        }
    }
}
