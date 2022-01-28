using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Services.Models
{
    public class MapNovedadesOut
    {
        [JsonProperty("data")]
        public data[] data { get; set; }
        //public Novedad[] Novedad { get; set; }
    }
    //public class data
    //{
    //    public Novedad[] Novedad { get; set; }
    //}
    public class data //Novedad
    {
        [JsonProperty("compania")]
        public Double? compania { get; set; }
        [JsonProperty("sector")]
        public Double? sector { get; set; }
        [JsonProperty("ramo")]
        public Double? ramo { get; set; }
        [JsonProperty("poliza")]
        public string poliza { get; set; }
        [JsonProperty("productor")]
        public Double? productor { get; set; }
        [JsonProperty("endoso")]
        public Double? endoso { get; set; }
        [JsonProperty("fechaEmiSpto")]
        public string fechaEmiSpto { get; set; }
        [JsonProperty("fechaEfecPol")]
        public string fechaEfecPol { get; set; }
        [JsonProperty("fechaVctoPol")]
        public string fechaVctoPol { get; set; }
        [JsonProperty("fechaEfecSpto")]
        public string fechaEfecSpto { get; set; }
        [JsonProperty("fechaVctoSpto")]
        public string fechaVctoSpto { get; set; }
        [JsonProperty("codEndoso")]
        public Double? codEndoso { get; set; }
        [JsonProperty("subEndoso")]
        public Double? subEndoso { get; set; }
        [JsonProperty("motivo")]
        public string motivo { get; set; }
        [JsonProperty("tipoDocumentoTom")]
        public string tipoDocumentoTom { get; set; }
        [JsonProperty("codigoDocumentoTom")]
        public string codigoDocumentoTom { get; set; }
        [JsonProperty("planPago")]
        public Double? planPago { get; set; }
        [JsonProperty("polizaAnterior")]
        public string polizaAnterior { get; set; }
        [JsonProperty("polizaMadre")]
        public string polizaMadre { get; set; }
        [JsonProperty("polizaSiguiente")]
        public string polizaSiguiente { get; set; }
        [JsonProperty("facturacion")]
        public string facturacion { get; set; }
        [JsonProperty("fechaRenov")]
        public string fechaRenov { get; set; }
        [JsonProperty("cuotas")]
        public cuotas[] cuotas { get; set; }
        [JsonProperty("coberturas")]
        public coberturas[] coberturas { get; set; }
        [JsonProperty("datosVariables")]
        public datosVariables[] datosVariables { get; set; }
        [JsonProperty("datosRiesgo")]
        public datosRiesgo[] datosRiesgo { get; set; }
        [JsonProperty("asegurados")]
        public asegurados[] asegurados { get; set; }
        [JsonProperty("impuestos")]
        public impuestos[] impuestos { get; set; }

    }
    public class cuotas
    {
        [JsonProperty("poliza")]
        public string poliza { get; set; }
        [JsonProperty("endoso")]
        public Double? endoso { get; set; }
        [JsonProperty("numeroRecibo")]
        public Double? numeroRecibo { get; set; }
        [JsonProperty("convenio")]
        public string convenio { get; set; }
        [JsonProperty("vctoRecibo")]
        public string vctoRecibo { get; set; }
        [JsonProperty("fecCobro")]
        public string fecCobro { get; set; }
        [JsonProperty("agrpImpositivo")]
        public Double? agrpImpositivo { get; set; }
        [JsonProperty("medioPago")]
        public string medioPago { get; set; }
        [JsonProperty("moneda")]
        public Double? moneda { get; set; }
        [JsonProperty("importe")]
        public Decimal? importe { get; set; }
        [JsonProperty("cobroAnticipado")]
        public Decimal? cobroAnticipado { get; set; }
        [JsonProperty("impComisiones")]
        public Decimal? impComisiones { get; set; }
        [JsonProperty("situacionRecibo")]
        public string situacionRecibo { get; set; }
    }
    public class coberturas
    {
        [JsonProperty("poliza")]
        public string poliza { get; set; }
        [JsonProperty("endoso")]
        public Double? endoso { get; set; }
        [JsonProperty("riesgo")]
        public Double? riesgo { get; set; }
        [JsonProperty("secu")]
        public Double? secu { get; set; }
        [JsonProperty("cobertura")]
        public Double? cobertura { get; set; }
        [JsonProperty("sumaAseg")]
        public Decimal? sumaAseg { get; set; }
    }
    public class datosVariables
    {
        [JsonProperty("poliza")]
        public string poliza { get; set; }
        [JsonProperty("endoso")]
        public Double? endoso { get; set; }
        [JsonProperty("riesgo")]
        public Double? riesgo { get; set; }
        [JsonProperty("campo")]
        public string campo { get; set; }
        [JsonProperty("valor")]
        public string valor { get; set; }
        [JsonProperty("descripcion")]
        public string descripcion { get; set; }
        [JsonProperty("nivel")]
        public Double? nivel { get; set; }
        [JsonProperty("secu")]
        public Double? secu { get; set; }
    }
    public class datosRiesgo
    {
        [JsonProperty("poliza")]
        public string poliza { get; set; }
        [JsonProperty("endoso")]
        public Double? endoso { get; set; }
        [JsonProperty("riesgo")]
        public Double? riesgo { get; set; }
        [JsonProperty("nombreRiesgo")]
        public string nombreRiesgo { get; set; }
        [JsonProperty("vigencia")]
        public string vigencia { get; set; }
        [JsonProperty("vencimiento")]
        public string vencimiento { get; set; }
        [JsonProperty("baja")]
        public string baja { get; set; }
    }
    public class asegurados
    {
        [JsonProperty("poliza")]
        public string poliza { get; set; }
        [JsonProperty("endoso")]
        public Double? endoso { get; set; }
        [JsonProperty("riesgo")]
        public Double? riesgo { get; set; }
        [JsonProperty("tipoBenef")]
        public string tipoBenef { get; set; }
        [JsonProperty("tipoDoc")]
        public string tipoDoc { get; set; }
        [JsonProperty("codDoc")]
        public string codDoc { get; set; }
        [JsonProperty("asegurado")]
        public string asegurado { get; set; }
        [JsonProperty("domicilio")]
        public string domicilio { get; set; }
        [JsonProperty("localidad")]
        public Double? localidad { get; set; }
        [JsonProperty("postal")]
        public Double? postal { get; set; }
        [JsonProperty("provincia")]
        public Double? provincia { get; set; }
        [JsonProperty("telefonoPais")]
        public Double? telefonoPais { get; set; }
        [JsonProperty("telefonoZona")]
        public Double? telefonoZona { get; set; }
        [JsonProperty("telefono")]
        public Double? telefono { get; set; }
        [JsonProperty("domicilioCom")]
        public string domicilioCom { get; set; }
        [JsonProperty("localidadCom")]
        public Double? localidadCom { get; set; }
        [JsonProperty("postalCom")]
        public Double? postalCom { get; set; }
        [JsonProperty("provinciaCom")]
        public Double? provinciaCom { get; set; }
        [JsonProperty("nacimiento")]
        public string nacimiento { get; set; }
        [JsonProperty("iva")]
        public Double? iva { get; set; }
        [JsonProperty("mcaSexo")]
        public string mcaSexo { get; set; }
        [JsonProperty("nomina")]
        public string nomina { get; set; }
        [JsonProperty("baja")]
        public string baja { get; set; }
    }
    public class impuestos
    {
        [JsonProperty("poliza")]
        public string poliza { get; set; }
        [JsonProperty("endoso")]
        public Double? endoso { get; set; }
        [JsonProperty("primaComisionable")]
        public Decimal? primaComisionable { get; set; }
        [JsonProperty("primaNoComisionable")]
        public Decimal? primaNoComisionable { get; set; }
        [JsonProperty("derEmis")]
        public Decimal? derEmis { get; set; }
        [JsonProperty("recAdmin")]
        public Decimal? recAdmin { get; set; }
        [JsonProperty("recFinan")]
        public Decimal? recFinan { get; set; }
        [JsonProperty("bonificaciones")]
        public Decimal? bonificaciones { get; set; }
        [JsonProperty("bonifAdic")]
        public Decimal? bonifAdic { get; set; }
        [JsonProperty("otrosImptos")]
        public Decimal? otrosImptos { get; set; }
        [JsonProperty("servSociales")]
        public Decimal? servSociales { get; set; }
        [JsonProperty("imptosInternos")]
        public Decimal? imptosInternos { get; set; }
        [JsonProperty("ingBrutos")]
        public Decimal? ingBrutos { get; set; }
        [JsonProperty("premio")]
        public Decimal? premio { get; set; }
        [JsonProperty("porComision")]
        public Decimal? porComision { get; set; }
    }
}
