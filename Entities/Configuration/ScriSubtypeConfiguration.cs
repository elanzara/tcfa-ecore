using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities.Configuration
{
    public class ScriSubtypeConfiguration : IEntityTypeConfiguration<ScriSubtype>
    {

        public void Configure(EntityTypeBuilder<ScriSubtype> builder)
        {
            builder.ToTable("scri_Subtype");

            builder.Property(e => e.Id).HasColumnName("id");

            builder.Property(e => e.idPolicy).HasColumnName("idPolicy");
            builder.Property(e => e.Code)
            .HasColumnName("Code")
            .HasMaxLength(255)
            .IsUnicode(false);
            builder.Property(e => e.Description)
            .HasColumnName("Description")
            .HasMaxLength(255)
            .IsUnicode(false);

            builder.HasOne(d => d.idPolicyNavigation)
            .WithMany(p => p.ScriSubtype)
            .HasForeignKey(d => d.idPolicy)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_scri_Subtype_Policy");
        }
    }
}
