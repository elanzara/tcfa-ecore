using System;
using System.Collections.Generic;

namespace eCore.Entities
{
    public class ScriPaymentMethod
    {
        public ScriPaymentMethod()
        {
            //AccProgramasAcciones = new HashSet<AccProgramasAcciones>();
        }

        public int Id { get; set; }
        public int idPolicy { get; set; }
        public string Description { get; set; }
        public string IdentificationCode { get; set; }
        public string Selected { get; set; }
        public string CBUTarjetaCredito { get; set; }

        public virtual ScriPolicy idPolicyNavigation { get; set; }
    }
}
