using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TIVIT.CIPA.Api.Domain.Model;

namespace TIVIT.CIPA.Api.Domain.Repositories.Config
{
    internal class ProfileConfig : IEntityTypeConfiguration<Profile>
    {
        public void Configure(EntityTypeBuilder<Profile> builder)
        {
            builder.ToTable("Profile");
            builder.HasKey(r => r.Id);
        }
    }
}
