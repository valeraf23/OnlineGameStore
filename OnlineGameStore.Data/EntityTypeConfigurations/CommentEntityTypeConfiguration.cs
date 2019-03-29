using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineGameStore.Domain.Entities;

namespace OnlineGameStore.Data.EntityTypeConfigurations
{
    internal class CommentEntityTypeConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder
                .HasMany(u => u.Answers)
                .WithOne(p => p.ParentComment)
                .HasForeignKey(p => p.ParentId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
