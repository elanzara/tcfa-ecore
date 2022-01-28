using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCore.Entities
{
    public class CajaCarteraCliente
    {
        public CajaCarteraCliente()
        {
            //AccProgramasAcciones = new HashSet<AccProgramasAcciones>();
        }

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int IdCajaCarteraEnc { get; set; }
        public String TipoDocumento { get; set; }
        public Decimal NroDocumento { get; set; }
        public String Apellido { get; set; }
        public String Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int CodIva { get; set; }
        public String Sexo { get; set; }
        public String EstCivil { get; set; }

        public virtual CajaCarteraDet IdCajaCarteraEncNavigation { get; set; }
    }
}
