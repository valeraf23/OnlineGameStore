using Microsoft.AspNetCore.Authorization;

namespace OnlineGameStore.Api.Authorization
{
    public class UserMustBeRequirementRole : IAuthorizationRequirement
    {

        public string Role { get; }

        public UserMustBeRequirementRole(string role)
        {
            Role = role;
        }
    }
}

