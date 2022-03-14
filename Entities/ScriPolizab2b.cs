using System;
using System.Collections.Generic;


namespace eCore.Entities
{
    public class ScriPolizab2b
    {

        public ScriPolizab2b()
        {
            //AccProgramasAcciones = new HashSet<AccProgramasAcciones>();
        }

        public int Id { get; set; }
        public string AlternativaComercial { get; set; }
        public string Updated { get; set; }
        public string HasError { get; set; }
        public string HasWarning { get; set; }
        public string HasInformation { get; set; }
        public string Messages { get; set; }

        //public List<AllianzCarteraDet> allianzCarteraDets { get; set; }
        //public virtual ICollection<AllianzCarteraDet> AllianzCarteraDet { get; set; }


        public virtual ICollection<ScriPolicy> ScriPolicy { get; set; }
        
    }
}
