using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using eCore.Services.Models;

namespace eCore.Entities
{
    public class ApicoreCompraSeguro
    {
        public ApicoreCompraSeguro()
        {
            //AccProgramasAcciones = new HashSet<AccProgramasAcciones>();
        }

        //[DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public Int64 id { get; set; }
        public string cobertura { get; set; }
        public string compania { get; set; }
        public DateTime createdAt { get; set; }
        public string estado { get; set; }
        public string externalCoberturaId { get; set; }
        public Int64 externalCotizacionId { get; set; }
        public string nroPoliza { get; set; }
        public Decimal? primaPoliza { get; set; }
        public string trackId { get; set; }
        public DateTime? updatedAt { get; set; }
        public Int64? auto_id { get; set; }
        public Int64? persona_id { get; set; }
        public Int64? user_id { get; set; }
        public Int64? medioPago_id { get; set; }
        public DateTime? fechaFuturaCotizacion { get; set; }

        //public virtual TrfDetallePremio IdTrfDetallePremioNavigation { get; set; }
    }
}
