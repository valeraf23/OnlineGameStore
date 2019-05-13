using System;
using System.Collections.Generic;
using System.Linq;
using OnlineGameStore.Api.Helpers.GameFilterStrategies.Abstractions;
using OnlineGameStore.Data.Dtos;
using OnlineGameStore.Data.Helpers;

namespace OnlineGameStore.Api.Helpers.GameFilterStrategies
{
    public class PlatformTypeFilterStrategy : BaseFilterStrategy
    {
        private readonly GameResourceParameters _parameterValue;
        public override string Filter => _parameterValue.PlatformType;

        public override IEnumerable<GameModel> Do(IEnumerable<GameModel> models) => models.Where(m =>
                m.PlatformTypes.Any(x => x.Type.Equals(Filter, StringComparison.CurrentCultureIgnoreCase)))
            .ToList();

        public PlatformTypeFilterStrategy(GameResourceParameters parameterValue) => _parameterValue = parameterValue;
    }
}