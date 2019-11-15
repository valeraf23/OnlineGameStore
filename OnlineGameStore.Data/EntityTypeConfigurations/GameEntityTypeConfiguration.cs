using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineGameStore.Domain.Entities;

namespace OnlineGameStore.Data.EntityTypeConfigurations
{
    public class GameEntityTypeConfiguration:  IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.HasMany<Comment>().WithOne(p=>p.Game).HasForeignKey(d => d.GameId).OnDelete(DeleteBehavior.SetNull);
        }
    }
}