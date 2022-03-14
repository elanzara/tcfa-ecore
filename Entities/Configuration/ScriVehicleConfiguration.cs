using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities.Configuration
{
    public class ScriVehicleConfiguration : IEntityTypeConfiguration<ScriVehicle>
    {
        public void Configure(EntityTypeBuilder<ScriVehicle> builder)
        {
            builder.ToTable("scri_Vehicle");

            builder.Property(e => e.Id).HasColumnName("id");

            builder.Property(e => e.idPolicy).HasColumnName("idPolicy");
            builder.Property(e => e.BrandCode).HasColumnName("BrandCode");
            builder.Property(e => e.BrandName)
            .HasColumnName("BrandName")
            .HasMaxLength(255)
            .IsUnicode(false);
            builder.Property(e => e.DeductibleValueDescription)
            .HasColumnName("DeductibleValueDescription")
            .HasMaxLength(255)
            .IsUnicode(false);
            builder.Property(e => e.EngineNumber)
            .HasColumnName("EngineNumber")
            .HasMaxLength(255)
            .IsUnicode(false);
            builder.Property(e => e.HasClaimComputableForBonusMalus)
            .HasColumnName("HasClaimComputableForBonusMalus")
            .HasMaxLength(255)
            .IsUnicode(false);
            builder.Property(e => e.HasGPS)
            .HasColumnName("HasGPS")
            .HasMaxLength(255)
            .IsUnicode(false);
            builder.Property(e => e.HasInspections)
            .HasColumnName("HasInspections")
            .HasMaxLength(255)
            .IsUnicode(false);
            builder.Property(e => e.InfoAutoCode)
            .HasColumnName("InfoAutoCode")
            .HasMaxLength(255)
            .IsUnicode(false);
            builder.Property(e => e.Is0Km)
            .HasColumnName("Is0Km")
            .HasMaxLength(255)
            .IsUnicode(false);
            builder.Property(e => e.IsPatentedAtArg)
            .HasColumnName("IsPatentedAtArg")
            .HasMaxLength(255)
            .IsUnicode(false);
            builder.Property(e => e.IsTruck10TT100KM)
            .HasColumnName("IsTruck10TT100KM")
            .HasMaxLength(255)
            .IsUnicode(false);
            builder.Property(e => e.LicensePlate)
            .HasColumnName("LicensePlate")
            .HasMaxLength(255)
            .IsUnicode(false);
            builder.Property(e => e.ModelCode)
            .HasColumnName("ModelCode")
            .HasMaxLength(255)
            .IsUnicode(false);
            builder.Property(e => e.ModelName)
            .HasColumnName("ModelName")
            .HasMaxLength(255)
            .IsUnicode(false);
            builder.Property(e => e.OriginalCostNew).HasColumnType("decimal(15, 2)").HasColumnName("OriginalCostNew");
            builder.Property(e => e.OtherBrandName)
            .HasColumnName("OtherBrandName")
            .HasMaxLength(255)
            .IsUnicode(false);
            builder.Property(e => e.OtherModelName)
            .HasColumnName("OtherModelName")
            .HasMaxLength(255)
            .IsUnicode(false);
            builder.Property(e => e.OtherVersionName)
            .HasColumnName("OtherVersionName")
            .HasMaxLength(255)
            .IsUnicode(false);
            builder.Property(e => e.PolicyOwnerIsInsured)
            .HasColumnName("PolicyOwnerIsInsured")
            .HasMaxLength(255)
            .IsUnicode(false);
            builder.Property(e => e.PrimaryNamedInsured)
            .HasColumnName("PrimaryNamedInsured")
            .HasMaxLength(255)
            .IsUnicode(false);
            builder.Property(e => e.PublicId)
            .HasColumnName("PublicId")
            .HasMaxLength(255)
            .IsUnicode(false);
            builder.Property(e => e.StatedAmount).HasColumnType("decimal(15, 2)").HasColumnName("StatedAmount");
            builder.Property(e => e.TargetPremium).HasColumnType("decimal(15, 2)").HasColumnName("TargetPremium");
            builder.Property(e => e.TargetPremiumAfterTax).HasColumnType("decimal(15, 2)").HasColumnName("TargetPremiumAfterTax");
            builder.Property(e => e.VIN)
            .HasColumnName("VIN")
            .HasMaxLength(255)
            .IsUnicode(false);
            builder.Property(e => e.VTVExpirationDate)
                .HasColumnName("VTVExpirationDate")
                .HasColumnType("datetime");
            builder.Property(e => e.VehicleNumber).HasColumnName("VehicleNumber");
            builder.Property(e => e.VersionCode).HasColumnName("VersionCode");
            builder.Property(e => e.VersionName)
            .HasColumnName("VersionName")
            .HasMaxLength(255)
            .IsUnicode(false);
            builder.Property(e => e.Year).HasColumnName("Year");
            builder.Property(e => e.CodigoInfoAuto).HasColumnName("CodigoInfoAuto");

            builder.HasOne(d => d.idPolicyNavigation)
            .WithMany(p => p.ScriVehicle)
            .HasForeignKey(d => d.idPolicy)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_scri_Vehicle_Policy");
        }
    }
}
