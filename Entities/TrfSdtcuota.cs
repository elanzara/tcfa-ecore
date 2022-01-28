using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using eCore.Services.Models;


namespace eCore.Entities
{
    public partial class TrfSdtcuota
    {
        public TrfSdtcuota()
        {
            //AccProgramasAcciones = new HashSet<AccProgramasAcciones>();
        }

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int IdTrfNovedades { get; set; }
        public string Estado { get; set; }
        public string FechaCancelada { get; set; }
        public string FechaVtoCuota { get; set; }
        public Decimal ImporteCuota { get; set; }
        public int NumeroCuota { get; set; }


        //public virtual ICollection<TrfRama> TrfRama { get; set; }
        public virtual TrfNovedades IdTrfNovedadesNavigation { get; set; }
    }
}