using System;
using System.Collections.Generic;
using eCore.Services.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCore.Entities
{

    public partial class ScriMessages
    {
        public ScriMessages()
        {
            //AccProgramasAcciones = new HashSet<AccProgramasAcciones>();
        }

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int IdScriMovimientos { get; set; }

        public string NombreServicio { get; set; }
        public string VersionServicio { get; set; }
        public string Description { get; set; }
        public string MessageBeautiful { get; set; }
        public string StackTrace { get; set; }
        public int ErrorLevel { get; set; }

        public virtual ScriMovimientos IdScriMovimientosNavigation { get; set; }
    }
}
