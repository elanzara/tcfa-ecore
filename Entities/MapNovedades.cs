using System;
using System.Collections.Generic;
using eCore.Services.Models;

namespace eCore.Entities
{

    public partial class MapNovedades
    {
        public MapNovedades()
        {
            //AccProgramasAcciones = new HashSet<AccProgramasAcciones>();
        }

        public int Id { get; set; }
        public double? compania { get; set; }
        public double? sector { get; set; }
        public double? ramo { get; set; }
        public string poliza { get; set; }
        public double? productor { get; set; }
        public double? endoso { get; set; }
        public string fechaEmiSpto { get; set; }
        public string fechaEfecPol { get; set; }
        public string fechaVctoPol { get; set; }
        public string fechaEfecSpto { get; set; }
        public string fechaVctoSpto { get; set; }
        public double? codEndoso { get; set; }
        public double? subEndoso { get; set; }
        public string motivo { get; set; }
        public string tipoDocumentoTom { get; set; }
        public string codigoDocumentoTom { get; set; }
        public double? planPago { get; set; }
        public string polizaAnterior { get; set; }
        public string polizaMadre { get; set; }
        public string polizaSiguiente { get; set; }
        public string facturacion { get; set; }
        public string fechaRenov { get; set; }

        public virtual ICollection<MapCuotas> MapCuotas { get; set; }
        public virtual ICollection<MapCoberturas> MapCoberturas { get; set; }
        public virtual ICollection<MapDatosVariables> MapDatosVariables { get; set; }
        public virtual ICollection<MapDatosRiesgo> MapDatosRiesgo { get; set; }
        public virtual ICollection<MapAsegurados> MapAsegurados { get; set; }
        public virtual ICollection<MapImpuestos> MapImpuestos { get; set; }


    }
}
