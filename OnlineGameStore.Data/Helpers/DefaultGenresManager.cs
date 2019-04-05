using OnlineGameStore.Domain.Entities;

namespace OnlineGameStore.Data.Helpers
{
    public static class DefaultGenresManager
    {
        public static Genre Strategy =>
            new Genre
            {
                Id = GuidsManager.Get[1],
                Name = "Strategy"
            };

        public static Genre Rpg =>
            new Genre
            {
                Id = GuidsManager.Get[2],
                Name = "RPG"
            };

        public static Genre Sports =>
            new Genre
            {
                Id = GuidsManager.Get[3],
                Name = "Sports"
            };

        public static Genre Races =>
            new Genre
            {
                Id = GuidsManager.Get[4],
                Name = "Races",
            };

        public static Genre Action =>
            new Genre
            {
                Id = GuidsManager.Get[5],
                Name = "Action",
            };

        public static Genre Adventure =>
            new Genre
            {
                Id = GuidsManager.Get[6],
                Name = "Adventure"
            };

        public static Genre PuzzleSkill =>
            new Genre
            {
                Id = GuidsManager.Get[7],
                Name = "PuzzleSkill"
            };

        public static Genre Misc =>
            new Genre
            {
                Id = GuidsManager.Get[8],
                Name = "Misc"
            };
    }
}