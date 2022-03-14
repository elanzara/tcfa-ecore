using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities.Configuration
{
    public class ScriColorConfiguration : IEntityTypeConfiguration<ScriColor>
    {
        public void Configure(EntityTypeBuilder<ScriColor> builder)
        {
            builder.ToTable("scri_Color");

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
            .WithMany(p => p.ScriColor)
            .HasForeignKey(d => d.idVehicle)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_scri_Color_Vehicle");
        }
    }
}
