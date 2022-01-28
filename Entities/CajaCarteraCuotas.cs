using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCore.Entities
{
    public class CajaCarteraCuotas
    {
        public CajaCarteraCuotas()
        {
            //AccProgramasAcciones = new HashSet<AccProgramasAcciones>();
        }

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int IdCajaCarteraEnc { get; set; }
        public int NumCuota { get; set; }
        public DateTime FechaVto { get; set; }
        public String Situacion { get; set; }
        public Decimal Prima { get; set; }
        public Decimal Comision { get; set; }
        public Decimal Premio { get; set; }
        public Decimal PorcInflacion { get; set; }

        public virtual CajaCarteraDet IdCajaCarteraEncNavigation { get; set; }
    }
}

