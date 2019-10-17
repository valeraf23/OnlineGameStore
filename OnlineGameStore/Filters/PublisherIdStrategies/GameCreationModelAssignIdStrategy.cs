using System;
using OnlineGameStore.Data.Dtos;

namespace OnlineGameStore.Api.Filters.PublisherIdStrategies
{
    public class GameCreationModelAssignIdStrategy : BaseAssignPublisherIdStrategy<GameForCreationModel>
    {
        protected override void Assign(Guid userIdAsGuid)
        {
            Model.PublisherId = userIdAsGuid;
        }
    }
}