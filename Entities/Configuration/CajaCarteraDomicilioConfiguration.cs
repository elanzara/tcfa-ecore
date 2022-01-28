using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities.Configuration
{
    public class CajaCarteraDomicilioConfiguration : IEntityTypeConfiguration<CajaCarteraDomicilio>
    {
        public void Configure(EntityTypeBuilder<CajaCarteraDomicilio> builder)
        {
            builder.ToTable("caja_cartera_domicilio");

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

            builder.HasOne(d => d.IdCajaCarteraEncNavigation)
            .WithMany(p => p.CajaCarteraDomicilio)
            .HasForeignKey(d => d.IdCajaCarteraEnc)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_caja_cartera_domicilio");

        }
    }
}
