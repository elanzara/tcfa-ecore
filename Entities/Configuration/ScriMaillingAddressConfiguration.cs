using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities.Configuration
{
    public class ScriMaillingAddressConfiguration : IEntityTypeConfiguration<ScriMaillingAddress>
    {
        public void Configure(EntityTypeBuilder<ScriMaillingAddress> builder)
        {
            builder.ToTable("scri_MaillingAddress");

            builder.Property(e => e.Id).HasColumnName("id");

            builder.Property(e => e.idPolicy).HasColumnName("idPolicy");

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

            builder.HasOne(d => d.idPolicyNavigation)
            .WithMany(p => p.ScriMaillingAddress)
            .HasForeignKey(d => d.idPolicy)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_scri_MaillingAddress_Policy");
        }
    }
}
