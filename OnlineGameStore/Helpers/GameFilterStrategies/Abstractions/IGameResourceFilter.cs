using System.Collections.Generic;
using OnlineGameStore.Data.Dtos;

namespace OnlineGameStore.Api.Helpers.GameFilterStrategies.Abstractions
{
    public interface IGameResourceFilter
    {
        string Filter { get; }

        IEnumerable<GameModel> Do(IEnumerable<GameModel> models);
    }
}