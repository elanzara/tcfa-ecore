using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Models
{
    public class PanelCartasProgramasDTO
    {
        public PanelProgramasDTO panel_programas { get; set; }
        public List<ProgramasDTO> cartas_programas { get; set; }
    }
}
