using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities.Configuration
{
    public class SomDetalleCoberturasConfiguration : IEntityTypeConfiguration<SomDetalleCoberturas>
    {
        public void Configure(EntityTypeBuilder<SomDetalleCoberturas> builder)
        {
            builder.ToTable("Som_DetalleCoberturas");

            builder.Property(e => e.Id).HasColumnName("Id");
            builder.Property(e => e.CodigoTCFA).HasColumnName("CodigoTCFA");
            builder.Property(e => e.CompaniaID).HasColumnName("CompaniaID");
            builder.Property(e => e.Cobertura)
                .HasColumnName("Cobertura")
                .HasMaxLength(20)
                .IsUnicode(false);
            builder.Property(e => e.Nombre)
                .HasColumnName("Nombre")
                .HasMaxLength(2000)
                .IsUnicode(false);
        }
    }
}
