

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities.Configuration
{
    public class ScriOfferingConfiguration : IEntityTypeConfiguration<ScriOffering>
    {
        public void Configure(EntityTypeBuilder<ScriOffering> builder)
        {
            builder.ToTable("scri_offering");

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.Id).HasColumnName("IdScriListJobSummary");


            builder.Property(e => e.Code)
                        .HasColumnName("Code")
                        .HasMaxLength(255)
                        .IsUnicode(false);
            builder.Property(e => e.Description)
                        .HasColumnName("Description")
                        .HasMaxLength(255)
                        .IsUnicode(false);

            builder.HasOne(d => d.IdScriListJobSummaryNavigation)
            .WithMany(p => p.ScriOffering)
            .HasForeignKey(d => d.IdScriListJobSummary)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_offering_listjobsummary");

        }
    }
}
