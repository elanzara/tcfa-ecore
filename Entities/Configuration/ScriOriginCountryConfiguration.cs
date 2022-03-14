using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities.Configuration
{
    public class ScriOriginCountryConfiguration : IEntityTypeConfiguration<ScriOriginCountry>
    {
        public void Configure(EntityTypeBuilder<ScriOriginCountry> builder)
        {
            builder.ToTable("scri_OriginCountry");

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
            .WithMany(p => p.ScriOriginCountry)
            .HasForeignKey(d => d.idVehicle)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_scri_OriginCountry_Vehicle");
        }
    }
}
