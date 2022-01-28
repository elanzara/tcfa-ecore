using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using eCore.Services.Models;


namespace eCore.Entities
{
    public partial class TrfRama
    {
        public TrfRama()
        {
            //AccProgramasAcciones = new HashSet<AccProgramasAcciones>();
        }

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int IdTrfDetallePremio { get; set; }
        public Decimal Bonificacion { get; set; }
        public Decimal CuotasSociales { get; set; }
        public Decimal DerechoEmiFijo { get; set; }
        public Decimal DerechoEmision { get; set; }
        public Decimal IIBBEmpresa { get; set; }
        public Decimal IIBBPercepcion { get; set; }
        public Decimal IIBBRiesgo { get; set; }
        public Decimal IVA { get; set; }
        public Decimal IVAPercepcion { get; set; }
        public Decimal IVARNI { get; set; }
        public Decimal ImpInternos { get; set; }
        public Decimal LeyEmergVial { get; set; }
        public Decimal Premio { get; set; }
        public Decimal Prima { get; set; }
        public Decimal PrimaNeta { get; set; }
        public int rama { get; set; }
        public Decimal RecargoAdm { get; set; }
        public Decimal RecargoFin { get; set; }
        public Decimal RecuperoGastosAsoc { get; set; }
        public Decimal SelladoEmpresa { get; set; }
        public Decimal SelladoRiesgo { get; set; }
        public Decimal ServiciosSociales { get; set; }
        public Decimal TasaSSN { get; set; }


        public virtual TrfDetallePremio IdTrfDetallePremioNavigation { get; set; }
    }
}
