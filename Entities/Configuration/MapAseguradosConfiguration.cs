using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities.Configuration
{
    public class MapAseguradosConfiguration : IEntityTypeConfiguration<MapAsegurados>
    {
        public void Configure(EntityTypeBuilder<MapAsegurados> builder)
        {
            builder.ToTable("map_asegurados");

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.IdMapNovedades).HasColumnName("id_map_novedades");

            builder.Property(e => e.poliza)
                .HasColumnName("poliza")
                .HasMaxLength(13)
                .IsUnicode(false);

            builder.Property(e => e.endoso).HasColumnName("endoso");
            builder.Property(e => e.riesgo).HasColumnName("riesgo");
            builder.Property(e => e.tipoBenef)
                .HasColumnName("tipoBenef")
                .HasMaxLength(2)
                .IsUnicode(false);
            builder.Property(e => e.tipoDoc)
                .HasColumnName("tipoDoc")
                .HasMaxLength(3)
                .IsUnicode(false);
            builder.Property(e => e.codDoc)
                .HasColumnName("codDoc")
                .HasMaxLength(20)
                .IsUnicode(false);
            builder.Property(e => e.asegurado)
                .HasColumnName("asegurado")
                .HasMaxLength(120)
                .IsUnicode(false);
            builder.Property(e => e.domicilio)
                .HasColumnName("domicilio")
                .HasMaxLength(120)
                .IsUnicode(false);
            builder.Property(e => e.localidad).HasColumnName("localidad");
            builder.Property(e => e.postal).HasColumnName("postal");
            builder.Property(e => e.provincia).HasColumnName("provincia");
            builder.Property(e => e.telefonoPais).HasColumnName("telefonoPais");
            builder.Property(e => e.telefonoZona).HasColumnName("telefonoZona");
            builder.Property(e => e.telefono).HasColumnName("telefono");
            builder.Property(e => e.domicilioCom)
                .HasColumnName("domicilioCom")
                .HasMaxLength(120)
                .IsUnicode(false);
            builder.Property(e => e.localidadCom).HasColumnName("localidadCom");
            builder.Property(e => e.postalCom).HasColumnName("postalCom");
            builder.Property(e => e.provinciaCom).HasColumnName("provinciaCom");
            builder.Property(e => e.nacimiento)
                .HasColumnName("nacimiento")
                .HasMaxLength(8)
                .IsUnicode(false);
            builder.Property(e => e.iva).HasColumnName("iva");
            builder.Property(e => e.mcaSexo)
                .HasColumnName("mcaSexo")
                .HasMaxLength(1)
                .IsUnicode(false);
            builder.Property(e => e.nomina)
                .HasColumnName("nomina")
                .HasMaxLength(1)
                .IsUnicode(false);
            builder.Property(e => e.baja)
                .HasColumnName("baja")
                .HasMaxLength(1)
                .IsUnicode(false);

            builder.HasOne(d => d.IdMapNovedadesNavigation)
            .WithMany(p => p.MapAsegurados)
            .HasForeignKey(d => d.IdMapNovedades)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_asegurados_novedades");
        }
    }
}