using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities.Configuration
{
    public class ScriPolicyTermConfiguration : IEntityTypeConfiguration<ScriPolicyTerm>
    {
        public void Configure(EntityTypeBuilder<ScriPolicyTerm> builder)
        {
            builder.ToTable("scri_PolicyTerm");

            builder.Property(e => e.Id).HasColumnName("id");

            builder.Property(e => e.idPolicy).HasColumnName("idPolicy");
            builder.Property(e => e.Description)
            .HasColumnName("Description")
            .HasMaxLength(255)
            .IsUnicode(false);
            builder.Property(e => e.IdentificationCode)
            .HasColumnName("IdentificationCode")
            .HasMaxLength(255)
            .IsUnicode(false);
            builder.Property(e => e.Selected)
            .HasColumnName("Selected")
            .HasMaxLength(255)
            .IsUnicode(false);

            builder.HasOne(d => d.idPolicyNavigation)
            .WithMany(p => p.ScriPolicyTerm)
            .HasForeignKey(d => d.idPolicy)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_scri_PolicyTerm_Policy");
        }
    }
}
