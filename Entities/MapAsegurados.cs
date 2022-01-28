using System;
using System.Collections.Generic;
using eCore.Services.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCore.Entities
{

    public partial class MapAsegurados
    {
        public MapAsegurados()
        {
            //AccProgramasAcciones = new HashSet<AccProgramasAcciones>();
        }

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int IdMapNovedades { get; set; }
        public string poliza { get; set; }
        public double? endoso { get; set; }
        public double? riesgo { get; set; }
        public string tipoBenef { get; set; }
        public string tipoDoc { get; set; }
        public string codDoc { get; set; }
        public string asegurado { get; set; }
        public string domicilio { get; set; }
        public double? localidad { get; set; }
        public double? postal { get; set; }
        public double? provincia { get; set; }
        public double? telefonoPais { get; set; }
        public double? telefonoZona { get; set; }
        public double? telefono { get; set; }
        public string domicilioCom { get; set; }
        public double? localidadCom { get; set; }
        public double? postalCom { get; set; }
        public double? provinciaCom { get; set; }
        public string nacimiento { get; set; }
        public double? iva { get; set; }
        public string mcaSexo { get; set; }
        public string nomina { get; set; }
        public string baja { get; set; }

        public virtual MapNovedades IdMapNovedadesNavigation { get; set; }
    }
}
