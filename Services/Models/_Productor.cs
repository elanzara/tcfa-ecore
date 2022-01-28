
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Services
{
    public class _Productor
    {
        [JsonProperty("Codigo")]
        public string Codigo { get; set; }

        [JsonProperty("Password")]
        public string Password { get; set; }

        [JsonProperty("Usuario")]
        public string Usuario { get; set; }

    }
}
