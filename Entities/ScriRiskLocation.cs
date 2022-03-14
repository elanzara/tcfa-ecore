using System;
using System.Collections.Generic;

namespace eCore.Entities
{
    public class ScriRiskLocation
    {
        public ScriRiskLocation()
        {
            //AccProgramasAcciones = new HashSet<AccProgramasAcciones>();
        }

        public int Id { get; set; }
        public int idVehicle { get; set; }
        public string City { get; set; }
        public string DisplayName { get; set; }
        public string PostalCode { get; set; }
        public string PublicID { get; set; }
        public string Street { get; set; }

        public virtual ScriVehicle idVehicleNavigation { get; set; }
    }
}
