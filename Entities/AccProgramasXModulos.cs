using System;
using System.Collections.Generic;

namespace eCore.Entities
{
    public partial class AccProgramasXModulos
    {
        public int Id { get; set; }
        public int IdAccPrograma { get; set; }
        public int IdAccModulo { get; set; }
        public string Icono { get; set; }
        public string CreadoPor { get; set; }
        public DateTime CreadoEn { get; set; }
        public string AutorizadoPor { get; set; }
        public DateTime? AutorizadoEn { get; set; }

        public virtual AccModulos IdAccModuloNavigation { get; set; }
        public virtual AccProgramas IdAccProgramaNavigation { get; set; }
    }
}
