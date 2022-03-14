using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities.Configuration
{
    public class ScriAffinityGroupSubConfiguration : IEntityTypeConfiguration<ScriAffinityGroupSub>
    {
        public void Configure(EntityTypeBuilder<ScriAffinityGroupSub> builder)
        {
            builder.ToTable("scri_AffinityGroupSub");

            builder.Property(e => e.Id).HasColumnName("id");

            builder.Property(e => e.idPolicy).HasColumnName("idPolicy");
            builder.Property(e => e.DisplayName)
            .HasColumnName("DisplayName")
            .HasMaxLength(255)
            .IsUnicode(false);
            builder.Property(e => e.EndDate)
            .HasColumnName("EndDate")
            .HasColumnType("datetime");
            builder.Property(e => e.PublicID)
            .HasColumnName("PublicID")
            .HasMaxLength(255)
            .IsUnicode(false);
            builder.Property(e => e.StartDate)
            .HasColumnName("StartDate")
            .HasColumnType("datetime");

            builder.HasOne(d => d.idPolicyNavigation)
            .WithMany(p => p.ScriAffinityGroupSub)
            .HasForeignKey(d => d.idPolicy)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_scri_AffinityGroupSub_Policy");
        }
    }
}
