using eCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Models
{
    public class PerspectivaDTO<T, V>
    {
        public T Perspectiva { get; set; }
        public List<V> Relacionados { get; set; }
    }
}
