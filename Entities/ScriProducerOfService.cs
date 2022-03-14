using System;
using System.Collections.Generic;

namespace eCore.Entities
{
    public class ScriProducerOfService
    {
        public ScriProducerOfService()
        {
            //AccProgramasAcciones = new HashSet<AccProgramasAcciones>();
        }

        public int Id { get; set; }
        public int idPolicy { get; set; }
        public string Code { get; set; }
        public string OrganizationDisplayName { get; set; }
        public string OrganizationPublicID { get; set; }
        public string PublicID { get; set; }
        public string Selected { get; set; }

        public virtual ScriPolicy idPolicyNavigation { get; set; }
    }
}
