using AuthService.Persistance.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthService.Persistance.Configurations
{
    public class CredentialsConfiguration : IEntityTypeConfiguration<CredentialsEntity>
    {
        public void Configure(EntityTypeBuilder<CredentialsEntity> builder)
        {
            throw new NotImplementedException();
        }
    }
}
