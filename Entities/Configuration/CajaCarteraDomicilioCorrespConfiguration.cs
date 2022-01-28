using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities.Configuration
{
    public class CajaCarteraDomicilioCorrespConfiguration : IEntityTypeConfiguration<CajaCarteraDomicilioCorresp>
    {
        public void Configure(EntityTypeBuilder<CajaCarteraDomicilioCorresp> builder)
        {
            builder.ToTable("caja_cartera_domicilio_corresp");

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.IdCajaCarteraEnc).HasColumnName("id_caja_cartera_enc");

            builder.Property(e => e.Direccion)
                        .HasColumnName("direccion")
                        .HasMaxLength(255)
                        .IsUnicode(false);

            builder.Property(e => e.Localidad)
                    .HasColumnName("localidad")
                    .HasMaxLength(255)
                    .IsUnicode(false);

            builder.Property(e => e.CodigoPostal)
                    .HasColumnName("codigo_postal")
                    .HasMaxLength(255)
                    .IsUnicode(false);

            builder.Property(e => e.CodigoProvincia).HasColumnName("codigo_provincia");

            builder.Property(e => e.Telefono)
                    .HasColumnName("telefono")
                    .HasMaxLength(255)
                    .IsUnicode(false);

            builder.HasOne(d => d.IdCajaCarteraEncNavigation)
                    .WithMany(p => p.CajaCarteraDomicilioCorresp)
                    .HasForeignKey(d => d.IdCajaCarteraEnc)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_caja_cartera_domicilio_corresp");
        }
    }
}
