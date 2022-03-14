using System;
using System.Collections.Generic;

namespace eCore.Entities
{
    public class ScriPhoneType
    {
        public ScriPhoneType()
        {
            //AccProgramasAcciones = new HashSet<AccProgramasAcciones>();
        }

        public int Id { get; set; }
        public int idPhone { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        public virtual ScriPhone idPhoneNavigation { get; set; }
    }
}
