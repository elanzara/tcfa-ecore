using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Services
{
    public class SDTWSNovedadesIn
    {
        [JsonProperty("Articulo")]
        public int Articulo { get; set; }

        [JsonProperty("Certificado")]
        public string Certificado { get; set; }

        [JsonProperty("FechaDesde")]
        public string FechaDesde { get; set; }

        [JsonProperty("FechaHasta")]
        public string FechaHasta { get; set; }

        [JsonProperty("Productor")]
        public _Productor Productor { get; set; }

    }
}
