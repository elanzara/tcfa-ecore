using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Entities
{
    public class NAccProgramas
    {
        public int Id { get; set; }
        public int? IdOrigen { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string Objeto { get; set; }
        public string Parametros { get; set; }
        public string Entidad { get; set; }
        public string Icono { get; set; }
        public string CreadoPor { get; set; }
        public DateTime CreadoEn { get; set; }
        public string AutorizadoPor { get; set; }
        public DateTime? AutorizadoEn { get; set; }
        public string Accionsql { get; set; }
    }
}
