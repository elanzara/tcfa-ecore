using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities.Configuration
{
    public class ScriContactConfiguration : IEntityTypeConfiguration<ScriContact>
    {
        public void Configure(EntityTypeBuilder<ScriContact> builder)
        {
            builder.ToTable("scri_Contact");

            builder.Property(e => e.Id).HasColumnName("id");

            builder.Property(e => e.idPolicy).HasColumnName("idPolicy");

            builder.Property(e => e.Activitystartdate)
            .HasColumnName("Activitystartdate")
            .HasColumnType("datetime");

            builder.Property(e => e.CUIL)
            .HasColumnName("CUIL")
            .HasMaxLength(255)
            .IsUnicode(false);

            builder.Property(e => e.DateOfBirth)
            .HasColumnName("DateOfBirth")
            .HasColumnType("datetime");

            builder.Property(e => e.EmailAddress1)
            .HasColumnName("EmailAddress1")
            .HasMaxLength(255)
            .IsUnicode(false);

            builder.Property(e => e.FirstName)
            .HasColumnName("FirstName")
            .HasMaxLength(255)
            .IsUnicode(false);

            builder.Property(e => e.InsuredNumberFormated)
            .HasColumnName("InsuredNumberFormated")
            .HasMaxLength(255)
            .IsUnicode(false);

            builder.Property(e => e.LastName)
            .HasColumnName("LastName")
            .HasMaxLength(255)
            .IsUnicode(false);

            builder.Property(e => e.Name)
            .HasColumnName("Name")
            .HasMaxLength(255)
            .IsUnicode(false);

            builder.Property(e => e.PEP)
            .HasColumnName("PEP")
            .HasMaxLength(255)
            .IsUnicode(false);

            builder.Property(e => e.PrimaryNamedInsured)
            .HasColumnName("PrimaryNamedInsured")
            .HasMaxLength(255)
            .IsUnicode(false);

            builder.Property(e => e.PublicID)
            .HasColumnName("PublicID")
            .HasMaxLength(255)
            .IsUnicode(false);

            builder.Property(e => e.Resident)
            .HasColumnName("Resident")
            .HasMaxLength(255)
            .IsUnicode(false);

            builder.Property(e => e.TaxID)
            .HasColumnName("TaxID")
            .HasMaxLength(255)
            .IsUnicode(false);

            builder.Property(e => e.UIFFormSubmitted)
            .HasColumnName("UIFFormSubmitted")
            .HasMaxLength(255)
            .IsUnicode(false);

            builder.HasOne(d => d.idPolicyNavigation)
            .WithMany(p => p.ScriContact)
            .HasForeignKey(d => d.idPolicy)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_scri_Contact_Policy");
        }
    }
}
