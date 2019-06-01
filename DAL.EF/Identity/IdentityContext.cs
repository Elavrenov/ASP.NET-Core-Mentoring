using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.Identity
{
    public class IdentityContext : IdentityDbContext<NorthwindUser>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options)
            :base(options)
        {
            Database.EnsureCreated();
        }
    }
}