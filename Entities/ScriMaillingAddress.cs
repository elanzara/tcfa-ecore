using System;
using System.Collections.Generic;

namespace eCore.Entities
{
    public class ScriMaillingAddress
    {
        public ScriMaillingAddress()
        {
            //AccProgramasAcciones = new HashSet<AccProgramasAcciones>();
        }

        public int Id { get; set; }
        public int idPolicy { get; set; }
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

        public virtual ScriPolicy idPolicyNavigation { get; set; }
        public virtual ICollection<ScriMaillingAddressAddressType> ScriMaillingAddressAddressType { get; set; }
        public virtual ICollection<ScriMaillingAddressCountry> ScriMaillingAddressCountry { get; set; }
        public virtual ICollection<ScriMaillingAddressState> ScriMaillingAddressState { get; set; }
    }
}
