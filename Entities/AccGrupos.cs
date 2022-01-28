using System;
using System.Collections.Generic;

namespace eCore.Entities
{
    public partial class AccGrupos
    {
        public AccGrupos()
        {
            AccGruposXPerfil = new HashSet<AccGruposXPerfil>();
            AccProgramasAccionesXGrupo = new HashSet<AccProgramasAccionesXGrupo>();
            MaccGruposXPerfilIdAccGrupoNavigation= new HashSet<MAccGruposXPerfil>();
        }

        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string Icono { get; set; }
        public string CreadoPor { get; set; }
        public DateTime CreadoEn { get; set; }
        public string AutorizadoPor { get; set; }
        public DateTime? AutorizadoEn { get; set; }

        public virtual ICollection<AccGruposXPerfil> AccGruposXPerfil { get; set; }
        public virtual ICollection<AccProgramasAccionesXGrupo> AccProgramasAccionesXGrupo { get; set; }
        public virtual ICollection<MAccGruposXPerfil> MaccGruposXPerfilIdAccGrupoNavigation { get; set; }
    }
}
