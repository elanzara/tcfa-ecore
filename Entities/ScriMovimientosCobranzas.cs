using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities
{
    public class ScriMovimientosCobranzas
    {
        public ScriMovimientosCobranzas()
        {
            //AccProgramasAcciones = new HashSet<AccProgramasAcciones>();
        }

        public int id { get; set; }
        public string poliza { get; set; }
        public DateTime? Ext_ApplicationDate { get; set; }
        public decimal? PaymentAmount { get; set; }
        public DateTime? ReversedDate { get; set; }

        //public virtual ICollection<ScriListJobSummary> ScriListJobSummary { get; set; }
        //public virtual ICollection<ScriMessages> ScriMessages { get; set; }

    }
}
