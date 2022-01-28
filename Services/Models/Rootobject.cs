using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Services.Models
{
        // If the response contains content we want to read it!
        public class Rootobject
        {
            public Sdtwsnovedadesout SDTWSNovedadesOut { get; set; }
        }

        public class Sdtwsnovedadesout
        {
            public Novedade[] Novedades { get; set; }
            public Resultado Resultado { get; set; }
        }

        public class Resultado
        {
            public string Estado { get; set; }
            public object[] Mensajes { get; set; }
        }

        public class Novedade
        {
            public int Articulo { get; set; }
            public int ArticuloAnt { get; set; }
            public string CUIT { get; set; }
            public string Certificado { get; set; }
            public string CertificadoAnt { get; set; }
            public int CodigoPostal { get; set; }
            public string CondicionIVA { get; set; }
            public int CondicionIVAN { get; set; }
            public Detallepremio DetallePremio { get; set; }
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
            public Sdtcomision[] SDTComision { get; set; }
            public Sdtcuota[] SDTCuota { get; set; }
            public Sdtvehiculodato[] SDTVehiculoDatos { get; set; }
            public int SubCodigoPostal { get; set; }
            public string Sucursal { get; set; }
            public string SucursalAnt { get; set; }
            public int Suplemento { get; set; }
            public string Telefono { get; set; }
            public string TelefonoParticular { get; set; }
            public string VigenciaDesde { get; set; }
            public string VigenciaHasta { get; set; }
        }

        public class Detallepremio
        {
            public string Premio { get; set; }
            public Rama[] Ramas { get; set; }
        }

        public class Rama
        {
            public string Bonificacion { get; set; }
            public string CuotasSociales { get; set; }
            public string DerechoEmiFijo { get; set; }
            public string DerechoEmision { get; set; }
            public string IIBBEmpresa { get; set; }
            public string IIBBPercepcion { get; set; }
            public string IIBBRiesgo { get; set; }
            public string IVA { get; set; }
            public string IVAPercepcion { get; set; }
            public string IVARNI { get; set; }
            public string ImpInternos { get; set; }
            public string LeyEmergVial { get; set; }
            public string Premio { get; set; }
            public string Prima { get; set; }
            public string PrimaNeta { get; set; }
            public int rama { get; set; }
            public string RecargoAdm { get; set; }
            public string RecargoFin { get; set; }
            public string RecuperoGastosAsoc { get; set; }
            public string SelladoEmpresa { get; set; }
            public string SelladoRiesgo { get; set; }
            public string ServiciosSociales { get; set; }
            public string TasaSSN { get; set; }
        }

        public class Sdtcomision
        {
            public string Monto { get; set; }
            public int NIVC { get; set; }
            public int NIVT { get; set; }
            public float Porcentaje { get; set; }
            public int Rama { get; set; }
        }

        public class Sdtcuota
        {
            public string Estado { get; set; }
            public string FechaCancelada { get; set; }
            public string FechaVtoCuota { get; set; }
            public string ImporteCuota { get; set; }
            public int NumeroCuota { get; set; }
        }

        public class Sdtvehiculodato
        {
            public int Anio { get; set; }
            public int CeroKm { get; set; }
            public string Chasis { get; set; }
            public string Cobertura { get; set; }
            public Datosgnc DatosGNC { get; set; }
            public string Dominio { get; set; }
            public string Marca { get; set; }
            public int MarcaIA { get; set; }
            public string Modelo { get; set; }
            public int ModeloIA { get; set; }
            public string Motor { get; set; }
            public string Origen { get; set; }
            public string SubModelo { get; set; }
            public string SumaAsegurada { get; set; }
            public string Tipo { get; set; }
            public int TipoN { get; set; }
            public string Uso { get; set; }
            public int UsoN { get; set; }
        }

        public class Datosgnc
        {
            public string Capital { get; set; }
            public object[] Cilindros { get; set; }
            public string NroOblea { get; set; }
            public string NroSerie { get; set; }
            public string Regulador { get; set; }
        }

    }

