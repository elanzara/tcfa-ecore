using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities.Configuration
{
    public class ScriAutomaticAdjustConfiguration : IEntityTypeConfiguration<ScriAutomaticAdjust>
    {
        public void Configure(EntityTypeBuilder<ScriAutomaticAdjust> builder)
        {
            builder.ToTable("scri_AutomaticAdjust");

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
            .WithMany(p => p.ScriAutomaticAdjust)
            .HasForeignKey(d => d.idVehicle)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_scri_AutomaticAdjust_Vehicle");
        }
    }
}
