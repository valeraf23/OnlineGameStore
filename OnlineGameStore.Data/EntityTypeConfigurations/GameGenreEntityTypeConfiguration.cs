using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineGameStore.Domain.Entities;

namespace OnlineGameStore.Data.EntityTypeConfigurations
{
    internal class GameGenreEntityTypeConfiguration : IEntityTypeConfiguration<GameGenre>
    {
        public void Configure(EntityTypeBuilder<GameGenre> builder)
        {
            builder
                .HasKey(pc => new {pc.GameId, pc.GenreId});

            builder
                .HasOne(pc => pc.Game)
                .WithMany(p => p.GameGenre)
                .HasForeignKey(pc => pc.GameId);

            builder
                .HasOne(pc => pc.Genre)
                .WithMany(c => c.GameGenre)
                .HasForeignKey(pc => pc.GenreId);
        }

    }
}