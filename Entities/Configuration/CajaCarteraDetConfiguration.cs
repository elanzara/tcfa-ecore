using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities.Configuration
{
    public class CajaCarteraDetConfiguration : IEntityTypeConfiguration<CajaCarteraDet>
    {
        public void Configure(EntityTypeBuilder<CajaCarteraDet> builder)
        {
            builder.ToTable("caja_cartera_det");

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.IdCajaCarteraEnc).HasColumnName("id_caja_cartera_enc");
            builder.Property(e => e.Compania).HasColumnName("compania");
            builder.Property(e => e.Seccion).HasColumnName("seccion");
            builder.Property(e => e.Ramo).HasColumnName("ramo");
            builder.Property(e => e.Numero).HasColumnType("Decimal(20)").HasColumnName("numero");
            builder.Property(e => e.Referencia).HasColumnType("Decimal(20)").HasColumnName("referencia");
            //builder.Property(e => e.Referencia).HasColumnType("double").HasColumnName("referencia");

            builder.Property(e => e.Observacion)
                    .HasColumnName("observacion")
                    .HasMaxLength(255)
                    .IsUnicode(false);

            builder.Property(e => e.FechaVigencia)
                .HasColumnName("fecha_vigencia")
                .HasColumnType("datetime");

            builder.Property(e => e.FechaVencimiento)
                .HasColumnName("fecha_vencimiento")
                .HasColumnType("datetime");

            builder.Property(e => e.FechaEmision)
                .HasColumnName("fecha_emision")
                .HasColumnType("datetime");

            builder.Property(e => e.FormaCobro)
                    .HasColumnName("forma_cobro")
                    .HasMaxLength(255)
                    .IsUnicode(false);

            builder.Property(e => e.CBU)
                    .HasColumnName("cbu")
                    .HasMaxLength(255)
                    .IsUnicode(false);

            builder.Property(e => e.NumEnd).HasColumnType("Decimal(10)").HasColumnName("num_end");
            builder.Property(e => e.CodMon).HasColumnName("cod_mon");
            builder.Property(e => e.CodProd).HasColumnName("cod_prod");

            builder.Property(e => e.PolizaAnterior)
                    .HasColumnName("poliza_anterior")
                    .HasMaxLength(255)
                    .IsUnicode(false);

            builder.Property(e => e.Aglutinador)
                    .HasColumnName("aglutinador")
                    .HasMaxLength(255)
                    .IsUnicode(false);

            builder.Property(e => e.Solicitud)
                    .HasColumnName("solicitud")
                    .HasMaxLength(255)
                    .IsUnicode(false);

            builder.Property(e => e.Negocio)
                    .HasColumnName("negocio")
                    .HasMaxLength(255)
                    .IsUnicode(false);

            builder.HasOne(d => d.IdCajaCarteraEncNavigation)
                .WithMany(p => p.CajaCarteraDet)
                .HasForeignKey(d => d.IdCajaCarteraEnc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_caja_cartera_det");

        }
    }
}
