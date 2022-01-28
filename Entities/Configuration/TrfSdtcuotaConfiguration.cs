using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace eCore.Entities.Configuration
{
    public class TrfSdtcuotaConfiguration : IEntityTypeConfiguration<TrfSdtcuota>
    {
        public void Configure(EntityTypeBuilder<TrfSdtcuota> builder)
        {
            builder.ToTable("trf_sdtcuota");
            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.IdTrfNovedades).HasColumnName("id_trf_novedades");
            builder.Property(e => e.Estado)
            .HasColumnName("estado")
            .HasMaxLength(255)
            .IsUnicode(false);
            builder.Property(e => e.FechaCancelada)
            .HasColumnName("fecha_cancelada")
            .HasMaxLength(255)
            .IsUnicode(false);
            builder.Property(e => e.FechaVtoCuota)
            .HasColumnName("fecha_vto_cuota")
            .HasMaxLength(255)
            .IsUnicode(false);
            builder.Property(e => e.ImporteCuota).HasColumnType("decimal(12, 2)").HasColumnName("importe_cuota");
            builder.Property(e => e.NumeroCuota).HasColumnName("numero_cuota");

            builder.HasOne(d => d.IdTrfNovedadesNavigation)
            .WithMany(p => p.TrfSdtcuota)
            .HasForeignKey(d => d.IdTrfNovedades)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_sdtcuota_novedades");
        }
    }
}
