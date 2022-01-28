using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Apellido { get; set; }
        public string Nombres { get; set; }
        public string Sucursal { get; set; }
        public DateTime FechaSucursal { get; set; }
        public DateTime? UltimaConexion { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
}
