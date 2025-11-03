
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TIVIT.CIPA.Api.Domain.Model;

namespace TIVIT.CIPA.Api.Domain.Repositories.Config
{
    internal class ElectionSiteConfig : IEntityTypeConfiguration<ElectionSite>
    {
        public void Configure(EntityTypeBuilder<ElectionSite> builder)
        {
            builder.ToTable("ElectionSite");
            builder.HasKey(up => new { up.ElectionId, up.SiteId});
        }
    }
}
