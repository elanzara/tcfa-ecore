using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities.Configuration
{
    public class WsBrokerFamiliaConfiguration : IEntityTypeConfiguration<WsBrokerFamilia>
    {
        public void Configure(EntityTypeBuilder<WsBrokerFamilia> builder)
        {
            builder.ToTable("ws_broker_familia");

            builder.Property(e => e.familiaid).HasColumnName("FamiliaID");

            builder.Property(e => e.codigo)
                .HasColumnName("Codigo")
                .HasMaxLength(20)
                .IsUnicode(false);
            builder.Property(e => e.descripcion)
                .HasColumnName("Descripcion")
                .HasMaxLength(255)
                .IsUnicode(false);

        }
    }
}
