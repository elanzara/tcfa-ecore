using System;
using System.Collections.Generic;

namespace eCore.Entities
{
    public partial class AccTiposEventos
    {
        public AccTiposEventos()
        {
            AccEventos = new HashSet<AccEventos>();
        }

        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string Tipo { get; set; }
        public string Icono { get; set; }
        public string CreadoPor { get; set; }
        public DateTime CreadoEn { get; set; }
        public string AutorizadoPor { get; set; }
        public DateTime? AutorizadoEn { get; set; }

        public virtual ICollection<AccEventos> AccEventos { get; set; }
    }
}
