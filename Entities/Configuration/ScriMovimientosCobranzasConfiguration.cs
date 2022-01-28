using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities.Configuration
{
    public class ScriMovimientosCobranzasConfiguration : IEntityTypeConfiguration<ScriMovimientosCobranzas>
    {
        public void Configure(EntityTypeBuilder<ScriMovimientosCobranzas> builder)
        {
            builder.ToTable("scri_movimientosCobranzas");

            builder.Property(e => e.id).HasColumnName("id");

            builder.Property(e => e.poliza)
            .HasColumnName("poliza")
            .HasMaxLength(200)
            .IsUnicode(false);

            builder.Property(e => e.Ext_ApplicationDate)
            .HasColumnName("Ext_ApplicationDate")
            .HasColumnType("datetime");

            builder.Property(e => e.PaymentAmount).HasColumnType("decimal(12, 2)").HasColumnName("PaymentAmount");

            builder.Property(e => e.ReversedDate)
            .HasColumnName("ReversedDate")
            .HasColumnType("datetime");
        }
    }
}
