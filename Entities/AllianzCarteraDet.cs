using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCore.Entities
{

    public partial class AllianzCarteraDet
    {
        public AllianzCarteraDet()
        {
            //AccProgramasAcciones = new HashSet<AccProgramasAcciones>();
        }

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int IdAllianzCarteraEnc { get; set; }
        public string Productor { get; set; }
        public string Organizador { get; set; }
        public string Seccion { get; set; }
        public string Poliza { get; set; }
        public int Endoso { get; set; }
        public string ClaseEndoso { get; set; }
        public string NombreDelAsegurado { get; set; }
        public DateTime FecEmision { get; set; }
        public DateTime FechaDesdeVigencia { get; set; }
        public DateTime FechaHastaVigencia { get; set; }
        public string Estado { get; set; }
        public string Moneda { get; set; }
        public Decimal TotalPrima { get; set; }
        public Decimal ComisionOrg { get; set; }
        public Decimal ComisionProd { get; set; }
        public Decimal TotalPremio { get; set; }
        public Decimal TotalPagado { get; set; }
        public Decimal Saldo { get; set; }
        public int CantSiniestros { get; set; }
        public int CantDenuncias { get; set; }
        public string TipoDeDocumento { get; set; }
        public string NumeroDeDocumento { get; set; }
        public int CantCuotas { get; set; }
        public string FormaDeCobro { get; set; }
        public string TipoOperacion { get; set; }
        public Decimal DerechoEmision { get; set; }
        public Decimal GastosFinanc { get; set; }
        public Decimal Iva { get; set; }
        public Decimal Sellos { get; set; }
        public Decimal GastosAdm { get; set; }
        public Decimal SumaAsegurada { get; set; }
        public Decimal ValorDeReferencia { get; set; }
        public string TipoPoliza { get; set; }
        public string Cuatrimestre { get; set; }
        public string EstadoSolicitud { get; set; }
        public DateTime FechaDespImp { get; set; }
        public string Propuesta { get; set; }
        public string Linea { get; set; }
        public string PolizaRenovada { get; set; }
        public int CantCuotas2 { get; set; }
        public Decimal Porc1erCuota { get; set; }
        public int Venc1eraCuota { get; set; }
        public string PlanPago { get; set; }
        public Decimal NroInterno { get; set; }
        public DateTime FechaVtoPoliza { get; set; }
        public string Patente { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Motor { get; set; }
        public string Chasis { get; set; }
        public string Uso { get; set; }
        public string Cobertura { get; set; }
        public Decimal SumaAsegurada2 { get; set; }
        public Decimal ValorDeReferencia2 { get; set; }
        public int Item { get; set; }
        public Decimal Infoauto { get; set; }
        public DateTime FechaFinPrestamo { get; set; }

        public virtual AllianzCarteraEnc IdAllianzCarteraEncNavigation { get; set; }

        //public virtual ICollection<AccProgramasAcciones> AccProgramasAcciones { get; set; }
    }
}

