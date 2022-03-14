using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities.Configuration
{
    public class ScriUsageConfiguration : IEntityTypeConfiguration<ScriUsage>
    {
        public void Configure(EntityTypeBuilder<ScriUsage> builder)
        {
            builder.ToTable("scri_Usage");

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
            .WithMany(p => p.ScriUsage)
            .HasForeignKey(d => d.idVehicle)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_scri_Usage_Vehicle");
        }
    }
}
