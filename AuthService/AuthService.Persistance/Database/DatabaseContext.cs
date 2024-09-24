using AuthService.Persistance.Configurations;
using AuthService.Persistance.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Persistance.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<RoleEntity> Roles { get; set; } = default!;
        public DbSet<UserEntity> Users { get; set; } = default!;
        public DbSet<CredentialsEntity> Credentials { get; set; } = default!;

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new CredentialsConfiguration());
        }
    }
}
