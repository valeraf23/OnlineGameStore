using System.Collections.Generic;
using OnlineGameStore.Data.Helpers;
using OnlineGameStore.Domain.Entities;

namespace OnlineGameStore.Tests.TestProject
{
    internal static class CommentsTestData
    {
        public static Comment FirstComment => new Comment
        {
            Id = GuidsManager.Get[1],
            Name = "First_Comment",
            Body = "Some_First_body",
            GameId = GuidsManager.Get[1],
            Answers = new List<Comment>
            {
                new Comment
                {
                    Id = GuidsManager.Get[2],
                    Name = "First_Answer_To_First_Comment",
                    ParentId = GuidsManager.Get[1],
                    GameId = GuidsManager.Get[1]
                },
                new Comment
                {
                    Id = GuidsManager.Get[3],
                    Name = "Second_Answer_To_First_Comment",
                    ParentId = GuidsManager.Get[1],
                    GameId = GuidsManager.Get[1]
                },
                new Comment
                {
                    Id = GuidsManager.Get[4],
                    Name = "Third_Answer_To_First_Comment",
                    ParentId = GuidsManager.Get[1],
                    GameId = GuidsManager.Get[1],
                    Answers = new List<Comment>
                    {
                        new Comment
                        {
                            Id = GuidsManager.Get[9],
                            Name = "First_Answer_To_Third_Answer",
                            ParentId = GuidsManager.Get[4],
                            GameId = GuidsManager.Get[1]
                        },
                        new Comment
                        {
                            Id = GuidsManager.Get[10],
                            Name = "Second_Answer_To_Third_Answer",
                            ParentId = GuidsManager.Get[4],
                            GameId = GuidsManager.Get[1],
                            Answers = new List<Comment>
                            {
                                new Comment
                                {
                                    Id = GuidsManager.Get[11],
                                    Name = "First_Answer_To_Second_Answer_To_Third_Answer",
                                    ParentId = GuidsManager.Get[10],
                                    GameId = GuidsManager.Get[1],
                                    Answers = new List<Comment>
                                    {
                                        new Comment
                                        {
                                            Id = GuidsManager.Get[12],
                                            Name = "First_Answer_First_Answer_To_Second_Answer_To_Third_Answer",
                                            ParentId = GuidsManager.Get[11],
                                            GameId = GuidsManager.Get[1]
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        };

        public static Comment SecondComment => new Comment
        {
            Id = GuidsManager.Get[5],
            Name = "Second_Comment",
            Body = "Some_First_body",
            GameId = GuidsManager.Get[1],
            Answers = new List<Comment>
            {
                new Comment
                {
                    Id = GuidsManager.Get[6],
                    Name = "First_Answer_To_Second_Comment",
                    ParentId = GuidsManager.Get[5],
                    GameId = GuidsManager.Get[1]
                },
                new Comment
                {
                    Id = GuidsManager.Get[7],
                    Name = "Second_Answer_To_Second_Comment",
                    ParentId = GuidsManager.Get[5],
                    GameId = GuidsManager.Get[1]
                },
                new Comment
                {
                    Id = GuidsManager.Get[8],
                    Name = "Third_Answer_To_Second_Comment",
                    ParentId = GuidsManager.Get[5],
                    GameId = GuidsManager.Get[1]
                }
            }
        };
    }
}
