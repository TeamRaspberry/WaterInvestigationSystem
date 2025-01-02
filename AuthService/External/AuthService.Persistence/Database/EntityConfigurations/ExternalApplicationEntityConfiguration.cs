using AuthService.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthService.Persistence.Database.EntityConfigurations
{
    public class ExternalApplicationEntityConfiguration : IEntityTypeConfiguration<ExternalApplicationModel>
    {
        public void Configure(EntityTypeBuilder<ExternalApplicationModel> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
