using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities
{
    public class ReporteCobertura
    {
        public ReporteCobertura()
        {
            //AccProgramasAcciones = new HashSet<AccProgramasAcciones>();
        }

        
        public int CompaniaID { get; set; }
        public string Compania { get; set; }
        
        public string CoberturaID { get; set; }
        [Key]
        public string Detalle { get; set; }
        public int FamiliaID { get; set; }
        public string Descripcion { get; set; }
        public Boolean Activo { get; set; }
        public Boolean AceptaPagoEfectivo { get; set; }
        public string Telesale { get; set; }
    }
}
