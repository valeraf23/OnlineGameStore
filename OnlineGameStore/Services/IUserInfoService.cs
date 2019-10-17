namespace OnlineGameStore.Api.Services
{
    public interface IUserInfoService
    {
        string UserId { get; set; }
        string Name { get; set; }
        string[] Roles { get; set; }

    }
}