using Microsoft.EntityFrameworkCore;
using SocialMemin.Domain;

namespace SocialMemin.Persistence
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options) { }

        public DbSet<Activity> Activities { get; set; }
    }
}
