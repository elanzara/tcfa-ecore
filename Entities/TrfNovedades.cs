using System;
using System.Collections.Generic;
using eCore.Services.Models;

namespace eCore.Entities
{

    public partial class TrfNovedades
    {
        public TrfNovedades()
        {
            //AccProgramasAcciones = new HashSet<AccProgramasAcciones>();
        }

        public int Id { get; set; }
        public int Articulo { get; set; }
        public int ArticuloAnt { get; set; }
        public string CUIT { get; set; }
        public string Certificado { get; set; }
        public string CertificadoAnt { get; set; }
        public int CodigoPostal { get; set; }
        public string CondicionIVA { get; set; }
        public int CondicionIVAN { get; set; }
        //public TrfDetallePremio TrfDetallePremio { get; set; }
        public string DocNumero { get; set; }
        public int DocTipo { get; set; }
        public string Domicilio { get; set; }
        public string Email { get; set; }
        public string Empresa { get; set; }
        public string EmpresaAnt { get; set; }
        public string EstadoPoliza { get; set; }
        public string EstadoPolizaN { get; set; }
        public string Moneda { get; set; }
        public string RazonSocial { get; set; }
        //public Sdtcomision[] SDTComision { get; set; }
        //public Sdtcuota[] SDTCuota { get; set; }
        //public Sdtvehiculodato[] SDTVehiculoDatos { get; set; }
        public int SubCodigoPostal { get; set; }
        public string Sucursal { get; set; }
        public string SucursalAnt { get; set; }
        public int Suplemento { get; set; }
        public string Telefono { get; set; }
        public string TelefonoParticular { get; set; }
        public string VigenciaDesde { get; set; }
        public string VigenciaHasta { get; set; }
        public string codigo_productor { get; set; }

        public virtual ICollection<TrfDetallePremio> TrfDetallePremio { get; set; }
        public virtual ICollection<TrfSdtcomision> TrfSdtcomision { get; set; }
        public virtual ICollection<TrfSdtcuota> TrfSdtcuota { get; set; }
        public virtual ICollection<TrfVehiculoDatos> TrfVehiculoDatos { get; set; }
    }
}

