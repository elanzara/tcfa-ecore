using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities
{
    public class WsBrokerFamilia
    {
        public WsBrokerFamilia()
        {
            //AccProgramasAcciones = new HashSet<AccProgramasAcciones>();
        }

        [Key]
        public int familiaid { get; set; }
        public string codigo { get; set; }
        public string descripcion { get; set; }
    }
}
