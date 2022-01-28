using System;
using System.Collections.Generic;

namespace eCore.Entities
{
    public partial class AccGruposXPerfil
    {
        public AccGruposXPerfil()
        {
            MAccGruposXPerfilIdOrigenNavigation = new HashSet<MAccGruposXPerfil>();
        }

        public int Id { get; set; }
        public int IdAccPerfil { get; set; }
        public int IdAccGrupo { get; set; }
        public string CreadoPor { get; set; }
        public DateTime CreadoEn { get; set; }
        public string AutorizadoPor { get; set; }
        public DateTime? AutorizadoEn { get; set; }

        public virtual AccGrupos IdAccGrupoNavigation { get; set; }
        public virtual AccPerfiles IdAccPerfilNavigation { get; set; }
        public virtual ICollection<MAccGruposXPerfil> MAccGruposXPerfilIdOrigenNavigation { get; set; }
    }
}
