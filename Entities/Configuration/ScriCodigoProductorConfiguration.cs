using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities.Configuration
{
    public class ScriCodigoProductorConfiguration : IEntityTypeConfiguration<ScriCodigoProductor>
    {
        public void Configure(EntityTypeBuilder<ScriCodigoProductor> builder)
        {
            builder.ToTable("scri_codigo_productor");

            builder.Property(e => e.Id).HasColumnName("id");

            builder.Property(e => e.CodigoProductor)
                .HasColumnName("CodigoProductor")
                .HasMaxLength(255)
                .IsUnicode(false);

        }
    }
}
