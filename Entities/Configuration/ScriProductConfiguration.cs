

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities.Configuration
{
    public class ScriProductConfiguration : IEntityTypeConfiguration<ScriProduct>
    {
        public void Configure(EntityTypeBuilder<ScriProduct> builder)
        {
            builder.ToTable("scri_product");

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
            .WithMany(p => p.ScriProduct)
            .HasForeignKey(d => d.IdScriListJobSummary)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_product_listjobsummary");

        }
    }
}
