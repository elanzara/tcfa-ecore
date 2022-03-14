using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities.Configuration
{
    public class ScriRiskLocationConfiguration : IEntityTypeConfiguration<ScriRiskLocation>
    {
        public void Configure(EntityTypeBuilder<ScriRiskLocation> builder)
        {
            builder.ToTable("scri_RiskLocation");

            builder.Property(e => e.Id).HasColumnName("id");

            builder.Property(e => e.idVehicle).HasColumnName("idVehicle");

            builder.Property(e => e.City)
            .HasColumnName("City")
            .HasMaxLength(255)
            .IsUnicode(false);
            builder.Property(e => e.DisplayName)
            .HasColumnName("DisplayName")
            .HasMaxLength(255)
            .IsUnicode(false);
            builder.Property(e => e.PostalCode)
            .HasColumnName("PostalCode")
            .HasMaxLength(255)
            .IsUnicode(false);
            builder.Property(e => e.PublicID)
            .HasColumnName("PublicID")
            .HasMaxLength(255)
            .IsUnicode(false);
            builder.Property(e => e.Street)
            .HasColumnName("Street")
            .HasMaxLength(255)
            .IsUnicode(false);

            builder.HasOne(d => d.idVehicleNavigation)
            .WithMany(p => p.ScriRiskLocation)
            .HasForeignKey(d => d.idVehicle)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_scri_RiskLocation_Vehicle");
        }
    }
}
