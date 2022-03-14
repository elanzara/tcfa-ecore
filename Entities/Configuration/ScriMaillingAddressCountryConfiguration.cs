using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities.Configuration
{
    public class ScriMaillingAddressCountryConfiguration : IEntityTypeConfiguration<ScriMaillingAddressCountry>
    {
        public void Configure(EntityTypeBuilder<ScriMaillingAddressCountry> builder)
        {
            builder.ToTable("scri_MaillingAddressCountry");

            builder.Property(e => e.Id).HasColumnName("id");

            builder.Property(e => e.idMaillingAddress).HasColumnName("idMaillingAddress");

            builder.Property(e => e.Code)
            .HasColumnName("Code")
            .HasMaxLength(255)
            .IsUnicode(false);

            builder.Property(e => e.Description)
            .HasColumnName("Description")
            .HasMaxLength(255)
            .IsUnicode(false);

            builder.HasOne(d => d.idMaillingAddressNavigation)
            .WithMany(p => p.ScriMaillingAddressCountry)
            .HasForeignKey(d => d.idMaillingAddress)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_scri_MaillingAddressCountry_MaillingAddress");
        }
    }
}
