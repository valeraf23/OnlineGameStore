using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;

namespace OnlineGameStore.Api.Services
{
    public class UserInfoService : IUserInfoService
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string [] Roles { get; set; }

        public UserInfoService(IHttpContextAccessor httpContextAccessor)
        {
            var httpContextAccessor1 = httpContextAccessor
                                       ?? throw new ArgumentNullException(nameof(httpContextAccessor));

            var currentContext = httpContextAccessor1.HttpContext;
            if (currentContext == null || !currentContext.User.Identity.IsAuthenticated)
            {
                return;
            }

            UserId = currentContext
                .User.FindFirstValue(ClaimTypes.NameIdentifier);
            Roles = currentContext.User.FindAll(ClaimTypes.Role).Select(x=>x.Value).ToArray();
            Name = currentContext.User
                .Claims.FirstOrDefault(c => c.Type == "name")?.Value;
        }
    }
}

