using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TIVIT.CIPA.Api.Domain.Model;

namespace TIVIT.CIPA.Api.Domain.Repositories.Config
{
    internal class VoterActionConfig : IEntityTypeConfiguration<VoterAction>
    {
        public void Configure(EntityTypeBuilder<VoterAction> builder)
        {
            builder.ToTable("VoterAction");

            // Define a chave primária composta
            builder.HasKey(va => new { va.VoterId, va.ActionId });

            // Configura os relacionamentos
            builder.HasOne(va => va.Voter)
                   .WithMany(v => v.VoterActions)
                   .HasForeignKey(va => va.VoterId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(va => va.Action)
                   .WithMany(a => a.VoterActions)
                   .HasForeignKey(va => va.ActionId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}