using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TIVIT.CIPA.Api.Domain.Model;

namespace TIVIT.CIPA.Api.Domain.Repositories.Config
{
    internal class ProfileActionConfig : IEntityTypeConfiguration<ProfileAction>
    {
        public void Configure(EntityTypeBuilder<ProfileAction> builder)
        {
            builder.ToTable("ProfileAction");
            builder.HasKey(ra => new { ra.ProfileId, ra.ActionId });
        }
    }
}
