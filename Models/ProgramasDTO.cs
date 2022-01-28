using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Models
{
    public class ProgramasDTO
    {
        public string id { get; set; }
        public string codigo { get; set; }
        public string descripcion { get; set; }
        public string parametros { get; set; }
        public string controller { get; set; }
        public string icono { get; set; }
        public string entidad { get; set; }
        public int cnt_total { get; set; }
        public int cnt_mios { get; set; }
        public int cnt_autorizar { get; set; }
        public DateTime? ultimo_uso { get; set; }
    }
}
