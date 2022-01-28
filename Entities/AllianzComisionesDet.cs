using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCore.Entities
{

    public partial class AllianzComisionesDet
    {
        public AllianzComisionesDet()
        {
            //AccProgramasAcciones = new HashSet<AccProgramasAcciones>();
        }

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int IdAllianzComisionesEnc { get; set; }
        public string Organizador { get; set; }
        public string Productor { get; set; }
        public string Tipo { get; set; }
        public DateTime Fecha { get; set; }
        public string Seccion { get; set; }
        public string NroPoliza { get; set; }
        public int Endoso { get; set; }
        public string Asegurado { get; set; }
        public string Mda { get; set; }
        public int TipoCambio { get; set; }
        public Decimal Premio { get; set; }
        public Decimal Prima { get; set; }
        public Decimal ComisionesDevengadas { get; set; }
        public Decimal ComisionesDevengadasPesos { get; set; }
        public Decimal OSSEG { get; set; }
        public Decimal IBAgente { get; set; }
        public Decimal IBRiesgo { get; set; }
        public string ProvinciaRiesgo { get; set; }
        public Decimal NetoAcreditado { get; set; }
        public Decimal NetoAcreditadoPesos { get; set; }
        public string FPago { get; set; }

        public virtual AllianzComisionesEnc IdAllianzComisionesEncNavigation { get; set; }

    }
}

