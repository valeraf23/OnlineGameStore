using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using OnlineGameStore.Data.Data;
using OnlineGameStore.Domain.Entities;

namespace OnlineGameStore.Data.Helpers
{
    public static class SeedDataContextExtensions
    {
        public static void EnsureSeedDataForContext(this OnlineGameContext context)
        {
            EnsureSeedDataForGenreContext(context);
            EnsureSeedDataForPlatformTypeContext(context);
            context.SaveChanges();
        }

        private static void EnsureSeedDataForGenreContext(OnlineGameContext context)
        {
            context.Genres.RemoveRange(context.Genres);
            context.SaveChanges();

            var genres = new List<Genre>
            {
                DefaultGenresManager.Action,
                DefaultGenresManager.Adventure,
                DefaultGenresManager.Misc,
                DefaultGenresManager.PuzzleSkill,
                DefaultGenresManager.Rpg,
                DefaultGenresManager.Races,
                DefaultGenresManager.Sports,
                DefaultGenresManager.Strategy
            };
            context.Genres.AddRange(genres);
        }

        private static void EnsureSeedDataForPlatformTypeContext(OnlineGameContext context)
        {
            context.PlatformTypes.RemoveRange(context.PlatformTypes);
            context.SaveChanges();
            var platformTypes = new List<PlatformType>
            {
                PlatformTypeFactory.Browser,
                PlatformTypeFactory.Console,
                PlatformTypeFactory.Desktop,
                PlatformTypeFactory.Mobile
            };
            context.PlatformTypes.AddRange(platformTypes);
        }

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
            (DefaultSubGenresManager.Action(5),
                DefaultSubGenresManager.Races(4),
                DefaultSubGenresManager.Strategy(1)
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