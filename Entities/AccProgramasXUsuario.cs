using System;
using System.Collections.Generic;

namespace eCore.Entities
{
    public partial class AccProgramasXUsuario
    {
        public int Id { get; set; }
        public string Tipo { get; set; }
        public int IdAccPrograma { get; set; }
        public int IdAccUsuario { get; set; }
        public string CreadoPor { get; set; }
        public DateTime CreadoEn { get; set; }

        public virtual AccUsuarios IdAccUsuarioNavigation { get; set; }
        public virtual AccProgramas IdAccProgramaNavigation { get; set; }

    }
}
