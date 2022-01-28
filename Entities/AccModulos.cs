using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCore.Entities
{
    public partial class AccModulos
    {
        public AccModulos()
        {
            MAccModulosIdAccModuloNavigation = new HashSet<MAccModulos>();
            MAccModulosIdOrigenNavigation = new HashSet<MAccModulos>();
            AccProgramasXModulos = new HashSet<AccProgramasXModulos>();
            InverseIdAccModuloNavigation = new HashSet<AccModulos>();
        }

        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string Icono { get; set; }
        public int? IdAccModulo { get; set; }
        public string CreadoPor { get; set; }
        public DateTime CreadoEn { get; set; }
        public string AutorizadoPor { get; set; }
        public DateTime? AutorizadoEn { get; set; }

        public virtual AccModulos IdAccModuloNavigation { get; set; }
        public virtual ICollection<AccProgramasXModulos> AccProgramasXModulos { get; set; }
        public virtual ICollection<AccModulos> InverseIdAccModuloNavigation { get; set; }
        public virtual ICollection<MAccModulos> MAccModulosIdAccModuloNavigation { get; set; }
        public virtual ICollection<MAccModulos> MAccModulosIdOrigenNavigation { get; set; }
    }
}
