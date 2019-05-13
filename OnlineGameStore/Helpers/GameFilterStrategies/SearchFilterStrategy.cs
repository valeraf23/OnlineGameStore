using System;
using System.Collections.Generic;
using System.Linq;
using OnlineGameStore.Api.Helpers.GameFilterStrategies.Abstractions;
using OnlineGameStore.Data.Dtos;
using OnlineGameStore.Data.Helpers;

namespace OnlineGameStore.Api.Helpers.GameFilterStrategies
{
    public class SearchFilterStrategy : BaseFilterStrategy
    {
        private readonly GameResourceParameters _parameterValue;
        public override string Filter => _parameterValue.SearchQuery;

        public override IEnumerable<GameModel> Do(IEnumerable<GameModel> models) =>
            models.Where(a => a.Description.Contains(Filter, StringComparison.CurrentCultureIgnoreCase)
                              || a.Name.Contains(Filter, StringComparison.CurrentCultureIgnoreCase)
                              || a.PlatformTypes.Any(x =>
                                  x.Type.Contains(Filter, StringComparison.CurrentCultureIgnoreCase))
                              || a.Publisher.Name.Contains(Filter, StringComparison.CurrentCultureIgnoreCase));

        public SearchFilterStrategy(GameResourceParameters parameterValue) => _parameterValue = parameterValue;
    }
}