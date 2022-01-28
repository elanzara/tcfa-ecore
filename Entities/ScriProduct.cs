using System;
using System.Collections.Generic;
using eCore.Services.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCore.Entities
{

    public partial class ScriProduct
    {
        public ScriProduct()
        {
            //AccProgramasAcciones = new HashSet<AccProgramasAcciones>();
        }

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int IdScriListJobSummary { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        public virtual ScriListJobSummary IdScriListJobSummaryNavigation { get; set; }
    }
}
