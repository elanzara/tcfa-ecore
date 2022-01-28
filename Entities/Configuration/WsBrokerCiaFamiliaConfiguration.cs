using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities.Configuration
{
    public class WsBrokerCiaFamiliaConfiguration : IEntityTypeConfiguration<WsBrokerCiaFamilia>
    {
        public void Configure(EntityTypeBuilder<WsBrokerCiaFamilia> builder)
        {
            builder.ToTable("ws_broker_cia_familia");

            builder.Property(e => e.CodigoTCFA).HasColumnName("CodigoTCFA");
            builder.Property(e => e.CompaniaID).HasColumnName("CompaniaID");
            builder.Property(e => e.FamiliaID).HasColumnName("FamiliaID");

            builder.Property(e => e.Cobertura)
                .HasColumnName("Cobertura")
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.Activo).HasColumnName("Activo");
            builder.Property(e => e.AceptaPagoEfectivo).HasColumnName("AceptaPagoEfectivo");

        }
    }
}
