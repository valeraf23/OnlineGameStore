using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineGame.DataAccess;
using OnlineGameStore.Data.Data;
using OnlineGameStore.Domain.Entities;

namespace OnlineGameStore.Data.Repository
{
    public class CommentRepository : OnlineGameRepositoryBase<Comment, OnlineGameContext>
    {
        public CommentRepository(OnlineGameContext context) : base(context)
        {
        }

        public CommentRepository(OnlineGameContext context, CancellationTokenSource cancellation) : base(context,
            cancellation)
        {
        }

        protected override DbSet<Comment> EntityDbSet => Context.Comments;

        public override Task<IEnumerable<Comment>> GetAllAsync() => GetAsync(x => true);

        public override async Task<IEnumerable<Comment>> GetAsync(Func<Comment, bool> predicate) =>
            await Context.Comments.Include(i => i.Answers).AsEnumerable()
                .Where(x => x.ParentComment == null && predicate(x)).ToAsyncEnumerable().ToList();

     
    }
}
