using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using eCore.Services.Models;


namespace eCore.Entities
{
    public partial class TrfSdtcomision
    {
        public TrfSdtcomision()
        {
            //AccProgramasAcciones = new HashSet<AccProgramasAcciones>();
        }

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int IdTrfNovedades { get; set; }
        public Decimal Monto { get; set; }
        public int NIVC { get; set; }
        public int NIVT { get; set; }
        public Decimal Porcentaje { get; set; }
        public int Rama { get; set; }

        //public virtual ICollection<TrfRama> TrfRama { get; set; }
        public virtual TrfNovedades IdTrfNovedadesNavigation { get; set; }
    }
}

