using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCore.Entities
{
    public class CajaCarteraAuto
    {
        public CajaCarteraAuto()
        {
            //AccProgramasAcciones = new HashSet<AccProgramasAcciones>();
        }

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int IdCajaCarteraEnc { get; set; }
        public String Patente { get; set; }
        public int Marca { get; set; }
        public String DescMarca { get; set; }
        public int Modelo { get; set; }
        public Decimal SumaAsegurada { get; set; }
        public int TipoVehiculo { get; set; }
        public int UsoVehiculo { get; set; }
        public int ClaseVehiculo { get; set; }
        public String Motor { get; set; }
        public String Chasis { get; set; }
        public String McaCeroKm { get; set; }
        public int CodInfoauto { get; set; }

        public virtual CajaCarteraDet IdCajaCarteraEncNavigation { get; set; }
    }
}

