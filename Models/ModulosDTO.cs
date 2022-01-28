using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Models
{
    public class ModulosDTO
    {
        public string id { get; set; }
        public string codigo { get; set; }
        public string descripcion { get; set; }
        public string parametros { get; set; }
        public string controller { get; set; }
        public string icono { get; set; }
        public List<ProgramasDTO> programas { get; set; }
        public List<ModulosDTO> modulos { get; set; }
    }
}
