using OnlineGameStore.Api.Services;
using OnlineGameStore.Data.Dtos;

namespace OnlineGameStore.Api.Filters
{
    public class AssignPublisherIdAttribute : BaseAssignModelAttribute
    {
        public AssignPublisherIdAttribute(IUserInfoService userInfoService) : base(userInfoService)
        {
            Models = new[] { typeof(PublisherModel) };
        }
    }
}

