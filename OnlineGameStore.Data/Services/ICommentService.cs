using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.Data.Dtos;

namespace OnlineGameStore.Data.Services
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentModel>> GetAllCommentsForGame(Guid gameId);

        Task<bool> AddCommentToGame(Guid gameId, CommentModel comment);

        Task<bool> AddAnswerToComment(Guid commentId, CommentModel comment);

        Task<IEnumerable<CommentModel>> GetCommentsForGame(Guid gameId, Func<CommentModel, bool> predicate);
    }
}