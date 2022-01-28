using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCore.Entities
{
    public class CajaCarteraAccesorio
    {
        public CajaCarteraAccesorio()
        {
            //AccProgramasAcciones = new HashSet<AccProgramasAcciones>();
        }

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int IdCajaCarteraEnc { get; set; }
        public int Codigo { get; set; }
        public int Valor { get; set; }

        public virtual CajaCarteraDet IdCajaCarteraEncNavigation { get; set; }
    }
}
