using System;
using System.Collections.Generic;

namespace eCore.Entities
{
    public class ScriContact
    {
        public ScriContact()
        {
            //AccProgramasAcciones = new HashSet<AccProgramasAcciones>();
        }

        public int Id { get; set; }
        public int idPolicy { get; set; }
        public DateTime Activitystartdate { get; set; }
        public string CUIL { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string EmailAddress1 { get; set; }
        public string FirstName { get; set; }
        public string InsuredNumberFormated { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public string PEP { get; set; }
        public string PrimaryNamedInsured { get; set; }
        public string PublicID { get; set; }
        public string Resident { get; set; }
        public string TaxID { get; set; }
        public string UIFFormSubmitted { get; set; }

        public virtual ScriPolicy idPolicyNavigation { get; set; }
        public virtual ICollection<ScriAddress> ScriAddress { get; set; }
        public virtual ICollection<ScriPhone> ScriPhone { get; set; }
        public virtual ICollection<ScriContactType> ScriContactType { get; set; }
        public virtual ICollection<ScriGender> ScriGender { get; set; }
        public virtual ICollection<ScriMaritalStatus> ScriMaritalStatus { get; set; }
        public virtual ICollection<ScriNationality> ScriNationality { get; set; }
        public virtual ICollection<ScriOccupation> ScriOccupation { get; set; }
        public virtual ICollection<ScriOfficialIDType> ScriOfficialIDType { get; set; }
        public virtual ICollection<ScriPreferredSettlementCurrency> ScriPreferredSettlementCurrency { get; set; }
        public virtual ICollection<ScriSchoolLevel> ScriSchoolLevel { get; set; }
        public virtual ICollection<ScriTaxStatus> ScriTaxStatus { get; set; }

    }
}
