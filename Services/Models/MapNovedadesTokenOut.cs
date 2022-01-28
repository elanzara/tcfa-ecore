using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Services.Models
{
    public class MapNovedadesTokenOut
    {
        public DataOut data { get; set; }
    }
    public class DataOut
    {
        public string accessToken { get; set; }
    }
}
