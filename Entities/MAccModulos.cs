﻿using System;
using System.Collections.Generic;

namespace eCore.Entities
{
    public partial class MAccModulos
    {
        public int Id { get; set; }
        public int? IdOrigen { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string Icono { get; set; }
        public int? IdAccModulo { get; set; }
        public string CreadoPor { get; set; }
        public DateTime CreadoEn { get; set; }
        public string AutorizadoPor { get; set; }
        public DateTime? AutorizadoEn { get; set; }
        public string Modifica { get; set; }

        public virtual AccModulos IdOrigenNavigation { get; set; }
        public virtual AccModulos IdAccModuloNavigation { get; set; }
    }
}
