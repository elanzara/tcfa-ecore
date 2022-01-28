using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCore.Entities
{
    public class CajaCarteraDet
    {
        public CajaCarteraDet()
        {
            //AccProgramasAcciones = new HashSet<AccProgramasAcciones>();
        }

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int IdCajaCarteraEnc { get; set; }
        public int Compania { get; set; }
        public int Seccion { get; set; }
        public int Ramo { get; set; }
        public Decimal Numero { get; set; }
        public Decimal Referencia { get; set; }
        public String Observacion { get; set; }
        public DateTime FechaVigencia { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public DateTime FechaEmision { get; set; }
        public String FormaCobro { get; set; }
        public String CBU { get; set; }
        public Decimal NumEnd { get; set; }
        public int CodMon { get; set; }
        public int CodProd { get; set; }
        public String PolizaAnterior { get; set; }
        public String Aglutinador { get; set; }
        public String Solicitud { get; set; }
        public String Negocio { get; set; }

        public virtual CajaCarteraEnc IdCajaCarteraEncNavigation { get; set; }
        public virtual ICollection<CajaCarteraCliente> CajaCarteraCliente { get; set; }
        public virtual ICollection<CajaCarteraDomicilio> CajaCarteraDomicilio { get; set; }
        public virtual ICollection<CajaCarteraDomicilioCorresp> CajaCarteraDomicilioCorresp { get; set; }
        public virtual ICollection<CajaCarteraAuto> CajaCarteraAuto { get; set; }
        public virtual ICollection<CajaCarteraAccesorio> CajaCarteraAccesorio { get; set; }
        public virtual ICollection<CajaCarteraCuota> CajaCarteraCuota { get; set; }
        public virtual ICollection<CajaCarteraCuotas> CajaCarteraCuotas { get; set; }
    }
}
