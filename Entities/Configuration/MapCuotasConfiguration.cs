using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities.Configuration
{
    public class MapCuotasConfiguration : IEntityTypeConfiguration<MapCuotas>
    {
        public void Configure(EntityTypeBuilder<MapCuotas> builder)
        {
            builder.ToTable("map_cuotas");

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.IdMapNovedades).HasColumnName("id_map_novedades");

            builder.Property(e => e.poliza)
                .HasColumnName("poliza")
                .HasMaxLength(13)
                .IsUnicode(false);

            builder.Property(e => e.endoso).HasColumnName("endoso");
            builder.Property(e => e.numeroRecibo).HasColumnName("numeroRecibo");
            builder.Property(e => e.convenio)
                .HasColumnName("convenio")
                .HasMaxLength(8)
                .IsUnicode(false);
            builder.Property(e => e.vctoRecibo)
                .HasColumnName("vctoRecibo")
                .HasMaxLength(8)
                .IsUnicode(false);
            builder.Property(e => e.fecCobro)
                .HasColumnName("fecCobro")
                .HasMaxLength(8)
                .IsUnicode(false);
            builder.Property(e => e.agrpImpositivo).HasColumnName("agrpImpositivo");
            builder.Property(e => e.medioPago)
                .HasColumnName("medioPago")
                .HasMaxLength(2)
                .IsUnicode(false);
            builder.Property(e => e.moneda).HasColumnName("moneda");
            builder.Property(e => e.importe).HasColumnType("decimal(8, 2)").HasColumnName("importe");
            builder.Property(e => e.cobroAnticipado).HasColumnType("decimal(8, 2)").HasColumnName("cobroAnticipado");
            builder.Property(e => e.impComisiones).HasColumnType("decimal(8, 2)").HasColumnName("impComisiones");
            builder.Property(e => e.situacionRecibo)
                .HasColumnName("situacionRecibo")
                .HasMaxLength(2)
                .IsUnicode(false);

            builder.HasOne(d => d.IdMapNovedadesNavigation)
            .WithMany(p => p.MapCuotas)
            .HasForeignKey(d => d.IdMapNovedades)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_cuotas_novedades");
        }
    }
}
