using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using eCore.Services.Models;


namespace eCore.Entities
{
    public class TrfVehiculoDatos
    {
        public TrfVehiculoDatos()
        {
            //AccProgramasAcciones = new HashSet<AccProgramasAcciones>();
        }

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int IdTrfNovedades { get; set; }
        public int Anio { get; set; }
        public int CeroKm { get; set; }
        public string Chasis { get; set; }
        public string Cobertura { get; set; }
        //public Datosgnc DatosGNC { get; set; }
        public string Dominio { get; set; }
        public string Marca { get; set; }
        public int MarcaIA { get; set; }
        public string Modelo { get; set; }
        public int ModeloIA { get; set; }
        public string Motor { get; set; }
        public string Origen { get; set; }
        public string SubModelo { get; set; }
        public Decimal SumaAsegurada { get; set; }
        public string Tipo { get; set; }
        public int TipoN { get; set; }
        public string Uso { get; set; }
        public int UsoN { get; set; }

        //public virtual ICollection<TrfRama> TrfRama { get; set; }
        public virtual TrfNovedades IdTrfNovedadesNavigation { get; set; }
    }
}

