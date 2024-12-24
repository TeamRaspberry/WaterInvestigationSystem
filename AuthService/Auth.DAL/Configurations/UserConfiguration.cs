using Auth.Domain.Enity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.DAL.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(user => user.Id).ValueGeneratedOnAdd();
            builder.Property(user => user.Login).IsRequired().HasMaxLength(120);
            builder.Property(user => user.Password).IsRequired();


            builder.HasMany(user => user.Role).WithMany(role => role.Users)
                .UsingEntity<UserRole>
                (
                    r => r.HasOne<Role>().WithMany().HasForeignKey(x => x.RoleId),
                    u => u.HasOne<User>().WithMany().HasForeignKey(x => x.UserId)
                );



        }
    }
}