using System;
using System.Collections.Generic;

namespace eCore.Entities
{
    public class ScriMaillingAddressCountry
    {
        public ScriMaillingAddressCountry()
        {
            //AccProgramasAcciones = new HashSet<AccProgramasAcciones>();
        }

        public int Id { get; set; }
        public int idMaillingAddress { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        public virtual ScriMaillingAddress idMaillingAddressNavigation { get; set; }
    }
}
