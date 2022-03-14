using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities.Configuration
{
    public class ScriEnrollementStatusConfiguration : IEntityTypeConfiguration<ScriEnrollementStatus>
    {
        public void Configure(EntityTypeBuilder<ScriEnrollementStatus> builder)
        {
            builder.ToTable("scri_EnrollementStatus");

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
            .WithMany(p => p.ScriEnrollementStatus)
            .HasForeignKey(d => d.idTaxStatus)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_scri_EnrollementStatus_TaxStatus");
        }
    }
}
