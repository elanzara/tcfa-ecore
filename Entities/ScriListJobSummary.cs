using System;
using System.Collections.Generic;
using eCore.Services.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCore.Entities
{

    public partial class ScriListJobSummary
    {
        public ScriListJobSummary()
        {
            //AccProgramasAcciones = new HashSet<AccProgramasAcciones>();
        }

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int IdScriMovimientos { get; set; }

        public string OfferingPlan { get; set; }
        public string PolicyPeriodID { get; set; }
        public string ScopeCoverage { get; set; }
        public DateTime StartDate { get; set; }
        public string Status { get; set; }
        public string TransactionJob { get; set; }
        public string Subtype { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime PeriodEnd { get; set; }
        public DateTime PolicyStartDate { get; set; }
        public string PolicyNumber { get; set; }

        public virtual ICollection<ScriOffering> ScriOffering { get; set; }
        public virtual ICollection<ScriPolicyType> ScriPolicyType { get; set; }
        public virtual ICollection<ScriProduct> ScriProduct { get; set; }

        public virtual ScriMovimientos IdScriMovimientosNavigation { get; set; }
    }
}
