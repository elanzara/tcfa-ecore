using System;
using System.Collections.Generic;

namespace eCore.Entities
{
    public partial class AccProgramasRecientesXUsuario
    {
        public int Id { get; set; }
        public int IdAccPrograma { get; set; }
        public int IdAccUsuario { get; set; }
        public DateTime Fecha { get; set; }

        public virtual AccUsuarios IdAccUsuarioNavigation { get; set; }
        public virtual AccProgramas IdAccProgramaNavigation { get; set; }
    }
}
