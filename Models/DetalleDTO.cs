using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Models
{
    public class DetalleDTO<T>
    {
        public List<T> datos { get; set; }
        public List<AccionesDTO> acciones { get; set; }

    }
}
