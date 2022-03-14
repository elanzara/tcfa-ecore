using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities.Configuration
{
    public class ScriTaxStatusConfiguration : IEntityTypeConfiguration<ScriTaxStatus>
    {
        public void Configure(EntityTypeBuilder<ScriTaxStatus> builder)
        {
            builder.ToTable("scri_TaxStatus");

            builder.Property(e => e.Id).HasColumnName("id");

            builder.Property(e => e.idContact).HasColumnName("idContact");

            builder.Property(e => e.PublicID)
            .HasColumnName("PublicID")
            .HasMaxLength(255)
            .IsUnicode(false);

            builder.Property(e => e.StatusValue)
            .HasColumnName("StatusValue")
            .HasMaxLength(255)
            .IsUnicode(false);

            builder.Property(e => e.TaxPercentage).HasColumnType("Decimal(5,2)").HasColumnName("TaxPercentage");

            builder.HasOne(d => d.idContactNavigation)
            .WithMany(p => p.ScriTaxStatus)
            .HasForeignKey(d => d.idContact)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_scri_TaxStatus_Contact");
        }
    }
}
