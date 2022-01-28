using System;
using System.Collections.Generic;

namespace eCore.Entities
{
    public partial class AccProgramas
    {
        public AccProgramas()
        {
            AccProgramasAcciones = new HashSet<AccProgramasAcciones>();
            AccProgramasXModulos = new HashSet<AccProgramasXModulos>();
            AccProgramasXUsuario = new HashSet<AccProgramasXUsuario>();
            AccProgramasFavoritosXUsuario = new HashSet<AccProgramasFavoritosXUsuario>();
            AccProgramasRecientesXUsuario = new HashSet<AccProgramasRecientesXUsuario>();
            MAccProgramas = new HashSet<MAccProgramas>();
        }

        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string Objeto { get; set; }
        public string Parametros { get; set; }
        public string Icono { get; set; }
        public string CreadoPor { get; set; }
        public DateTime CreadoEn { get; set; }
        public string AutorizadoPor { get; set; }
        public DateTime? AutorizadoEn { get; set; }
        public string Entidad { get; set; }

        public virtual ICollection<AccProgramasAcciones> AccProgramasAcciones { get; set; }
        public virtual ICollection<AccProgramasXModulos> AccProgramasXModulos { get; set; }
        public virtual ICollection<AccProgramasXUsuario> AccProgramasXUsuario { get; set; }
        public virtual ICollection<AccProgramasFavoritosXUsuario> AccProgramasFavoritosXUsuario { get; set; }
        public virtual ICollection<AccProgramasRecientesXUsuario> AccProgramasRecientesXUsuario { get; set; }
        public virtual ICollection<MAccProgramas> MAccProgramas { get; set; }
    }
}
