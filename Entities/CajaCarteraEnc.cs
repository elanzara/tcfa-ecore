using System;
using System.Collections.Generic;

namespace eCore.Entities
{
    public class CajaCarteraEnc
    {
        public CajaCarteraEnc()
        {
            //AccProgramasAcciones = new HashSet<AccProgramasAcciones>();
        }

        public int Id { get; set; }
        public string Archivo { get; set; }
        public string Usuario { get; set; }
        public DateTime FechaProceso { get; set; }

        //public List<AllianzCarteraDet> allianzCarteraDets { get; set; }
        //public virtual ICollection<AllianzCarteraDet> AllianzCarteraDet { get; set; }
        public virtual ICollection<CajaCarteraDet> CajaCarteraDet { get; set; }
    }
}

