using System;
using System.Collections.Generic;
using System.Linq;
using OnlineGameStore.Api.Helpers.GameFilterStrategies.Abstractions;
using OnlineGameStore.Data.Dtos;
using OnlineGameStore.Data.Helpers;

namespace OnlineGameStore.Api.Helpers.GameFilterStrategies
{
    public class GenreFilterStrategy : BaseFilterStrategy
    {
        public override IEnumerable<GameModel> Do(IEnumerable<GameModel> models) => models.Where(m =>
            m.Genres.SelectMany(GetGenresName)
                .Any(x => x.Equals(Filter, StringComparison.CurrentCultureIgnoreCase))).ToList();

        private static IList<string> GetGenresName(GenreModel models)
        {
            var list = new List<string> {models.Name.ToLowerInvariant()};
            foreach (var m in models.SubGenres) list.AddRange(GetGenresName(m));
            return list;
        }

        private readonly GameResourceParameters _parameterValue;
        public override string Filter => _parameterValue.Genre;

        public GenreFilterStrategy(GameResourceParameters parameterValue) => _parameterValue = parameterValue;

    }
}