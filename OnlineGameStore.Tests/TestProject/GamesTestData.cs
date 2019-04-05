using System;
using System.Collections.Generic;
using OnlineGameStore.Data.Helpers;
using OnlineGameStore.Domain.Entities;

namespace OnlineGameStore.Tests.TestProject
{

    internal static class GamesTestData
    {

        public static Game FourthGame => new Game
        {
            Id = GuidsManager.Get[4],
            Name = "FourthGame",
            Description = "FourthGame_Description",
            Publisher = new Publisher
            {
                Name = "VF1"
            },
            GamePlatformType = new List<GamePlatformType>
            {
                new GamePlatformType
                {
                    PlatformType = new PlatformType
                    {
                        Type = "test_PlatformType_ThirdGame"
                    }
                }
            }
        };

        public static Game ThirdGame => new Game
        {
            Id = GuidsManager.Get[3],
            Name = "ThirdGame",
            Description = "ThirdGame_Description",
            Publisher = new Publisher
            {
                Name = "VF1"
            },
            GamePlatformType = new List<GamePlatformType>
            {
                new GamePlatformType
                {
                    PlatformType = new PlatformType
                    {
                        Type = "test_PlatformType_ThirdGame"
                    }
                }
            }
        };

        public static Game SecondGame => new Game
        {
            Name = "SecondGame",
            Description = "SecondGame_Description",
            Publisher = new Publisher
            {
                Name = "VF1"
            },
            GamePlatformType = new List<GamePlatformType>
            {
                new GamePlatformType
                {
                    PlatformType = new PlatformType
                    {
                        Type = "test_PlatformType_SecondGame"
                    }
                }
            }
        };

        public static Game FirstGame => new Game
        {
            Id = GuidsManager.Get[1],
            Name = "Test",
            Description = "Test_Description",
            GameGenre = new List<GameGenre>
            {
                new GameGenre
                {
                    GameId = GuidsManager.Get[1],
                    Genre = new Genre
                    {
                        Id = GuidsManager.Get[3],
                        Name = "test_Genre",
                        SubGenres = new List<Genre>
                        {
                            new Genre
                            {
                                Id = GuidsManager.Get[4],
                                Name = "Sub-Genre",
                                ParentId = GuidsManager.Get[3]
                            }
                        }
                    }
                }
            },
            Publisher = new Publisher
            {
                Name = "VF"
            },
            GamePlatformType = new List<GamePlatformType>
            {
                new GamePlatformType
                {
                    PlatformType = new PlatformType
                    {
                        Id =  GuidsManager.Get[1],
                        Type = "test_PlatformType"
                    }
                }
            },
            Comments = new List<Comment>
            {
                CommentsTestData.FirstComment, CommentsTestData.SecondComment
            }
        };
    }
}