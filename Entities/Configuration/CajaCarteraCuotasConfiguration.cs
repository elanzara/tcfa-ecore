using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities.Configuration
{
    public class CajaCarteraCuotasConfiguration : IEntityTypeConfiguration<CajaCarteraCuotas>
    {
        public void Configure(EntityTypeBuilder<CajaCarteraCuotas> builder)
        {
            builder.ToTable("caja_cartera_cuotas");

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.IdCajaCarteraEnc).HasColumnName("id_caja_cartera_enc");
            builder.Property(e => e.NumCuota).HasColumnName("num_cuota");

            builder.Property(e => e.FechaVto)
                .HasColumnName("fecha_vto")
                .HasColumnType("datetime");

            builder.Property(e => e.Situacion)
                    .HasColumnName("situacion")
                    .HasMaxLength(255)
                    .IsUnicode(false);

            builder.Property(e => e.Prima).HasColumnType("decimal(12, 2)").HasColumnName("prima");
            builder.Property(e => e.Comision).HasColumnType("decimal(12, 2)").HasColumnName("comision");
            builder.Property(e => e.Premio).HasColumnType("decimal(12, 2)").HasColumnName("premio");
            builder.Property(e => e.PorcInflacion).HasColumnType("decimal(12, 2)").HasColumnName("porc_inflacion");

            builder.HasOne(d => d.IdCajaCarteraEncNavigation)
                    .WithMany(p => p.CajaCarteraCuotas)
                    .HasForeignKey(d => d.IdCajaCarteraEnc)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_caja_cartera_cuotas");
        }
    }
}
