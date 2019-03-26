using System.Collections.Generic;
using OnlineGameStore.Domain.Entities;

namespace OnlineGameStore.Data.Data
{
    public static class DefaultSubGenresFactories
    {
        public static ICollection<Genre> Strategy => new List<Genre>
        {
            new Genre
            {
                Id = 9,
                Name = "RTS",
                ParentId = 1
            },
            new Genre
            {
                Id = 10,
                Name = "TBS",
                ParentId = 1
            }
        };

        public static ICollection<Genre> Races => new List<Genre>
        {
            new Genre
            {
                Id = 11,
                Name = "rally",
                ParentId = 4
            },
            new Genre
            {
                Id = 12,
                Name = "arcade",
                ParentId = 4
            },
            new Genre
            {
                Id = 13,
                Name = "formula",
                ParentId = 4
            },
            new Genre
            {
                Id = 14,
                Name = "off-road",
                ParentId = 4
            }
        };

        public static ICollection<Genre> Action => new List<Genre>
        {
            new Genre
            {
                Id = 15,
                Name = "FPS",
                ParentId = 5
            },
            new Genre
            {
                Id = 16,
                Name = "TPS",
                ParentId = 5
            },
            new Genre
            {
                Id = 17,
                Name = "Misc",
                ParentId = 5
            }
        };
    }
}