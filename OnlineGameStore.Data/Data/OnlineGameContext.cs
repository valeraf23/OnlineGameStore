using Microsoft.EntityFrameworkCore;
using OnlineGameStore.Data.EntityTypeConfigurations;
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
            modelBuilder.ApplyConfiguration(new GameEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CommentEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new GameGenreEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new GamePlatformTypeEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new GenreEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PlatformTypeEntityTypeConfiguration());

            modelBuilder.EnsureSeedDataForContext();
            base.OnModelCreating(modelBuilder);
        }
    }
}
