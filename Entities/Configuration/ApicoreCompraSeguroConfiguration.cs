
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities.Configuration
{
    public class ApicoreCompraSeguroConfiguration : IEntityTypeConfiguration<ApicoreCompraSeguro>
    {
        public void Configure(EntityTypeBuilder<ApicoreCompraSeguro> builder)
        {
            builder.ToTable("apicore_compra_seguro");

            builder.Property(e => e.id).HasColumnName("id");
            builder.Property(e => e.cobertura)
                .HasColumnName("cobertura")
                .HasMaxLength(255)
                .IsUnicode(false);
            builder.Property(e => e.compania)
                .HasColumnName("compania")
                .HasMaxLength(255)
                .IsUnicode(false);
            builder.Property(e => e.createdAt)
                .HasColumnName("createdAt")
                .HasColumnType("datetime");
            builder.Property(e => e.estado)
                .HasColumnName("estado")
                .HasMaxLength(255)
                .IsUnicode(false);
            builder.Property(e => e.externalCoberturaId)
                .HasColumnName("externalCoberturaId")
                .HasMaxLength(255)
                .IsUnicode(false);
            builder.Property(e => e.externalCotizacionId).HasColumnName("externalCotizacionId");
            builder.Property(e => e.nroPoliza)
                .HasColumnName("nroPoliza")
                .HasMaxLength(255)
                .IsUnicode(false);
            builder.Property(e => e.primaPoliza).HasColumnType("Decimal(20)").HasColumnName("primaPoliza");
            builder.Property(e => e.trackId)
                .HasColumnName("trackId")
                .HasMaxLength(255)
                .IsUnicode(false);
            builder.Property(e => e.updatedAt)
                .HasColumnName("updatedAt")
                .HasColumnType("datetime");
            builder.Property(e => e.persona_id).HasColumnName("persona_id");
            builder.Property(e => e.user_id).HasColumnName("user_id");
            builder.Property(e => e.medioPago_id).HasColumnName("medioPago_id");
            builder.Property(e => e.fechaFuturaCotizacion)
                .HasColumnName("fechaFuturaCotizacion")
                .HasColumnType("datetime");

        }
    }
}
