using System;
using System.Collections.Generic;

namespace eCore.Entities
{
    public class ScriAddress
    {
        public ScriAddress()
        {
            //AccProgramasAcciones = new HashSet<AccProgramasAcciones>();
        }

        public int Id { get; set; }
        public int idContact { get; set; }
        public string updateLinkedAddresses { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string DisplayText { get; set; }
        public string PolicyAddress { get; set; }
        public string PostalCode { get; set; }
        public string PrimaryAddress { get; set; }
        public string PublicID { get; set; }
        public string StreetNumber { get; set; }

        public virtual ScriContact idContactNavigation { get; set; }
        public virtual ICollection<ScriAddressType> ScriAddressType { get; set; }
        public virtual ICollection<ScriAddressCountry> ScriAddressCountry { get; set; }
        public virtual ICollection<ScriState> ScriState { get; set; }
    }
}
