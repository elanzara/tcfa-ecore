using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities.Configuration
{
    public class ScriAddressConfiguration : IEntityTypeConfiguration<ScriAddress>
    {
        public void Configure(EntityTypeBuilder<ScriAddress> builder)
        {
            builder.ToTable("scri_Address");

            builder.Property(e => e.Id).HasColumnName("id");

            builder.Property(e => e.idContact).HasColumnName("idContact");

            builder.Property(e => e.updateLinkedAddresses)
            .HasColumnName("updateLinkedAddresses")
            .HasMaxLength(255)
            .IsUnicode(false);

            builder.Property(e => e.AddressLine1)
            .HasColumnName("AddressLine1")
            .HasMaxLength(255)
            .IsUnicode(false);

            builder.Property(e => e.AddressLine2)
            .HasColumnName("AddressLine2")
            .HasMaxLength(255)
            .IsUnicode(false);

            builder.Property(e => e.City)
            .HasColumnName("City")
            .HasMaxLength(255)
            .IsUnicode(false);

            builder.Property(e => e.County)
            .HasColumnName("County")
            .HasMaxLength(255)
            .IsUnicode(false);

            builder.Property(e => e.DisplayText)
            .HasColumnName("DisplayText")
            .HasMaxLength(255)
            .IsUnicode(false);

            builder.Property(e => e.PolicyAddress)
            .HasColumnName("PolicyAddress")
            .HasMaxLength(255)
            .IsUnicode(false);

            builder.Property(e => e.PostalCode)
            .HasColumnName("PostalCode")
            .HasMaxLength(255)
            .IsUnicode(false);

            builder.Property(e => e.PrimaryAddress)
            .HasColumnName("PrimaryAddress")
            .HasMaxLength(255)
            .IsUnicode(false);

            builder.Property(e => e.PublicID)
            .HasColumnName("PublicID")
            .HasMaxLength(255)
            .IsUnicode(false);

            builder.Property(e => e.StreetNumber)
            .HasColumnName("StreetNumber")
            .HasMaxLength(255)
            .IsUnicode(false);

            builder.HasOne(d => d.idContactNavigation)
            .WithMany(p => p.ScriAddress)
            .HasForeignKey(d => d.idContact)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_scri_Address_Contact");
        }
    }
}
