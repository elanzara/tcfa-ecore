using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities.Configuration
{
    public class WsBrokerCiaCoberturaDetalleConfiguration : IEntityTypeConfiguration<WsBrokerCiaCoberturaDetalle>
    {
        public void Configure(EntityTypeBuilder<WsBrokerCiaCoberturaDetalle> builder)
        {
            builder.ToTable("ws_broker_cia_cobertura_detalle");

            builder.Property(e => e.CompaniaID).HasColumnName("CompaniaID");

            builder.Property(e => e.CoberturaID)
                .HasColumnName("CoberturaID")
                .HasMaxLength(10)
                .IsUnicode(false);
            builder.Property(e => e.Detalle)
                .HasColumnName("Detalle")
                .HasMaxLength(2000)
                .IsUnicode(false);

        }
    }
}
