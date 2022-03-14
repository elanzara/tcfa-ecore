using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities.Configuration
{
    public class ScriPolicyConfiguration : IEntityTypeConfiguration<ScriPolicy>
    {
        public void Configure(EntityTypeBuilder<ScriPolicy> builder)
        {
            builder.ToTable("scri_policy");

            builder.Property(e => e.Id).HasColumnName("id");

            builder.Property(e => e.idPolizaB2B).HasColumnName("idPolizaB2B");
            builder.Property(e => e.InsuredCapital).HasColumnType("Decimal(12,2)").HasColumnName("InsuredCapital");
            builder.Property(e => e.JobDate)
                .HasColumnName("JobDate")
                .HasColumnType("datetime");
            builder.Property(e => e.MaxAgeEntry).HasColumnName("MaxAgeEntry");
            builder.Property(e => e.MaxAgeStayAdditional).HasColumnName("MaxAgeStayAdditional");
            builder.Property(e => e.MaxAgeStayBasic).HasColumnName("MaxAgeStayBasic");
            builder.Property(e => e.MaximaCompensation).HasColumnType("Decimal(12,2)").HasColumnName("MaximaCompensation");
            builder.Property(e => e.FirstPolicyDate)
                .HasColumnName("FirstPolicyDate")
                .HasColumnType("datetime");
            builder.Property(e => e.RamoDescripcion)
                    .HasColumnName("RamoDescripcion")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            builder.Property(e => e.AccountNumber)
                    .HasColumnName("AccountNumber")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            builder.Property(e => e.MinAgeEntry).HasColumnName("MinAgeEntry");
            builder.Property(e => e.PaymentFees).HasColumnType("Decimal(12,2)").HasColumnName("PaymentFees");
            builder.Property(e => e.PeriodEnd)
                .HasColumnName("PeriodEnd")
                .HasColumnType("datetime");
            builder.Property(e => e.PeriodStart)
                .HasColumnName("PeriodStart")
                .HasColumnType("datetime");
            builder.Property(e => e.PolicyPeriodID)
                    .HasColumnName("PolicyPeriodID")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            builder.Property(e => e.TripEndDate)
                .HasColumnName("TripEndDate")
                .HasColumnType("datetime");
            builder.Property(e => e.TripStarDate)
                .HasColumnName("TripStarDate")
                .HasColumnType("datetime");
            builder.Property(e => e.FacultativePolicy)
                    .HasColumnName("FacultativePolicy")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            builder.Property(e => e.ProvisionalGuard)
                    .HasColumnName("ProvisionalGuard")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            builder.Property(e => e.JobNumber)
                    .HasColumnName("JobNumber")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            builder.Property(e => e.BranchNumber).HasColumnName("BranchNumber");
            builder.Property(e => e.RenewTo)
                    .HasColumnName("RenewTo")
                    .HasMaxLength(255)
                    .IsUnicode(false);

            builder.HasOne(d => d.idPolizaB2BNavigation)
                .WithMany(p => p.ScriPolicy)
                .HasForeignKey(d => d.idPolizaB2B)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_scri_polizab2b");

        }
    }
}
