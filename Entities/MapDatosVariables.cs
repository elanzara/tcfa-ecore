using System;
using System.Collections.Generic;
using eCore.Services.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCore.Entities
{

    public partial class MapDatosVariables
    {
        public MapDatosVariables()
        {
            //AccProgramasAcciones = new HashSet<AccProgramasAcciones>();
        }

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int IdMapNovedades { get; set; }
        public string poliza { get; set; }
        public double? endoso { get; set; }
        public double? riesgo { get; set; }
        public string campo { get; set; }
        public string valor { get; set; }
        public string descripcion { get; set; }
        public double? nivel { get; set; }
        public double? secu { get; set; }

        public virtual MapNovedades IdMapNovedadesNavigation { get; set; }
    }
}
