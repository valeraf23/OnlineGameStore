using Microsoft.EntityFrameworkCore;

namespace OnlineGameStore.IDP.Data.Identity
{
    public class AppIdentityDbContextFactory : DesignTimeDbContextFactoryBase<ApplicationDbContext>
    {
        protected override ApplicationDbContext CreateNewInstance(DbContextOptions<ApplicationDbContext> options)
        {
            return new ApplicationDbContext(options);
        }
    }
}
