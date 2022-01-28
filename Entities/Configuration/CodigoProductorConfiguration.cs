using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities.Configuration
{
    public class CodigoProductorConfiguration : IEntityTypeConfiguration<CodigoProductor>
    {
        public void Configure(EntityTypeBuilder<CodigoProductor> builder)
        {
            builder.ToTable("codigo_productor");

            builder.Property(e => e.Id).HasColumnName("id");

            builder.Property(e => e.ID_ws_broker_compania).HasColumnName("ID_ws_broker_compania");

            builder.Property(e => e.Codigo)
            .HasColumnName("Codigo")
            .HasMaxLength(10)
            .IsUnicode(false);

            builder.Property(e => e.Nombre)
            .HasColumnName("Nombre")
            .HasMaxLength(50)
            .IsUnicode(false);

            builder.Property(e => e.codigo_productor)
            .HasColumnName("codigo_productor")
            .HasMaxLength(255)
            .IsUnicode(false);

            builder.Property(e => e.usuario)
            .HasColumnName("usuario")
            .HasMaxLength(255)
            .IsUnicode(false);

            builder.Property(e => e.clave)
            .HasColumnName("clave")
            .HasMaxLength(255)
            .IsUnicode(false);
        }
    }
}
