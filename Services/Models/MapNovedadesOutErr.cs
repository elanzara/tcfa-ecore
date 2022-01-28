using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Services.Models
{
    public class MapNovedadesOutErr
    {
        [JsonProperty("error")]
        public error error { get; set; }
    }
    public class error //Novedad
    {
        [JsonProperty("code")]
        public string code { get; set; }
        [JsonProperty("message")]
        public string message { get; set; }
    }
}
