using System;
using System.Collections.Generic;

namespace eCore.Entities
{
    public class ScriTaxStatus
    {
        public ScriTaxStatus()
        {
            //AccProgramasAcciones = new HashSet<AccProgramasAcciones>();
        }

        public int Id { get; set; }
        public int idContact { get; set; }
        public string PublicID { get; set; }
        public string StatusValue { get; set; }
        public Decimal TaxPercentage { get; set; }

        public virtual ScriContact idContactNavigation { get; set; }
        public virtual ICollection<ScriEnrollementStatus> ScriEnrollementStatus { get; set; }
        public virtual ICollection<ScriRetentionAgent> ScriRetentionAgent { get; set; }
    }
}
