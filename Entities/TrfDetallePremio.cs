using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using eCore.Services.Models;


namespace eCore.Entities
{
    public partial class TrfDetallePremio
    {
        public TrfDetallePremio()
        {
            //AccProgramasAcciones = new HashSet<AccProgramasAcciones>();
        }

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int IdTrfNovedades { get; set; }
        public Decimal Premio { get; set; }

        public virtual ICollection<TrfRama> TrfRama { get; set; }
        public virtual TrfNovedades IdTrfNovedadesNavigation { get; set; }
    }
}
