using System;
using System.Collections.Generic;

namespace eCore.Entities
{
    public class ScriSubtype
    {
        public ScriSubtype()
        {
            //AccProgramasAcciones = new HashSet<AccProgramasAcciones>();
        }

        public int Id { get; set; }
        public int idPolicy { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        public virtual ScriPolicy idPolicyNavigation { get; set; }
    }
}
