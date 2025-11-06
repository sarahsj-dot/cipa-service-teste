using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TIVIT.CIPA.Api.Domain.Model;

namespace TIVIT.CIPA.Api.Domain.Repositories.Config
{
    internal class SiteConfig : IEntityTypeConfiguration<Site>
    {
        public void Configure(EntityTypeBuilder<Site> builder)
        {
            builder.ToTable("Site");
            builder.HasKey(r => r.Id);
            builder.HasOne(r => r.Company)
                   .WithMany(company => company.Sites)
                   .HasForeignKey(r => r.CompanyId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
