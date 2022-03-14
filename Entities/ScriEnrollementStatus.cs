using System;
using System.Collections.Generic;

namespace eCore.Entities
{
    public class ScriEnrollementStatus
    {
        public ScriEnrollementStatus()
        {
            //AccProgramasAcciones = new HashSet<AccProgramasAcciones>();
        }

        public int Id { get; set; }
        public int idTaxStatus { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        public virtual ScriTaxStatus idTaxStatusNavigation { get; set; }
    }
}
