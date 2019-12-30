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
    public class CommentService : BaseService<CommentModel, Comment>, ICommentService
    {
        public CommentService(IRepository<Comment> repositoryInstance,
            IValidatorStrategy<CommentModel> validator) :
            base(repositoryInstance, validator)
        {

        }

        public async Task<IEnumerable<CommentModel>> GetAllCommentsForGame(Guid gameId)
        {
            var comments = await Repository.GetAsync(comment => comment.GameId == gameId);
            return Mapper.Map<IEnumerable<CommentModel>>(comments);
        }

        public async Task<Either<Error, CommentModel>> AddCommentToGameAsync(Guid gameId, CommentModel comment)
        {
            var entity = comment.ToEntity<Comment>();
            entity.GameId = gameId;
            return (await Repository.SaveAsync(entity)).OnSuccess(e => e.ToModel<CommentModel>());
        }

        public async Task<Either<Error, CommentModel>> AddAnswerToCommentAsync(Guid gameId, Guid commentId,
            CommentModel comment)
        {
            var entity = comment.ToEntity<Comment>();
            entity.ParentId = commentId;
            entity.GameId = gameId;
            return (await Repository.SaveAsync(entity)).OnSuccess(e => e.ToModel<CommentModel>());
        }

        public async Task<IEnumerable<CommentModel>> GetCommentsForGameAsync(Guid gameId,
            Func<CommentModel, bool> predicate)
        {
            var comments = await GetAllCommentsForGame(gameId);
            return comments.Where(predicate);
        }
    }
}
