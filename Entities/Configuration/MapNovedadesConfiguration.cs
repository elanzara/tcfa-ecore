using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities.Configuration
{
    public class MapNovedadesConfiguration : IEntityTypeConfiguration<MapNovedades>
    {
        public void Configure(EntityTypeBuilder<MapNovedades> builder)
        {
            builder.ToTable("map_novedades");

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.compania).HasColumnName("compania");
            builder.Property(e => e.sector).HasColumnName("sector");
            builder.Property(e => e.ramo).HasColumnName("ramo");

            builder.Property(e => e.poliza)
                .HasColumnName("poliza")
                .HasMaxLength(13)
                .IsUnicode(false);

            builder.Property(e => e.productor).HasColumnName("productor");
            builder.Property(e => e.endoso).HasColumnName("endoso");

            builder.Property(e => e.fechaEmiSpto)
                .HasColumnName("fechaEmiSpto")
                .HasMaxLength(8)
                .IsUnicode(false);

            builder.Property(e => e.fechaEfecPol)
                .HasColumnName("fechaEfecPol")
                .HasMaxLength(8)
                .IsUnicode(false);

            builder.Property(e => e.fechaVctoPol)
                .HasColumnName("fechaVctoPol")
                .HasMaxLength(8)
                .IsUnicode(false);

            builder.Property(e => e.fechaEfecSpto)
                .HasColumnName("fechaEfecSpto")
                .HasMaxLength(8)
                .IsUnicode(false);

            builder.Property(e => e.fechaVctoSpto)
                .HasColumnName("fechaVctoSpto")
                .HasMaxLength(8)
                .IsUnicode(false);

            builder.Property(e => e.codEndoso).HasColumnName("codEndoso");
            builder.Property(e => e.subEndoso).HasColumnName("subEndoso");
            //builder.Property(e => e.motivo).HasColumnName("motivo");

            builder.Property(e => e.motivo)
                .HasColumnName("motivo")
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.tipoDocumentoTom)
                .HasColumnName("tipoDocumentoTom")
                .HasMaxLength(3)
                .IsUnicode(false);

            builder.Property(e => e.codigoDocumentoTom)
                .HasColumnName("codigoDocumentoTom")
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.planPago).HasColumnName("planPago");

            builder.Property(e => e.polizaAnterior)
                .HasColumnName("polizaAnterior")
                .HasMaxLength(13)
                .IsUnicode(false);

            builder.Property(e => e.polizaMadre)
                .HasColumnName("polizaMadre")
                .HasMaxLength(13)
                .IsUnicode(false);

            builder.Property(e => e.polizaSiguiente)
                .HasColumnName("polizaSiguiente")
                .HasMaxLength(13)
                .IsUnicode(false);

            builder.Property(e => e.facturacion)
                .HasColumnName("facturacion")
                .HasMaxLength(13)
                .IsUnicode(false);

            builder.Property(e => e.fechaRenov)
                .HasColumnName("fechaRenov")
                .HasMaxLength(8)
                .IsUnicode(false);

        }
    }
}