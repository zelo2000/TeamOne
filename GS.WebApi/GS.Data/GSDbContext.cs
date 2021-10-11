using GS.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GS.Data
{
    public class GSDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public GSDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
