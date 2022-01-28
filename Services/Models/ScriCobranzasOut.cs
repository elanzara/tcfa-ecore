
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Services.Models
{
    public class ScriCobranzasOut
    {
        public Boolean HasError { get; set; }
        public Boolean HasWarning { get; set; }
        public Boolean HasInformation { get; set; }
        public ScriDetMovimintosCobranzasOut[] MovimientosCobranzas { get; set; }
        public ScriMessagesCobOut[] Messages { get; set; }
    }
    public class ScriMessagesCobOut
    {
        public string NombreServicio { get; set; }
        public string VersionServicio { get; set; }
        public string Description { get; set; }
        public string MessageBeautiful { get; set; }
        public string StackTrace { get; set; }
        public int ErrorLevel { get; set; }
    }
    public class ScriDetMovimintosCobranzasOut
    {
        public string Poliza { get; set; }
        public ScriPagosOut[] Pagos { get; set; }
    }
    public class ScriPagosOut
    {
        public string Ext_ApplicationDate { get; set; }
        public string PaymentAmount { get; set; }
        public string ReversedDate { get; set; }
    }
}
