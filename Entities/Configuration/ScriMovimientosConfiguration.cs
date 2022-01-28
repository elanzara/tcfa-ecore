using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities.Configuration
{
    public class ScriMovimientosConfiguration : IEntityTypeConfiguration<ScriMovimientos>
    {
        public void Configure(EntityTypeBuilder<ScriMovimientos> builder)
        {
            builder.ToTable("scri_movimientos");

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.HasError).HasColumnName("HasError");
            builder.Property(e => e.HasWarning).HasColumnName("HasWarning");
            builder.Property(e => e.HasInformation).HasColumnName("HasInformation");

        }
    }
}