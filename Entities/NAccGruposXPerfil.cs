using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities
{
    public class NAccGruposXPerfil
    {
        public int Id { get; set; }
        public int? IdOrigen { get; set; }
        public int IdAccPerfil { get; set; }
        public int IdAccGrupo { get; set; }
        public string CreadoPor { get; set; }
        public DateTime CreadoEn { get; set; }
        public string AutorizadoPor { get; set; }
        public DateTime? AutorizadoEn { get; set; }
        public string Accionsql { get; set; }
    }
}
