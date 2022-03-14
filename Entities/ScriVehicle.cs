using System;
using System.Collections.Generic;

namespace eCore.Entities
{
    public class ScriVehicle
    {
        public ScriVehicle()
        {
            //AccProgramasAcciones = new HashSet<AccProgramasAcciones>();
        }

        public int Id { get; set; }
        public int idPolicy { get; set; }
        public int BrandCode { get; set; }
        public string BrandName { get; set; }
        public string DeductibleValueDescription { get; set; }
        public string EngineNumber { get; set; }
        public string HasClaimComputableForBonusMalus { get; set; }
        public string HasGPS { get; set; }
        public string HasInspections { get; set; }
        public string InfoAutoCode { get; set; }
        public string Is0Km { get; set; }
        public string IsPatentedAtArg { get; set; }
        public string IsTruck10TT100KM { get; set; }
        public string LicensePlate { get; set; }
        public string ModelCode { get; set; }
        public string ModelName { get; set; }
        public Decimal OriginalCostNew { get; set; }
        public string OtherBrandName { get; set; }
        public string OtherModelName { get; set; }
        public string OtherVersionName { get; set; }
        public string PolicyOwnerIsInsured { get; set; }
        public string PrimaryNamedInsured { get; set; }
        public string PublicId { get; set; }
        public Decimal StatedAmount { get; set; }
        public Decimal TargetPremium { get; set; }
        public Decimal TargetPremiumAfterTax { get; set; }
        public string VIN { get; set; }
        public DateTime VTVExpirationDate { get; set; }
        public int VehicleNumber { get; set; }
        public int VersionCode { get; set; }
        public string VersionName { get; set; }
        public int Year { get; set; }
        public int CodigoInfoAuto { get; set; }

        public virtual ScriPolicy idPolicyNavigation { get; set; }
        public virtual ICollection<ScriAutomaticAdjust> ScriAutomaticAdjust { get; set; }
        public virtual ICollection<ScriCategory> ScriCategory { get; set; }
        public virtual ICollection<ScriColor> ScriColor { get; set; }
        public virtual ICollection<ScriFuelType> ScriFuelType { get; set; }
        public virtual ICollection<ScriJurisdiction> ScriJurisdiction { get; set; }
        public virtual ICollection<ScriOriginCountry> ScriOriginCountry { get; set; }
        public virtual ICollection<ScriProductOffering> ScriProductOffering { get; set; }
        public virtual ICollection<ScriRiskLocation> ScriRiskLocation { get; set; }
        public virtual ICollection<ScriUsage> ScriUsage { get; set; }
        
        

    }
}
