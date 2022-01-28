using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities
{
    public class LAccUsuarios
    {
        public int Id { get; set; }
        public int? IdOrigen { get; set; }
        public int IdAccPerfil { get; set; }
        public string SecurityIdentifier { get; set; }
        public string AdCuenta { get; set; }
        public string Apellido { get; set; }
        public string Nombres { get; set; }
        public int MaxCantidadConexiones { get; set; }
        public int Bloqueado { get; set; }
        public string Sucursal { get; set; }
        public DateTime? UltimaConexion { get; set; }
        public string CreadoPor { get; set; }
        public DateTime CreadoEn { get; set; }
        public string AutorizadoPor { get; set; }
        public DateTime? AutorizadoEn { get; set; }
        public string Accionsql { get; set; }
    }
}
