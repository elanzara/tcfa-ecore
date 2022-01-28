using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities
{
    public class WsBrokerCiaFamilia
    {
        public WsBrokerCiaFamilia()
        {
            //AccProgramasAcciones = new HashSet<AccProgramasAcciones>();
        }

        [Key]
        public int CodigoTCFA { get; set; }
        public int CompaniaID { get; set; }
        public int FamiliaID { get; set; }
        public string Cobertura { get; set; }
        public Boolean Activo { get; set; }
        public Boolean AceptaPagoEfectivo { get; set; }
    }
}
