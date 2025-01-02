using AuthService.Core.Models;
using AuthService.Persistence.Database.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Persistence.Database
{
    public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<ExternalApplicationModel> Applications { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ExternalApplicationEntityConfiguration());
        }
    }
}
