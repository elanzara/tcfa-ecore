using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Models
{
    public class PanelMenuDTO
    {
        public string codigo { get; set; }
        public string descripcion { get; set; }
        public List<ModulosDTO> modulos { get; set; }
    }
}
