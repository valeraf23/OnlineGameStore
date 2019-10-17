using System;

namespace OnlineGameStore.Api.Filters.PublisherIdStrategies
{
    public interface IAssignPublisherIdStrategy
    {
        void Convert(object argument, Guid userIdAsGuid);
        Type ForType { get; }
    }
}