using System;
using System.Collections.Generic;

namespace eCore.Entities
{
    public partial class AccProgramasAcciones
    {
        public AccProgramasAcciones()
        {
            AccProgramasAccionesXGrupo = new HashSet<AccProgramasAccionesXGrupo>();
        }

        public int Id { get; set; }
        public int IdAccPrograma { get; set; }
        public int IdAccAccion { get; set; }
        public string Origen { get; set; }
        public string Icono { get; set; }
        public string CreadoPor { get; set; }
        public DateTime CreadoEn { get; set; }
        public string AutorizadoPor { get; set; }
        public DateTime? AutorizadoEn { get; set; }

        public virtual AccProgramas IdAccProgramaNavigation { get; set; }
        public virtual AccAcciones IdAccAccionNavigation { get; set; }
        public virtual ICollection<AccProgramasAccionesXGrupo> AccProgramasAccionesXGrupo { get; set; }
    }
}
