using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities.Configuration
{
    public class ScriPhoneConfiguration : IEntityTypeConfiguration<ScriPhone>
    {
        public void Configure(EntityTypeBuilder<ScriPhone> builder)
        {
            builder.ToTable("scri_Phone");

            builder.Property(e => e.Id).HasColumnName("id");

            builder.Property(e => e.idContact).HasColumnName("idContact");

            builder.Property(e => e.PhoneNumber)
            .HasColumnName("PhoneNumber")
            .HasMaxLength(255)
            .IsUnicode(false);

            builder.HasOne(d => d.idContactNavigation)
            .WithMany(p => p.ScriPhone)
            .HasForeignKey(d => d.idContact)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_scri_Phone_Contact");
        }
    }
}
