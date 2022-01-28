using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities.Configuration
{
    public class ReporteCoberturaConfiguration : IEntityTypeConfiguration<ReporteCobertura>
    {
        public void Configure(EntityTypeBuilder<ReporteCobertura> builder)
        {
            //builder.ToTable("ws_broker_familia");

            builder.Property(e => e.CompaniaID).HasColumnName("CompaniaID");

            builder.Property(e => e.Compania)
                .HasColumnName("Compania")
                .HasMaxLength(20)
                .IsUnicode(false);
            builder.Property(e => e.CoberturaID)
                .HasColumnName("CoberturaID")
                .HasMaxLength(255)
                .IsUnicode(false);
            builder.Property(e => e.Detalle)
            .HasColumnName("Detalle")
            .HasMaxLength(2000)
            .IsUnicode(false);

            builder.Property(e => e.FamiliaID).HasColumnName("FamiliaID");
            builder.Property(e => e.Descripcion)
            .HasColumnName("Descripcion")
            .HasMaxLength(255)
            .IsUnicode(false);

            builder.Property(e => e.Activo).HasColumnName("Activo");
            builder.Property(e => e.AceptaPagoEfectivo).HasColumnName("AceptaPagoEfectivo");
            builder.Property(e => e.Telesale)
            .HasColumnName("Telesale")
            .HasMaxLength(255)
            .IsUnicode(false);
        }
    }
}
