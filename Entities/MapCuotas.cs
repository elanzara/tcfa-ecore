using System;
using System;
using System.Collections.Generic;
using eCore.Services.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCore.Entities
{

    public partial class MapCuotas
    {
        public MapCuotas()
        {
            //AccProgramasAcciones = new HashSet<AccProgramasAcciones>();
        }

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int IdMapNovedades { get; set; }
        public string poliza { get; set; }
        public double? endoso { get; set; }
        public double? numeroRecibo { get; set; }
        public string convenio { get; set; }
        public string vctoRecibo { get; set; }
        public string fecCobro { get; set; }
        public double? agrpImpositivo { get; set; }
        public string medioPago { get; set; }
        public double? moneda { get; set; }
        public Decimal? importe { get; set; }
        public Decimal? cobroAnticipado { get; set; }
        public Decimal? impComisiones { get; set; }
        public string situacionRecibo { get; set; }

        public virtual MapNovedades IdMapNovedadesNavigation { get; set; }
    }
}
