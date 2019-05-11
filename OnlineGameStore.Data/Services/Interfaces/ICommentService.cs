using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.Common.Either;
using OnlineGameStore.Common.Errors;
using OnlineGameStore.Data.Dtos;

namespace OnlineGameStore.Data.Services.Interfaces
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentModel>> GetAllCommentsForGame(Guid gameId);

        Task<Either<Error, CommentModel>> AddCommentToGame(Guid gameId, CommentModel comment);

        Task<Either<Error, CommentModel>> AddAnswerToComment(Guid id, Guid idComment, CommentModel comment);

        Task<IEnumerable<CommentModel>> GetCommentsForGame(Guid gameId, Func<CommentModel, bool> predicate);
    }
}