using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities.Configuration
{
    public class CajaCarteraAccesorioConfiguration : IEntityTypeConfiguration<CajaCarteraAccesorio>
    {
        public void Configure(EntityTypeBuilder<CajaCarteraAccesorio> builder)
        {
            builder.ToTable("caja_cartera_accesorio");

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.IdCajaCarteraEnc).HasColumnName("id_caja_cartera_enc");
            builder.Property(e => e.Codigo).HasColumnName("codigo");
            builder.Property(e => e.Valor).HasColumnName("valor");

            builder.HasOne(d => d.IdCajaCarteraEncNavigation)
                    .WithMany(p => p.CajaCarteraAccesorio)
                    .HasForeignKey(d => d.IdCajaCarteraEnc)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_caja_cartera_accesorio");
        }
    }
}
