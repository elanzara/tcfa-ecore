using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities.Configuration
{
    public class MapCodigoConexionConfiguration : IEntityTypeConfiguration<MapCodigoConexion>
    {
        public void Configure(EntityTypeBuilder<MapCodigoConexion> builder)
        {
            builder.ToTable("map_codigo_conexion");

            builder.Property(e => e.Id).HasColumnName("id");

            builder.Property(e => e.codAgt)
                .HasColumnName("codAgt")
                .HasMaxLength(255)
                .IsUnicode(false);
            builder.Property(e => e.claveAcceso)
                .HasColumnName("claveAcceso")
                .HasMaxLength(255)
                .IsUnicode(false);
            builder.Property(e => e.claveProcedencia)
                .HasColumnName("claveProcedencia")
                .HasMaxLength(255)
                .IsUnicode(false);
        }
    }
}
