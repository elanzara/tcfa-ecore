using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities.Configuration
{
    public class MapDatosVariablesConfiguration : IEntityTypeConfiguration<MapDatosVariables>
    {
        public void Configure(EntityTypeBuilder<MapDatosVariables> builder)
        {
            builder.ToTable("map_datos_variables");

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.IdMapNovedades).HasColumnName("id_map_novedades");

            builder.Property(e => e.poliza)
                .HasColumnName("poliza")
                .HasMaxLength(13)
                .IsUnicode(false);

            builder.Property(e => e.endoso).HasColumnName("endoso");
            builder.Property(e => e.riesgo).HasColumnName("riesgo");
            builder.Property(e => e.campo)
                .HasColumnName("campo")
                .HasMaxLength(30)
                .IsUnicode(false);
            builder.Property(e => e.valor)
                .HasColumnName("valor")
                .HasMaxLength(80)
                .IsUnicode(false);
            builder.Property(e => e.descripcion)
                .HasColumnName("descripcion")
                .HasMaxLength(80)
                .IsUnicode(false);
            builder.Property(e => e.nivel).HasColumnName("nivel");
            builder.Property(e => e.secu).HasColumnName("secu");

            builder.HasOne(d => d.IdMapNovedadesNavigation)
            .WithMany(p => p.MapDatosVariables)
            .HasForeignKey(d => d.IdMapNovedades)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_datos_variables_novedades");
        }
    }
}