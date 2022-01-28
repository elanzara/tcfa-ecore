using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities.Configuration
{
    public class TrfVehiculoDatosConfiguration : IEntityTypeConfiguration<TrfVehiculoDatos>
    {
        public void Configure(EntityTypeBuilder<TrfVehiculoDatos> builder)
        {
            builder.ToTable("trf_vehiculo_datos");
            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.IdTrfNovedades).HasColumnName("id_trf_novedades");
            builder.Property(e => e.Anio).HasColumnName("anio");
            builder.Property(e => e.CeroKm).HasColumnName("ceroKm");
            builder.Property(e => e.Chasis)
            .HasColumnName("chasis")
            .HasMaxLength(255)
            .IsUnicode(false);
            builder.Property(e => e.Cobertura)
            .HasColumnName("cobertura")
            .HasMaxLength(255)
            .IsUnicode(false);
            builder.Property(e => e.Dominio)
            .HasColumnName("dominio")
            .HasMaxLength(255)
            .IsUnicode(false);
            builder.Property(e => e.Marca)
            .HasColumnName("marca")
            .HasMaxLength(255)
            .IsUnicode(false);
            builder.Property(e => e.MarcaIA).HasColumnName("marcaia");
            builder.Property(e => e.Modelo)
            .HasColumnName("modelo")
            .HasMaxLength(255)
            .IsUnicode(false);
            builder.Property(e => e.ModeloIA).HasColumnName("modeloia");
            builder.Property(e => e.Motor)
            .HasColumnName("motor")
            .HasMaxLength(255)
            .IsUnicode(false);
            builder.Property(e => e.Origen)
            .HasColumnName("origen")
            .HasMaxLength(255)
            .IsUnicode(false);
            builder.Property(e => e.SubModelo)
            .HasColumnName("sub_modelo")
            .HasMaxLength(255)
            .IsUnicode(false);
            builder.Property(e => e.SumaAsegurada).HasColumnType("decimal(12, 2)").HasColumnName("suma_asegurada");
            builder.Property(e => e.Tipo)
            .HasColumnName("tipo")
            .HasMaxLength(255)
            .IsUnicode(false);
            builder.Property(e => e.TipoN).HasColumnName("tipon");
            builder.Property(e => e.Uso)
            .HasColumnName("uso")
            .HasMaxLength(255)
            .IsUnicode(false);
            builder.Property(e => e.UsoN).HasColumnName("uson");

            builder.HasOne(d => d.IdTrfNovedadesNavigation)
            .WithMany(p => p.TrfVehiculoDatos)
            .HasForeignKey(d => d.IdTrfNovedades)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_trf_vehiculo_datos");

        }
    }
}
