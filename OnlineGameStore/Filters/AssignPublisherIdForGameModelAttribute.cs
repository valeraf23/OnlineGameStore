using OnlineGameStore.Api.Services;
using OnlineGameStore.Data.Dtos;

namespace OnlineGameStore.Api.Filters
{
    public class AssignPublisherIdForGameModelAttribute : BaseAssignModelAttribute
    {
        public AssignPublisherIdForGameModelAttribute(IUserInfoService userInfoService) : base(userInfoService)
        {
            Models = new[] {typeof(GameForCreationModel)};
        }
    }
}