using Microsoft.EntityFrameworkCore;
using SocialMemin.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace SocialMemin.Persistence
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        public DataContext(DbContextOptions options) : base(options) { }

        public DbSet<Activity> Activities { get; set; }
    }
}
