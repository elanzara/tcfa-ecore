using System;
using System.Collections.Generic;
using eCore.Services.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCore.Entities
{

    public partial class MapCoberturas
    {
        public MapCoberturas()
        {
            //AccProgramasAcciones = new HashSet<AccProgramasAcciones>();
        }

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int IdMapNovedades { get; set; }
        public string poliza { get; set; }
        public double? endoso { get; set; }
        public double? riesgo { get; set; }
        public double? secu { get; set; }
        public double? cobertura { get; set; }
        public Decimal? sumaAseg { get; set; }

        public virtual MapNovedades IdMapNovedadesNavigation { get; set; }
    }
}
