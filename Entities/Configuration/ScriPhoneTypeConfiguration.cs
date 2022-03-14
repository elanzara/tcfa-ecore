using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities.Configuration
{
    public class ScriPhoneTypeConfiguration : IEntityTypeConfiguration<ScriPhoneType>
    {
        public void Configure(EntityTypeBuilder<ScriPhoneType> builder)
        {
            builder.ToTable("scri_PhoneType");

            builder.Property(e => e.Id).HasColumnName("id");

            builder.Property(e => e.idPhone).HasColumnName("idPhone");

            builder.Property(e => e.Code)
            .HasColumnName("Code")
            .HasMaxLength(255)
            .IsUnicode(false);

            builder.Property(e => e.Description)
            .HasColumnName("Description")
            .HasMaxLength(255)
            .IsUnicode(false);

            builder.HasOne(d => d.idPhoneNavigation)
            .WithMany(p => p.ScriPhoneType)
            .HasForeignKey(d => d.idPhone)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_scri_PhoneType_Phone");
        }
    }
}
