using System;
using System.Collections.Generic;
using eCore.Services.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCore.Entities
{

    public partial class MapDatosRiesgo
    {
        public MapDatosRiesgo()
        {
            //AccProgramasAcciones = new HashSet<AccProgramasAcciones>();
        }

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int IdMapNovedades { get; set; }
        public string poliza { get; set; }
        public double? endoso { get; set; }
        public double? riesgo { get; set; }
        public string nombreRiesgo { get; set; }
        public string vigencia { get; set; }
        public string vencimiento { get; set; }
        public string baja { get; set; }


        public virtual MapNovedades IdMapNovedadesNavigation { get; set; }
    }
}
