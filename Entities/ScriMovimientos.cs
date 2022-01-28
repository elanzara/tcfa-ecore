using System;
using System.Collections.Generic;
using eCore.Services.Models;

namespace eCore.Entities
{

    public partial class ScriMovimientos
    {
        public ScriMovimientos()
        {
            //AccProgramasAcciones = new HashSet<AccProgramasAcciones>();
        }

        public int Id { get; set; }
        public Boolean HasError { get; set; }
        public Boolean HasWarning { get; set; }
        public Boolean HasInformation { get; set; }

        public virtual ICollection<ScriListJobSummary> ScriListJobSummary { get; set; }
        public virtual ICollection<ScriMessages> ScriMessages { get; set; }

    }
}
