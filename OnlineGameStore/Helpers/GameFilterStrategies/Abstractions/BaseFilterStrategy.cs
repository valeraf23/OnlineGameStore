using System.Collections.Generic;
using OnlineGameStore.Data.Dtos;

namespace OnlineGameStore.Api.Helpers.GameFilterStrategies.Abstractions
{
    public abstract class BaseFilterStrategy : IGameResourceFilter
    {
        private bool IsValid() => !string.IsNullOrEmpty(Filter);

        public IEnumerable<GameModel> ApplyFilter(IEnumerable<GameModel> models) => IsValid() ?Do(models) : models;

        public abstract string Filter { get;}
        public abstract IEnumerable<GameModel> Do(IEnumerable<GameModel> models);

    }
}