using Microsoft.EntityFrameworkCore;
using OnlineGameStore.Data.Data;
using OnlineGameStore.Domain.Entities;

namespace OnlineGameStore.Data.Helpers
{
    public static class SeedDataContextExtensions
    {
        public static void EnsureSeedDataForContext(this ModelBuilder context)
        {
            EnsureSeedDataForGenreContext(context);
            EnsureSeedDataForPlatformTypeContext(context);
        }

        private static void EnsureSeedDataForGenreContext(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>().HasData
            (DefaultGenresFactories.Action,
                DefaultGenresFactories.Adventure,
                DefaultGenresFactories.Misc,
                DefaultGenresFactories.PuzzleSkill,
                DefaultGenresFactories.Rpg,
                DefaultGenresFactories.Races,
                DefaultGenresFactories.Sports,
                DefaultGenresFactories.Strategy
            );
        }

        private static void EnsureSeedDataForPlatformTypeContext(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlatformType>().HasData
            (
                PlatformTypeFactory.Browser,
                PlatformTypeFactory.Console,
                PlatformTypeFactory.Desktop,
                PlatformTypeFactory.Mobile
            );
        }
    }
}