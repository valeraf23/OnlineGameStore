using Microsoft.EntityFrameworkCore;
using OnlineGameStore.Data.Helpers;
using OnlineGameStore.Domain.Entities;

namespace OnlineGameStore.Data.Data
{
    public class OnlineGameContext : DbContext
    {
        public OnlineGameContext(DbContextOptions<OnlineGameContext> options)
            : base(options)
        {
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<PlatformType> PlatformTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Genre>()
                .HasMany(u => u.SubGenres)
                .WithOne(p => p.ParentGenre)
                .HasForeignKey(p => p.ParentId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .Entity<Comment>()
                .HasMany(u => u.Answers)
                .WithOne(p => p.ParentComment)
                .HasForeignKey(p => p.ParentId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.SetUniqueValues();
            modelBuilder.CreatingManyToManyGameGenre();
            modelBuilder.CreatingManyToManyGamePlatformType();
            modelBuilder.EnsureSeedDataForContext();
            base.OnModelCreating(modelBuilder);
        }
    }
}
