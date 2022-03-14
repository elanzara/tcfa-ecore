using System;
using System.Collections.Generic;

namespace eCore.Entities
{
    public class ScriAutomaticAdjust
    {
        public ScriAutomaticAdjust()
        {
            //AccProgramasAcciones = new HashSet<AccProgramasAcciones>();
        }

        public int Id { get; set; }
        public int idVehicle { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        public virtual ScriVehicle idVehicleNavigation { get; set; }
    }
}
