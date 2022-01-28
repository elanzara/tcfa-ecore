using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities.Configuration
{
    public class MapDatosRiesgoConfiguration : IEntityTypeConfiguration<MapDatosRiesgo>
    {
        public void Configure(EntityTypeBuilder<MapDatosRiesgo> builder)
        {
            builder.ToTable("map_datos_riesgo");

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.IdMapNovedades).HasColumnName("id_map_novedades");

            builder.Property(e => e.poliza)
                .HasColumnName("poliza")
                .HasMaxLength(13)
                .IsUnicode(false);

            builder.Property(e => e.endoso).HasColumnName("endoso");
            builder.Property(e => e.riesgo).HasColumnName("riesgo");
            builder.Property(e => e.nombreRiesgo)
                .HasColumnName("nombreRiesgo")
                .HasMaxLength(80)
                .IsUnicode(false);
            builder.Property(e => e.vigencia)
                .HasColumnName("vigencia")
                .HasMaxLength(8)
                .IsUnicode(false);
            builder.Property(e => e.vencimiento)
                .HasColumnName("vencimiento")
                .HasMaxLength(8)
                .IsUnicode(false);
            builder.Property(e => e.baja)
                .HasColumnName("baja")
                .HasMaxLength(1)
                .IsUnicode(false);

            builder.HasOne(d => d.IdMapNovedadesNavigation)
            .WithMany(p => p.MapDatosRiesgo)
            .HasForeignKey(d => d.IdMapNovedades)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_datos_riesgo_novedades");
        }
    }
}