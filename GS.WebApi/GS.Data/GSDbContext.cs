using GS.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GS.Data
{
    public class GSDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserLogin> UserLogins { get; set; }

        public GSDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<UserLogin>()
                .HasKey(ul => new { ul.ProviderKey, ul.UserId });

            modelBuilder.Entity<UserLogin>()
                .HasOne(ul => ul.User)
                .WithMany(u => u.UserLogins)
                .HasForeignKey(ul => ul.UserId);
        }
    }
}
