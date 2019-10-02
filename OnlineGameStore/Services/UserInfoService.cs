using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace OnlineGameStore.Api.Services
{

    public class UserInfoService : IUserInfoService
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }

        public UserInfoService(IHttpContextAccessor httpContextAccessor)
        {
            var httpContextAccessor1 = httpContextAccessor
                                                        ?? throw new ArgumentNullException(nameof(httpContextAccessor));

            var currentContext = httpContextAccessor1.HttpContext;
            if (currentContext == null || !currentContext.User.Identity.IsAuthenticated)
            {
                return;
            }


            var tt1 = currentContext
                .User.FindFirstValue(ClaimTypes.NameIdentifier);

            var tt  = currentContext
                .User.FindFirstValue(ClaimTypes.Name);

            var tt2 = currentContext.User.FindFirstValue(ClaimTypes.Role);

            UserId = currentContext
                .User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;

            Name = currentContext.User
                .Claims.FirstOrDefault(c => c.Type == "name")?.Value;

            Role = currentContext
                .User.Claims.FirstOrDefault(c => c.Type == "role")?.Value;
        }
    }
}
