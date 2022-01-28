using System;
using System.Collections.Generic;
using eCore.Services.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCore.Entities
{
    public class CodigoProductor
    {
        public CodigoProductor()
        {
            //AccProgramasAcciones = new HashSet<AccProgramasAcciones>();
        }

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ID_ws_broker_compania { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string codigo_productor { get; set; }
        public string usuario { get; set; }
        public string clave  { get; set; }
    }
}
