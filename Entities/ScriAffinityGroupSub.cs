using System;
using System.Collections.Generic;

namespace eCore.Entities
{
    public class ScriAffinityGroupSub
    {

        public ScriAffinityGroupSub()
        {
            //AccProgramasAcciones = new HashSet<AccProgramasAcciones>();
        }

        public int Id { get; set; }
        public int idPolicy { get; set; }
        public string DisplayName { get; set; }
        public DateTime EndDate { get; set; }
        public string PublicID { get; set; }
        public DateTime StartDate { get; set; }

        public virtual ScriPolicy idPolicyNavigation { get; set; }
        public virtual ICollection<ScriAffinityGroupType> ScriAffinityGroupType { get; set; }

    }
}
