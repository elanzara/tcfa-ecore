using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities
{
    public class WsBrokerCiaCoberturaDetalle
    {
        public WsBrokerCiaCoberturaDetalle()
        {
            //AccProgramasAcciones = new HashSet<AccProgramasAcciones>();
        }

        [Key]
        public int CompaniaID { get; set; }
        public string CoberturaID { get; set; }
        public string Detalle { get; set; }
    }
}
