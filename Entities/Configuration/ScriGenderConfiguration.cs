using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace eCore.Entities.Configuration
{
    public class ScriGenderConfiguration : IEntityTypeConfiguration<ScriGender>
    {
        public void Configure(EntityTypeBuilder<ScriGender> builder)
        {
            builder.ToTable("scri_Gender");

            builder.Property(e => e.Id).HasColumnName("id");

            builder.Property(e => e.idContact).HasColumnName("idContact");

            builder.Property(e => e.Code)
            .HasColumnName("Code")
            .HasMaxLength(255)
            .IsUnicode(false);

            builder.Property(e => e.Description)
            .HasColumnName("Description")
            .HasMaxLength(255)
            .IsUnicode(false);

            builder.HasOne(d => d.idContactNavigation)
            .WithMany(p => p.ScriGender)
            .HasForeignKey(d => d.idContact)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_scri_Gender_Contact");
        }
    }
}
