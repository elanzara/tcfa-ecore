using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCore.Entities
{
    public class CajaCarteraDomicilio
    {
        public CajaCarteraDomicilio()
        {
            //AccProgramasAcciones = new HashSet<AccProgramasAcciones>();
        }

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int IdCajaCarteraEnc { get; set; }
        public String Direccion { get; set; }
        public String Localidad { get; set; }
        public String CodigoPostal { get; set; }
        public int CodigoProvincia { get; set; }

        public virtual CajaCarteraDet IdCajaCarteraEncNavigation { get; set; }
    }
}

