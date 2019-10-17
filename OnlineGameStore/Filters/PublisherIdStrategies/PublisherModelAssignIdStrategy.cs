using System;
using OnlineGameStore.Data.Dtos;

namespace OnlineGameStore.Api.Filters.PublisherIdStrategies
{
    public class PublisherModelAssignIdStrategy : BaseAssignPublisherIdStrategy<PublisherModel>
    {
        protected override void Assign(Guid userIdAsGuid)
        {
            Model.Id = userIdAsGuid;
        }
    }
}