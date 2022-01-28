using System;
using System.Collections.Generic;

namespace eCore.Entities
{
    public partial class AccProgramasAccionesXGrupo
    {
        public int Id { get; set; }
        public int IdProgramaAccion { get; set; }
        public int IdAccGrupo { get; set; }
        public string Icono { get; set; }
        public string CreadoPor { get; set; }
        public DateTime CreadoEn { get; set; }
        public string AutorizadoPor { get; set; }
        public DateTime? AutorizadoEn { get; set; }

        public virtual AccGrupos IdAccGrupoNavigation { get; set; }
        public virtual AccProgramasAcciones IdProgramaAccionNavigation { get; set; }
    }
}
