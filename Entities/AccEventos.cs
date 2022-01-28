using System;
using System.Collections.Generic;

namespace eCore.Entities
{
    public partial class AccEventos
    {
        public int Id { get; set; }
        public int IdAccUsuario { get; set; }
        public int IdAccTipoEvento { get; set; }
        public string Contexto { get; set; }
        public DateTime OcurridoEn { get; set; }

        public virtual AccTiposEventos IdAccTipoEventoNavigation { get; set; }
        public virtual AccUsuarios IdAccUsuarioNavigation { get; set; }
    }
}
