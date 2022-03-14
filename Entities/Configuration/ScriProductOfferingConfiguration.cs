using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities.Configuration
{
    public class ScriProductOfferingConfiguration : IEntityTypeConfiguration<ScriProductOffering>
    {
        public void Configure(EntityTypeBuilder<ScriProductOffering> builder)
        {
            builder.ToTable("scri_ProductOffering");

            builder.Property(e => e.Id).HasColumnName("id");

            builder.Property(e => e.idVehicle).HasColumnName("idVehicle");

            builder.Property(e => e.Code)
            .HasColumnName("Code")
            .HasMaxLength(255)
            .IsUnicode(false);
            builder.Property(e => e.Description)
            .HasColumnName("Description")
            .HasMaxLength(255)
            .IsUnicode(false);

            builder.HasOne(d => d.idVehicleNavigation)
            .WithMany(p => p.ScriProductOffering)
            .HasForeignKey(d => d.idVehicle)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_scri_ProductOffering_Vehicle");
        }
    }
}
