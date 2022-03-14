using System;
using System.Collections.Generic;

namespace eCore.Entities
{
    public class ScriState
    {
        public ScriState()
        {
            //AccProgramasAcciones = new HashSet<AccProgramasAcciones>();
        }

        public int Id { get; set; }
        public int idAddress { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        public virtual ScriAddress idAddressNavigation { get; set; }
    }
}
