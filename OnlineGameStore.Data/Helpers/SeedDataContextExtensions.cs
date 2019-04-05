using System.Linq;
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
            (DefaultGenresManager.Action,
                DefaultGenresManager.Adventure,
                DefaultGenresManager.Misc,
                DefaultGenresManager.PuzzleSkill,
                DefaultGenresManager.Rpg,
                DefaultGenresManager.Races,
                DefaultGenresManager.Sports,
                DefaultGenresManager.Strategy
            );

            modelBuilder.Entity<Genre>().HasData
            (DefaultSubGenresManager.Action
                .Concat(DefaultSubGenresManager.Races
                    .Concat(DefaultSubGenresManager.Strategy)));
        }

        private static void EnsureSeedDataForPlatformTypeContext(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlatformType>().HasData
            (
                PlatformTypeManager.Browser,
                PlatformTypeManager.Console,
                PlatformTypeManager.Desktop,
                PlatformTypeManager.Mobile
            );
        }
    }
}