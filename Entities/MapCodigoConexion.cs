using System;
using System.Collections.Generic;
using eCore.Services.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCore.Entities
{
    public class MapCodigoConexion
    {
        public MapCodigoConexion()
        {
            //AccProgramasAcciones = new HashSet<AccProgramasAcciones>();
        }

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string codAgt { get; set; }
        public string claveAcceso { get; set; }
        public string claveProcedencia { get; set; }
    }
}
