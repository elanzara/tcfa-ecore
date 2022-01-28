using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities.Configuration
{
    public class ScriTaxIdConfiguration : IEntityTypeConfiguration<ScriTaxId>
    {
        public void Configure(EntityTypeBuilder<ScriTaxId> builder)
        {
            builder.ToTable("scri_tax_id");

            builder.Property(e => e.Id).HasColumnName("id");

            builder.Property(e => e.TaxId)
                .HasColumnName("TaxId")
                .HasMaxLength(255)
                .IsUnicode(false);

        }
    }
}