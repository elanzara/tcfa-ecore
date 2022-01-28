

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities.Configuration
{
    public class ScriListJobSummaryConfiguration : IEntityTypeConfiguration<ScriListJobSummary>
    {
        public void Configure(EntityTypeBuilder<ScriListJobSummary> builder)
        {
            builder.ToTable("scri_list_job_summary");

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.Id).HasColumnName("IdScriMovimientos");

            builder.Property(e => e.OfferingPlan)
                    .HasColumnName("OfferingPlan")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            builder.Property(e => e.PolicyPeriodID)
                    .HasColumnName("PolicyPeriodID")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            builder.Property(e => e.ScopeCoverage)
                    .HasColumnName("ScopeCoverage")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            builder.Property(e => e.StartDate)
                    .HasColumnName("StartDate")
                    .HasColumnType("datetime");
            builder.Property(e => e.Status)
                    .HasColumnName("Status")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            builder.Property(e => e.TransactionJob)
                    .HasColumnName("TransactionJob")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            builder.Property(e => e.Subtype)
                    .HasColumnName("Subtype")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            builder.Property(e => e.EffectiveDate)
                    .HasColumnName("EffectiveDate")
                    .HasColumnType("datetime");
            builder.Property(e => e.PeriodEnd)
                    .HasColumnName("PeriodEnd")
                    .HasColumnType("datetime");
            builder.Property(e => e.PolicyStartDate)
                    .HasColumnName("PolicyStartDate")
                    .HasColumnType("datetime");
            builder.Property(e => e.PolicyNumber)
                    .HasColumnName("PolicyNumber")
                    .HasMaxLength(255)
                    .IsUnicode(false);

            builder.HasOne(d => d.IdScriMovimientosNavigation)
            .WithMany(p => p.ScriListJobSummary)
            .HasForeignKey(d => d.IdScriMovimientos)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_listjobsummary_movimientos");

        }
    }
}
