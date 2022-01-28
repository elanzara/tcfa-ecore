using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities.Configuration
{
    public class WsBrokerCompaniaConfiguration : IEntityTypeConfiguration<WsBrokerCompania>
    {
        public void Configure(EntityTypeBuilder<WsBrokerCompania> builder)
        {
            builder.ToTable("ws_broker_compania");

            builder.Property(e => e.ID).HasColumnName("ID");

            builder.Property(e => e.codigo)
                .HasColumnName("Codigo")
                .HasMaxLength(10)
                .IsUnicode(false);
            builder.Property(e => e.nombre)
                .HasColumnName("Nombre")
                .HasMaxLength(50)
                .IsUnicode(false);

        }
    }
}
