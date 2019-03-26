using OnlineGameStore.Domain.Entities;

namespace OnlineGameStore.Data.Data
{
    public static class DefaultGenresFactories
    {
        public static Genre Strategy =>
            new Genre
            {
                Id = 1,
                Name = "Strategy",
                SubGenres = DefaultSubGenresFactories.Strategy
            };

        public static Genre Rpg =>
            new Genre
            {
                Id = 2,
                Name = "RPG"
            };

        public static Genre Sports =>
            new Genre
            {
                Id = 3,
                Name = "Sports"
            };
        public static Genre Races =>
            new Genre
            {
                Id = 4,
                Name = "Races",
                SubGenres = DefaultSubGenresFactories.Races
            };
        public static Genre Action =>
            new Genre
            {
                Id = 5,
                Name = "Action",
                SubGenres = DefaultSubGenresFactories.Action
            };
        public static Genre Adventure =>
            new Genre
            {
                Id = 6,
                Name = "Adventure"
            };
        public static Genre PuzzleSkill =>
            new Genre
            {
                Id = 7,
                Name = "PuzzleSkill"
            };
        public static Genre Misc =>
            new Genre
            {
                Id = 8,
                Name = "Misc"
            };
    }
}