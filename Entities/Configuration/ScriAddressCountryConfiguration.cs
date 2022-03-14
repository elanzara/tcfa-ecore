using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities.Configuration
{
    public class ScriAddressCountryConfiguration : IEntityTypeConfiguration<ScriAddressCountry>
    {
        public void Configure(EntityTypeBuilder<ScriAddressCountry> builder)
        {
            builder.ToTable("scri_AddressCountry");

            builder.Property(e => e.Id).HasColumnName("id");

            builder.Property(e => e.idAddress).HasColumnName("idAddress");

            builder.Property(e => e.Code)
            .HasColumnName("Code")
            .HasMaxLength(255)
            .IsUnicode(false);

            builder.Property(e => e.Description)
            .HasColumnName("Description")
            .HasMaxLength(255)
            .IsUnicode(false);

            builder.HasOne(d => d.idAddressNavigation)
            .WithMany(p => p.ScriAddressCountry)
            .HasForeignKey(d => d.idAddress)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_scri_AddressCountry_Address");
        }
    }
}
