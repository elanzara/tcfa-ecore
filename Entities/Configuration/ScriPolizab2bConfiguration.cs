using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace eCore.Entities.Configuration
{
    public class ScriPolizab2bConfiguration : IEntityTypeConfiguration<ScriPolizab2b>
    {
        public void Configure(EntityTypeBuilder<ScriPolizab2b> builder)
        {
            builder.ToTable("scri_polizab2b");

            builder.Property(e => e.Id).HasColumnName("id");

            builder.Property(e => e.AlternativaComercial)
                .HasColumnName("AlternativaComercial")
                .HasMaxLength(5)
                .IsUnicode(false);

            builder.Property(e => e.Updated)
                .HasColumnName("Updated")
                .HasMaxLength(5)
                .IsUnicode(false);

            builder.Property(e => e.HasError)
                .HasColumnName("HasError")
                .HasMaxLength(5)
                .IsUnicode(false);

            builder.Property(e => e.HasWarning)
            .HasColumnName("HasWarning")
            .HasMaxLength(5)
            .IsUnicode(false);

            builder.Property(e => e.HasInformation)
            .HasColumnName("HasInformation")
            .HasMaxLength(5)
            .IsUnicode(false);

            builder.Property(e => e.Messages)
            .HasColumnName("Messages")
            .HasMaxLength(2000)
            .IsUnicode(false);
        }
    }
}
