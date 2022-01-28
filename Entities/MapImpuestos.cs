using System;
using System.Collections.Generic;
using eCore.Services.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCore.Entities
{

    public partial class MapImpuestos
    {
        public MapImpuestos()
        {
            //AccProgramasAcciones = new HashSet<AccProgramasAcciones>();
        }

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int IdMapNovedades { get; set; }
        public string poliza { get; set; }
        public double? endoso { get; set; }
        public Decimal? primaComisionable { get; set; }
        public Decimal? primaNoComisionable { get; set; }
        public Decimal? derEmis { get; set; }
        public Decimal? recAdmin { get; set; }
        public Decimal? recFinan { get; set; }
        public Decimal? bonificaciones { get; set; }
        public Decimal? bonifAdic { get; set; }
        public Decimal? otrosImptos { get; set; }
        public Decimal? servSociales { get; set; }
        public Decimal? imptosInternos { get; set; }
        public Decimal? ingBrutos { get; set; }
        public Decimal? premio { get; set; }
        public Decimal? porComision { get; set; }

        public virtual MapNovedades IdMapNovedadesNavigation { get; set; }
    }
}
