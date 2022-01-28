using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities.Configuration
{
    public class CajaCarteraClienteConfiguration : IEntityTypeConfiguration<CajaCarteraCliente>
    {
        public void Configure(EntityTypeBuilder<CajaCarteraCliente> builder)
        {
            builder.ToTable("caja_cartera_cliente");

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.IdCajaCarteraEnc).HasColumnName("id_caja_cartera_enc");

            builder.Property(e => e.TipoDocumento)
                    .HasColumnName("tipo_documento")
                    .HasMaxLength(255)
                    .IsUnicode(false);

            builder.Property(e => e.NroDocumento).HasColumnType("decimal(20)").HasColumnName("nro_documento");

            builder.Property(e => e.Apellido)
                    .HasColumnName("apellido")
                    .HasMaxLength(255)
                    .IsUnicode(false);

            builder.Property(e => e.Nombre)
                    .HasColumnName("nombre")
                    .HasMaxLength(255)
                    .IsUnicode(false);

            builder.Property(e => e.FechaNacimiento)
                .HasColumnName("fecha_nacimiento")
                .HasColumnType("datetime");

            builder.Property(e => e.CodIva).HasColumnName("cod_iva");

            builder.Property(e => e.Sexo)
                    .HasColumnName("sexo")
                    .HasMaxLength(255)
                    .IsUnicode(false);

            builder.Property(e => e.EstCivil)
                    .HasColumnName("est_civil")
                    .HasMaxLength(255)
                    .IsUnicode(false);

            builder.HasOne(d => d.IdCajaCarteraEncNavigation)
                    .WithMany(p => p.CajaCarteraCliente)
                    .HasForeignKey(d => d.IdCajaCarteraEnc)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_caja_cartera_cliente");
        }
    }
}
