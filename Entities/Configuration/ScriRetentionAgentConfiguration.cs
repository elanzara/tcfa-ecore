using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities.Configuration
{
    public class ScriRetentionAgentConfiguration : IEntityTypeConfiguration<ScriRetentionAgent>
    {
        public void Configure(EntityTypeBuilder<ScriRetentionAgent> builder)
        {
            builder.ToTable("scri_RetentionAgent");

            builder.Property(e => e.Id).HasColumnName("id");

            builder.Property(e => e.idTaxStatus).HasColumnName("idTaxStatus");

            builder.Property(e => e.Code)
            .HasColumnName("Code")
            .HasMaxLength(255)
            .IsUnicode(false);

            builder.Property(e => e.Description)
            .HasColumnName("Description")
            .HasMaxLength(255)
            .IsUnicode(false);

            builder.HasOne(d => d.idTaxStatusNavigation)
            .WithMany(p => p.ScriRetentionAgent)
            .HasForeignKey(d => d.idTaxStatus)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("[fk_scri_RetentionAgent_TaxStatus]");
        }
    }
}
