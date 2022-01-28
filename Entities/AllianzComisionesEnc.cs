using System;
using System.Collections.Generic;

namespace eCore.Entities
{

    public partial class AllianzComisionesEnc
    {
        public AllianzComisionesEnc()
        {
            //AccProgramasAcciones = new HashSet<AccProgramasAcciones>();
        }

        public int Id { get; set; }
        public string Archivo { get; set; }
        public string Usuario { get; set; }
        public DateTime FechaProceso { get; set; }

        //public List<AllianzCarteraDet> allianzCarteraDets { get; set; }
        //public virtual ICollection<AllianzCarteraDet> AllianzCarteraDet { get; set; }
        public virtual ICollection<AllianzComisionesDet> AllianzComisionesDet { get; set; }
    }
}
