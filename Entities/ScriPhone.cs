using System;
using System.Collections.Generic;

namespace eCore.Entities
{
    public class ScriPhone
    {
        public ScriPhone()
        {
            //AccProgramasAcciones = new HashSet<AccProgramasAcciones>();
        }

        public int Id { get; set; }
        public int idContact { get; set; }
        public string PhoneNumber { get; set; }

        public virtual ScriContact idContactNavigation { get; set; }
        public virtual ICollection<ScriPhoneType> ScriPhoneType { get; set; }
    }
}
