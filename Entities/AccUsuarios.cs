using System;
using System.Collections.Generic;

namespace eCore.Entities
{
    public partial class AccUsuarios
    {
        public AccUsuarios()
        {
            AccEventos = new HashSet<AccEventos>();
            AccProgramasXUsuario = new HashSet<AccProgramasXUsuario>();
            AccUsuariosSesiones = new HashSet<AccUsuariosSesiones>();
            AccProgramasFavoritosXUsuario = new HashSet<AccProgramasFavoritosXUsuario>();
            AccProgramasRecientesXUsuario = new HashSet<AccProgramasRecientesXUsuario>();
            MAccUsuariosIdOrigenNavigation = new HashSet<MAccUsuarios>();
        }

        public int Id { get; set; }
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

        public virtual AccPerfiles IdAccPerfilNavigation { get; set; }
        public virtual ICollection<AccEventos> AccEventos { get; set; }
        public virtual ICollection<AccProgramasXUsuario> AccProgramasXUsuario { get; set; }
        public virtual ICollection<AccUsuariosSesiones> AccUsuariosSesiones { get; set; }
        public virtual ICollection<AccProgramasFavoritosXUsuario> AccProgramasFavoritosXUsuario { get; set; }
        public virtual ICollection<AccProgramasRecientesXUsuario> AccProgramasRecientesXUsuario { get; set; }
        public virtual ICollection<MAccUsuarios> MAccUsuariosIdOrigenNavigation { get; set; }
    }
}
