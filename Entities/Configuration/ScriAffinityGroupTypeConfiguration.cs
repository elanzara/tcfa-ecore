using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities.Configuration
{
    public class ScriAffinityGroupTypeConfiguration : IEntityTypeConfiguration<ScriAffinityGroupType>
    {
        public void Configure(EntityTypeBuilder<ScriAffinityGroupType> builder)
        {
            builder.ToTable("scri_AffinityGroupType");

            builder.Property(e => e.Id).HasColumnName("id");

            builder.Property(e => e.idAffinityGroupSub).HasColumnName("idAffinityGroupSub");
            builder.Property(e => e.Code)
            .HasColumnName("Code")
            .HasMaxLength(255)
            .IsUnicode(false);
            builder.Property(e => e.Description)
            .HasColumnName("Description")
            .HasMaxLength(255)
            .IsUnicode(false);

            builder.HasOne(d => d.idAffinityGroupSubNavigation)
            .WithMany(p => p.ScriAffinityGroupType)
            .HasForeignKey(d => d.idAffinityGroupSub)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_scri_AffinityGroupType_AffinityGroupSub");
        }
    }
}
