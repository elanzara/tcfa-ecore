using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities.Configuration
{
    public class ScriStateConfiguration : IEntityTypeConfiguration<ScriState>
    {
        public void Configure(EntityTypeBuilder<ScriState> builder)
        {
            builder.ToTable("scri_State");

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
            .WithMany(p => p.ScriState)
            .HasForeignKey(d => d.idAddress)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_scri_State_Address");
        }
    }
}
