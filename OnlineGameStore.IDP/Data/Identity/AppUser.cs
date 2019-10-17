using Microsoft.AspNetCore.Identity;

namespace OnlineGameStore.IDP.Data.Identity
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
