using System;
using System.Collections.Generic;
using System.Linq;
using OnlineGameStore.Api.Helpers.GameFilterStrategies.Abstractions;
using OnlineGameStore.Data.Dtos;
using OnlineGameStore.Data.Helpers;

namespace OnlineGameStore.Api.Helpers.GameFilterStrategies
{
    public class PublisherFilterStrategy : BaseFilterStrategy
    {
        private readonly GameResourceParameters _parameterValue;
        public override string Filter => _parameterValue.Publisher;

        public override IEnumerable<GameModel> Do(IEnumerable<GameModel> models) => models.Where(m =>
            m.Publisher.Name.Equals(Filter, StringComparison.CurrentCultureIgnoreCase));

        public PublisherFilterStrategy(GameResourceParameters parameterValue) => _parameterValue = parameterValue;
    }
}