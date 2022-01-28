using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities.Configuration
{
    public class MapCoberturasConfiguration : IEntityTypeConfiguration<MapCoberturas>
    {
        public void Configure(EntityTypeBuilder<MapCoberturas> builder)
        {
            builder.ToTable("map_coberturas");

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.IdMapNovedades).HasColumnName("id_map_novedades");
            builder.Property(e => e.poliza)
                .HasColumnName("poliza")
                .HasMaxLength(13)
                .IsUnicode(false);
            builder.Property(e => e.endoso).HasColumnName("endoso");
            builder.Property(e => e.riesgo).HasColumnName("riesgo");
            builder.Property(e => e.secu).HasColumnName("secu");
            builder.Property(e => e.cobertura).HasColumnName("cobertura");
            builder.Property(e => e.sumaAseg).HasColumnType("decimal(12, 2)").HasColumnName("sumaAseg");

            builder.HasOne(d => d.IdMapNovedadesNavigation)
            .WithMany(p => p.MapCoberturas)
            .HasForeignKey(d => d.IdMapNovedades)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_coberturas_novedades");
        }
    }
}
