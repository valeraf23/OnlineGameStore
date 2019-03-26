using Microsoft.EntityFrameworkCore;
using OnlineGameStore.Domain.Entities;

namespace OnlineGameStore.Data.Helpers
{
    public static class ModelBuilderExtension
    {
        public static void SetUniqueValues(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>()
                .HasIndex(p => new { p.Name })
                .IsUnique();

            modelBuilder.Entity<PlatformType>()
                .HasIndex(p => new { p.Type })
                .IsUnique();
        }
        public static void CreatingManyToManyGameGenre(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GameGenre>()
                .HasKey(pc => new { pc.GameId, pc.GenreId });

            modelBuilder.Entity<GameGenre>()
                .HasOne(pc => pc.Game)
                .WithMany(p => p.GameGenre)
                .HasForeignKey(pc => pc.GameId);

            modelBuilder.Entity<GameGenre>()
                .HasOne(pc => pc.Genre)
                .WithMany(c => c.GameGenre)
                .HasForeignKey(pc => pc.GenreId);
        }

        public static void CreatingManyToManyGamePlatformType(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GamePlatformType>()
                .HasKey(pc => new {pc.GameId, pc.PlatformTypeId});

            modelBuilder.Entity<GamePlatformType>()
                .HasOne(pc => pc.Game)
                .WithMany(p => p.GamePlatformType)
                .HasForeignKey(pc => pc.GameId);

            modelBuilder.Entity<GamePlatformType>()
                .HasOne(pc => pc.PlatformType)
                .WithMany(c => c.GamePlatformType)
                .HasForeignKey(pc => pc.PlatformTypeId);
        }
    }
}