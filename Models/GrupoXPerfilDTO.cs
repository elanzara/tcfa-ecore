using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Models
{
    public class GrupoXPerfilDTO
    {
        public int id { get; set; }
        public int idAccPerfil { get; set; }
        public int idAccGrupo { get; set; }
        public PerfilDTO idAccPerfilNavigation { get; set; }
        public GrupoDTO idAccGrupoNavigation { get; set; }
    }
}
