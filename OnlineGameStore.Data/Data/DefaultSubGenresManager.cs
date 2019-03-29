using System.Collections.Generic;
using OnlineGameStore.Domain.Entities;

namespace OnlineGameStore.Data.Data
{
    public static class DefaultSubGenresManager
    {
        public static ICollection<Genre> Strategy(int parentId) => new List<Genre>
        {
            new Genre
            {
                Id = 9,
                Name = "RTS",
                ParentId = parentId
            },
            new Genre
            {
                Id = 10,
                Name = "TBS",
                ParentId = parentId
            }
        };

        public static ICollection<Genre> Races(int parentId) => new List<Genre>
        {
            new Genre
            {
                Id = 11,
                Name = "rally",
                ParentId = parentId
            },
            new Genre
            {
                Id = 12,
                Name = "arcade",
                ParentId = parentId
            },
            new Genre
            {
                Id = 13,
                Name = "formula",
                ParentId = parentId
            },
            new Genre
            {
                Id = 14,
                Name = "off-road",
                ParentId = parentId
            }
        };

        public static ICollection<Genre> Action(int parentId) => new List<Genre>
        {
            new Genre
            {
                Id = 15,
                Name = "FPS",
                ParentId = parentId
            },
            new Genre
            {
                Id = 16,
                Name = "TPS",
                ParentId = parentId
            },
            new Genre
            {
                Id = 17,
                Name = "Misc",
                ParentId = parentId
            }
        };
    }
}