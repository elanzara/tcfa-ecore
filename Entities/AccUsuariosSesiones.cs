using System;
using System.Collections.Generic;

namespace eCore.Entities
{
    public partial class AccUsuariosSesiones
    {
        public int Id { get; set; }
        public int IdAccUsuario { get; set; }
        public string Token { get; set; }
        public DateTime CreadoEn { get; set; }
        public DateTime? FinalizaEn { get; set; }
        public DateTime? UltimaConexion { get; set; }
        public string Eventos { get; set; }

        public virtual AccUsuarios IdAccUsuarioNavigation { get; set; }
    }
}
