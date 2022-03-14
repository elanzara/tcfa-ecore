using System;
using System.Collections.Generic;

namespace eCore.Entities
{
    public class ScriPolicy
    {

        public ScriPolicy()
        {
            //AccProgramasAcciones = new HashSet<AccProgramasAcciones>();
        }

        public int Id { get; set; }
        public int idPolizaB2B { get; set; }
        public decimal InsuredCapital { get; set; }
        public DateTime JobDate { get; set; }
        public int MaxAgeEntry { get; set; }
        public int MaxAgeStayAdditional { get; set; }
        public int MaxAgeStayBasic { get; set; }
        public decimal MaximaCompensation { get; set; }
        public DateTime FirstPolicyDate { get; set; }

        public string RamoDescripcion { get; set; }
        public string AccountNumber { get; set; }
        public int MinAgeEntry { get; set; }
        public decimal PaymentFees { get; set; }
        public DateTime PeriodEnd { get; set; }
        public DateTime PeriodStart { get; set; }
        public string PolicyPeriodID { get; set; }
        public DateTime TripEndDate { get; set; }
        public DateTime TripStarDate { get; set; }
        public string FacultativePolicy { get; set; }
        public string ProvisionalGuard { get; set; }
        public string JobNumber { get; set; }
        public int BranchNumber { get; set; }
        public string RenewTo { get; set; }

        public virtual ScriPolizab2b idPolizaB2BNavigation { get; set; }
        public virtual ICollection<ScriAffinityGroupSub> ScriAffinityGroupSub { get; set; }
        public virtual ICollection<ScriSubtype> ScriSubtype { get; set; }
        public virtual ICollection<ScriContact> ScriContact { get; set; }
        public virtual ICollection<ScriCountry> ScriCountry { get; set; }
        public virtual ICollection<ScriMaillingAddress> ScriMaillingAddress { get; set; }
        public virtual ICollection<ScriExt_RamoSSN> ScriExt_RamoSSN { get; set; }
        public virtual ICollection<ScriExt_PolicyType> ScriExt_PolicyType { get; set; }
        public virtual ICollection<ScriProducerCode> ScriProducerCode { get; set; }
        public virtual ICollection<ScriPaymentMethod> ScriPaymentMethod { get; set; }
        public virtual ICollection<ScriPolicyTerm> ScriPolicyTerm { get; set; }
        public virtual ICollection<ScriCurrency> ScriCurrency { get; set; }
        public virtual ICollection<ScriProducerAgent> ScriProducerAgent { get; set; }
        public virtual ICollection<ScriProducerOfService> ScriProducerOfService { get; set; }
        public virtual ICollection<ScriServiceOrganizer> ScriServiceOrganizer { get; set; }
        public virtual ICollection<ScriChannelEntry> ScriChannelEntry { get; set; }
        public virtual ICollection<ScriStatus> ScriStatus { get; set; }
        public virtual ICollection<ScriVehicle> ScriVehicle { get; set; }
        public virtual ICollection<ScriReasonCancelDTO> ScriReasonCancelDTO { get; set; }
        

        //public List<AllianzCarteraDet> allianzCarteraDets { get; set; }
        //public virtual ICollection<AllianzCarteraDet> AllianzCarteraDet { get; set; }


        //public virtual ICollection<CajaCarteraDet> CajaCarteraDet { get; set; }
    }
}
