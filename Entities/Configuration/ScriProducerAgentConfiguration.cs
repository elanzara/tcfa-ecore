using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities.Configuration
{
    public class ScriProducerAgentConfiguration : IEntityTypeConfiguration<ScriProducerAgent>
    {
        public void Configure(EntityTypeBuilder<ScriProducerAgent> builder)
        {
            builder.ToTable("scri_ProducerAgent");

            builder.Property(e => e.Id).HasColumnName("id");

            builder.Property(e => e.idPolicy).HasColumnName("idPolicy");
            builder.Property(e => e.Code)
            .HasColumnName("Code")
            .HasMaxLength(255)
            .IsUnicode(false);
            builder.Property(e => e.OrganizationDisplayName)
            .HasColumnName("OrganizationDisplayName")
            .HasMaxLength(255)
            .IsUnicode(false);
            builder.Property(e => e.OrganizationPublicID)
            .HasColumnName("OrganizationPublicID")
            .HasMaxLength(255)
            .IsUnicode(false);
            builder.Property(e => e.PublicID)
            .HasColumnName("PublicID")
            .HasMaxLength(255)
            .IsUnicode(false);
            builder.Property(e => e.Selected)
            .HasColumnName("Selected")
            .HasMaxLength(255)
            .IsUnicode(false);

            builder.HasOne(d => d.idPolicyNavigation)
            .WithMany(p => p.ScriProducerAgent)
            .HasForeignKey(d => d.idPolicy)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_scri_ProducerAgent_Policy");
        }
    }
}
