

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities.Configuration
{
    public class ScriPolicyTypeConfiguration : IEntityTypeConfiguration<ScriPolicyType>
    {
        public void Configure(EntityTypeBuilder<ScriPolicyType> builder)
        {
            builder.ToTable("scri_policy_type");

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.Id).HasColumnName("IdScriListJobSummary");


            builder.Property(e => e.Code)
                        .HasColumnName("Code")
                        .HasMaxLength(255)
                        .IsUnicode(false);
            builder.Property(e => e.Description)
                        .HasColumnName("Description")
                        .HasMaxLength(255)
                        .IsUnicode(false);

            builder.HasOne(d => d.IdScriListJobSummaryNavigation)
            .WithMany(p => p.ScriPolicyType)
            .HasForeignKey(d => d.IdScriListJobSummary)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_policytype_listjobsummary");

        }
    }
}
