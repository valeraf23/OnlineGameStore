using System.Collections.Generic;
using OnlineGameStore.Domain.Entities;

namespace OnlineGameStore.Data.Data
{
    public static class DefaultSubGenresManager
    {
        public static ICollection<Genre> Strategy
        {
            get
            {
                var id = DefaultGenresManager.Strategy.Id;
                return new List<Genre>
                {
                    new Genre
                    {
                        Id = 9,
                        Name = "RTS",
                        ParentId = id
                    },
                    new Genre
                    {
                        Id = 10,
                        Name = "TBS",
                        ParentId = id
                    }
                };
            }
        }

        public static ICollection<Genre> Races
        {
            get
            {
                var id = DefaultGenresManager.Races.Id;
                return new List<Genre>
                {

                    new Genre
                    {
                        Id = 11,
                        Name = "rally",
                        ParentId = id
                    },
                    new Genre
                    {
                        Id = 12,
                        Name = "arcade",
                        ParentId = id
                    },
                    new Genre
                    {
                        Id = 13,
                        Name = "formula",
                        ParentId = id
                    },
                    new Genre
                    {
                        Id = 14,
                        Name = "off-road",
                        ParentId = id
                    }
                };
            }
        }

        public static ICollection<Genre> Action
        {
            get
            {
                var id = DefaultGenresManager.Action.Id;
                return new List<Genre>
                {
                    new Genre
                    {
                        Id = 15,
                        Name = "FPS",
                        ParentId = id
                    },
                    new Genre
                    {
                        Id = 16,
                        Name = "TPS",
                        ParentId = id
                    }
                };
            }

        }
    }
}