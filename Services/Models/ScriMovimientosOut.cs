
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Services.Models
{
    public class ScriMovimientosOut
    {
        public Boolean HasError { get; set; }
        public Boolean HasWarning { get; set; }
        public Boolean HasInformation { get; set; }
        public ScriListJobSummaryOut[] ListJobSummary { get; set; }
        public ScriMessagesOut[] Messages { get; set; }
    }
    public class ScriMessagesOut
    {
        public string NombreServicio { get; set; }
        public string VersionServicio { get; set; }
        public string Description { get; set; }
        public string MessageBeautiful { get; set; }
        public string StackTrace { get; set; }
        public int ErrorLevel { get; set; }
    }
    public class ScriListJobSummaryOut
    {
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
        public ScriOfferingOut Offering { get; set; }
        public ScriPolicyTypeOut PolicyType { get; set; }
        public ScriProductOut Product { get; set; }
    }
    public class ScriOfferingOut
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }
    public class ScriPolicyTypeOut
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }
    public class ScriProductOut
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }
}
