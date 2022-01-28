using System;
using System.Collections.Generic;

namespace eCore.Entities
{
    public partial class AccPerfiles
    {
        public AccPerfiles()
        {
            MAccPerfiles = new HashSet<MAccPerfiles>();
            AccGruposXPerfil = new HashSet<AccGruposXPerfil>();
            AccUsuarios = new HashSet<AccUsuarios>();
            MAccUsuariosIdAccPerfilNavigation = new HashSet<MAccUsuarios>();
            MaccGruposXPerfilIdAccPerfilNavigation = new HashSet<MAccGruposXPerfil>();
        }

        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string Icono { get; set; }
        public string CreadoPor { get; set; }
        public DateTime CreadoEn { get; set; }
        public string AutorizadoPor { get; set; }
        public DateTime? AutorizadoEn { get; set; }

        public virtual ICollection<MAccPerfiles> MAccPerfiles { get; set; }
        public virtual ICollection<AccGruposXPerfil> AccGruposXPerfil { get; set; }
        public virtual ICollection<AccUsuarios> AccUsuarios { get; set; }
        public virtual ICollection<MAccUsuarios> MAccUsuariosIdAccPerfilNavigation { get; set; }
        public virtual ICollection<MAccGruposXPerfil> MaccGruposXPerfilIdAccPerfilNavigation { get; set; }
    }
}
