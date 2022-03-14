using System;
using System.Collections.Generic;

namespace eCore.Entities
{
    public class ScriOccupation
    {
        public ScriOccupation()
        {
            //AccProgramasAcciones = new HashSet<AccProgramasAcciones>();
        }

        public int Id { get; set; }
        public int idContact { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        public virtual ScriContact idContactNavigation { get; set; }
    }
}
