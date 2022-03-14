using System;
using System.Collections.Generic;

namespace eCore.Entities
{
    public class ScriReasonCancelDTO
    {
        public ScriReasonCancelDTO()
        {
            //AccProgramasAcciones = new HashSet<AccProgramasAcciones>();
        }

        public int Id { get; set; }
        public int idPolicy { get; set; }
        public string ReasonCode { get; set; }
        public string ReasonDescrip { get; set; }
        public DateTime CancellationDate { get; set; }

        public virtual ScriPolicy idPolicyNavigation { get; set; }
        
    }
}
