using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities
{
    public class WsBrokerCompania
    {
        public WsBrokerCompania()
        {
            //AccProgramasAcciones = new HashSet<AccProgramasAcciones>();
        }

        public int ID { get; set; }
        public string codigo { get; set; }
        public string nombre { get; set; }

    }
}

