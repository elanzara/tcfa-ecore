using System;
using System.Collections.Generic;

namespace eCore.Entities
{
    public class ScriMaritalStatus
    {
        public ScriMaritalStatus()
        {
            //AccProgramasAcciones = new HashSet<AccProgramasAcciones>();
        }

        public int Id { get; set; }
        public int idContact { get; set; }
        public string Code { get; set; }
        public string DisplayName { get; set; }

        public virtual ScriContact idContactNavigation { get; set; }
    }
}
