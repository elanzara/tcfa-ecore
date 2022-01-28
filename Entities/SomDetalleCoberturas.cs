using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities
{
    public class SomDetalleCoberturas
    {
        public SomDetalleCoberturas()
        {
            //AccProgramasAcciones = new HashSet<AccProgramasAcciones>();
        }

        [Key]
        public int Id { get; set; }
        public int CodigoTCFA { get; set; }
        public int CompaniaID { get; set; }
        public string Cobertura { get; set; }
        public string Nombre { get; set; }
    }
}
