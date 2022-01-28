using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eCore.Context;
using eCore.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using eCore.Services.WebApi.Services;
using AutoMapper;
using eCore.Models;
using System.Data.SqlClient;
using System.Linq.Expressions;
using eCore.Services.Models;
using eCore.Services;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace eCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    [EnableCors("AllowOrigin")]
    public class TrfNovedadesController : ControllerBase
    {
        private readonly SecureDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IConfiguration Configuration;

        public TrfNovedadesController(SecureDbContext context, IMapper mapper, IUserService userService, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _userService = userService;
            Configuration = configuration;
        }

        //Task<ActionResult<IEnumerable<TrfNovedades>>>
        // GET: api/TrfNovedades/
        [HttpGet("fechamax/", Name = "trfnovedades")]
        public async Task<IEnumerable<TrfNovedades>> GetTrfNovedades()
        {
            return _context.TrfNovedades.FromSql("select id, articulo, articulo_ant, cuit, certificado, certificado_ant, codigo_postal, condicion_iva, condicion_ivan, doc_numero, doc_tipo, domicilio, email, empresa, empresa_ant, estado_poliza, estado_poliza_n, moneda, razon_social, sub_codigo_postal, sucursal, Sucursal_ant,  suplemento, telefono, telefono_particular, (convert(varchar(250), (convert(datetime, vigencia_desde) + 1), 103)) as vigencia_desde, vigencia_hasta, codigo_productor FROM(SELECT top 1 * from trf_novedades order by vigencia_desde desc) a");
        }

        // GET: api/TrfNovedades
        [HttpGet, DisableRequestSizeLimit]
        public async Task<IActionResult> PostTrfNovedades(DateTime desde, DateTime hasta)
        {

            var url = Configuration["triunfo:url"];
            var user = Configuration["triunfo:user"];
            var pass = Configuration["triunfo:pass"];
            var codigo = Configuration["triunfo:codigo"];
            Rootobject sDTWSNovedadesOut = new Rootobject();
            var error = "";
            int cantidadAux = 0;

            var codigoConexion = _context.CodigoProductor.FromSql("SELECT * FROM codigo_productor WHERE ID_ws_broker_compania = 5").ToArray();

            int cantidad = codigoConexion.Count();

            /*Recorro todos los codigo de productor para hacer los llamados*/
            for (int aux = 0; aux < cantidad; aux++)
            { /*Inicia el loop*/

                var body = new
                {
                    SDTWSNovedadesIn = new SDTWSNovedadesIn
                    {
                        Productor = new _Productor
                        {
                            Codigo = codigoConexion[aux].codigo_productor,
                            Password = codigoConexion[aux].clave,
                            Usuario = codigoConexion[aux].usuario
                        },
                        Articulo = 0,
                        Certificado = "0",
                        FechaDesde = desde.ToString("yyyy-MM-dd"),
                        FechaHasta = hasta.ToString("yyyy-MM-dd")
                    }
                };

                //"2020-09-01"
                //"2020-09-20"
                // Serialize our concrete class into a JSON String
                var stringBody = await Task.Run(() => JsonConvert.SerializeObject(body));

                // Wrap our JSON inside a StringContent which then can be used by the HttpClient class
                HttpContent httpContent = new StringContent(stringBody, Encoding.UTF8, "application/json");

                using (var httpClient = new HttpClient())
                {

                    // Do the actual request and await the response
                    var httpResponse = await httpClient.PostAsync(url, httpContent);

                    if (httpResponse.Content != null)
                    {
                        var responseContent = await httpResponse.Content.ReadAsStringAsync();

                        try
                        {
                            sDTWSNovedadesOut = JsonConvert.DeserializeObject<Rootobject>(responseContent);

                            if (sDTWSNovedadesOut.SDTWSNovedadesOut.Resultado.Estado == "")
                            {
                                int i = 0;
                                int ramas = 0;
                                int comisiones = 0;
                                int cuotas = 0;
                                int vehiculo = 0;
                                string saux;
                                TrfNovedades novedad = new TrfNovedades();

                                TrfDetallePremio trfDetallePremio = new TrfDetallePremio();
                                List<TrfDetallePremio> ctrfDetallePremio = new List<TrfDetallePremio>();

                                TrfRama trfRama = new TrfRama();
                                List<TrfRama> cTrfRamas = new List<TrfRama>();

                                TrfSdtcomision trfSdtcomision = new TrfSdtcomision();
                                List<TrfSdtcomision> ctrfSdtcomision = new List<TrfSdtcomision>();

                                TrfSdtcuota trfSdtcuota = new TrfSdtcuota();
                                List<TrfSdtcuota> ctrfSdtcuota = new List<TrfSdtcuota>();

                                TrfVehiculoDatos trfVehiculoDatos = new TrfVehiculoDatos();
                                List<TrfVehiculoDatos> ctrfVehiculoDatos = new List<TrfVehiculoDatos>();

                                while (i < sDTWSNovedadesOut.SDTWSNovedadesOut.Novedades.Count())
                                {
                                    novedad = new TrfNovedades();
                                    ramas = 0;
                                    comisiones = 0;
                                    cuotas = 0;
                                    vehiculo = 0;

                                    trfDetallePremio = new TrfDetallePremio();
                                    ctrfDetallePremio = new List<TrfDetallePremio>();
                                    trfSdtcomision = new TrfSdtcomision();
                                    ctrfSdtcomision = new List<TrfSdtcomision>();
                                    trfSdtcuota = new TrfSdtcuota();
                                    ctrfSdtcuota = new List<TrfSdtcuota>();
                                    trfVehiculoDatos = new TrfVehiculoDatos();
                                    ctrfVehiculoDatos = new List<TrfVehiculoDatos>();

                                    novedad.Articulo = sDTWSNovedadesOut.SDTWSNovedadesOut.Novedades[i].Articulo;
                                    novedad.ArticuloAnt = sDTWSNovedadesOut.SDTWSNovedadesOut.Novedades[i].ArticuloAnt;
                                    novedad.Certificado = sDTWSNovedadesOut.SDTWSNovedadesOut.Novedades[i].Certificado;
                                    novedad.CertificadoAnt = sDTWSNovedadesOut.SDTWSNovedadesOut.Novedades[i].CertificadoAnt;
                                    novedad.CodigoPostal = sDTWSNovedadesOut.SDTWSNovedadesOut.Novedades[i].CodigoPostal;
                                    novedad.CondicionIVA = sDTWSNovedadesOut.SDTWSNovedadesOut.Novedades[i].CondicionIVA;
                                    novedad.CondicionIVAN = sDTWSNovedadesOut.SDTWSNovedadesOut.Novedades[i].CondicionIVAN;
                                    novedad.CUIT = sDTWSNovedadesOut.SDTWSNovedadesOut.Novedades[i].CUIT;
                                    novedad.DocNumero = sDTWSNovedadesOut.SDTWSNovedadesOut.Novedades[i].DocNumero;
                                    novedad.DocTipo = sDTWSNovedadesOut.SDTWSNovedadesOut.Novedades[i].DocTipo;
                                    novedad.Domicilio = sDTWSNovedadesOut.SDTWSNovedadesOut.Novedades[i].Domicilio;
                                    novedad.Email = sDTWSNovedadesOut.SDTWSNovedadesOut.Novedades[i].Email;
                                    novedad.Empresa = sDTWSNovedadesOut.SDTWSNovedadesOut.Novedades[i].Empresa;
                                    novedad.EmpresaAnt = sDTWSNovedadesOut.SDTWSNovedadesOut.Novedades[i].EmpresaAnt;
                                    novedad.EstadoPoliza = sDTWSNovedadesOut.SDTWSNovedadesOut.Novedades[i].EstadoPoliza;
                                    novedad.EstadoPolizaN = sDTWSNovedadesOut.SDTWSNovedadesOut.Novedades[i].EstadoPolizaN;
                                    novedad.Moneda = sDTWSNovedadesOut.SDTWSNovedadesOut.Novedades[i].Moneda;
                                    novedad.RazonSocial = sDTWSNovedadesOut.SDTWSNovedadesOut.Novedades[i].RazonSocial;
                                    novedad.SubCodigoPostal = sDTWSNovedadesOut.SDTWSNovedadesOut.Novedades[i].SubCodigoPostal;
                                    novedad.Sucursal = sDTWSNovedadesOut.SDTWSNovedadesOut.Novedades[i].Sucursal;
                                    novedad.SucursalAnt = sDTWSNovedadesOut.SDTWSNovedadesOut.Novedades[i].SucursalAnt;
                                    novedad.Suplemento = sDTWSNovedadesOut.SDTWSNovedadesOut.Novedades[i].Suplemento;
                                    novedad.Telefono = sDTWSNovedadesOut.SDTWSNovedadesOut.Novedades[i].Telefono;
                                    novedad.TelefonoParticular = sDTWSNovedadesOut.SDTWSNovedadesOut.Novedades[i].TelefonoParticular;
                                    novedad.VigenciaDesde = sDTWSNovedadesOut.SDTWSNovedadesOut.Novedades[i].VigenciaDesde;
                                    novedad.VigenciaHasta = sDTWSNovedadesOut.SDTWSNovedadesOut.Novedades[i].VigenciaHasta;
                                    saux = sDTWSNovedadesOut.SDTWSNovedadesOut.Novedades[i].DetallePremio.Premio;
                                    saux = saux.Replace(".", ",");
                                    trfDetallePremio.Premio = Decimal.Parse(saux);
                                    novedad.codigo_productor = codigoConexion[aux].codigo_productor;

                                    ramas = 0;
                                    while (ramas < sDTWSNovedadesOut.SDTWSNovedadesOut.Novedades[i].DetallePremio.Ramas.Count())
                                    {
                                        trfRama = new TrfRama();
                                        trfRama.Bonificacion = Decimal.Parse(sDTWSNovedadesOut.SDTWSNovedadesOut.Novedades[i].DetallePremio.Ramas[ramas].Bonificacion.Replace(".", ","));
                                        trfRama.CuotasSociales = Decimal.Parse(sDTWSNovedadesOut.SDTWSNovedadesOut.Novedades[i].DetallePremio.Ramas[ramas].CuotasSociales.Replace(".", ","));
                                        trfRama.DerechoEmiFijo = Decimal.Parse(sDTWSNovedadesOut.SDTWSNovedadesOut.Novedades[i].DetallePremio.Ramas[ramas].DerechoEmiFijo.Replace(".", ","));
                                        trfRama.DerechoEmision = Decimal.Parse(sDTWSNovedadesOut.SDTWSNovedadesOut.Novedades[i].DetallePremio.Ramas[ramas].DerechoEmision.Replace(".", ","));
                                        trfRama.IIBBEmpresa = Decimal.Parse(sDTWSNovedadesOut.SDTWSNovedadesOut.Novedades[i].DetallePremio.Ramas[ramas].IIBBEmpresa.Replace(".", ","));
                                        trfRama.IIBBPercepcion = Decimal.Parse(sDTWSNovedadesOut.SDTWSNovedadesOut.Novedades[i].DetallePremio.Ramas[ramas].IIBBPercepcion.Replace(".", ","));
                                        trfRama.IIBBRiesgo = Decimal.Parse(sDTWSNovedadesOut.SDTWSNovedadesOut.Novedades[i].DetallePremio.Ramas[ramas].IIBBRiesgo.Replace(".", ","));
                                        trfRama.ImpInternos = Decimal.Parse(sDTWSNovedadesOut.SDTWSNovedadesOut.Novedades[i].DetallePremio.Ramas[ramas].ImpInternos.Replace(".", ","));
                                        trfRama.IVA = Decimal.Parse(sDTWSNovedadesOut.SDTWSNovedadesOut.Novedades[i].DetallePremio.Ramas[ramas].IVA.Replace(".", ","));
                                        trfRama.IVAPercepcion = Decimal.Parse(sDTWSNovedadesOut.SDTWSNovedadesOut.Novedades[i].DetallePremio.Ramas[ramas].IVAPercepcion.Replace(".", ","));
                                        trfRama.IVARNI = Decimal.Parse(sDTWSNovedadesOut.SDTWSNovedadesOut.Novedades[i].DetallePremio.Ramas[ramas].IVARNI.Replace(".", ","));
                                        trfRama.LeyEmergVial = Decimal.Parse(sDTWSNovedadesOut.SDTWSNovedadesOut.Novedades[i].DetallePremio.Ramas[ramas].LeyEmergVial.Replace(".", ","));
                                        trfRama.Premio = Decimal.Parse(sDTWSNovedadesOut.SDTWSNovedadesOut.Novedades[i].DetallePremio.Ramas[ramas].Premio.Replace(".", ","));
                                        trfRama.Prima = Decimal.Parse(sDTWSNovedadesOut.SDTWSNovedadesOut.Novedades[i].DetallePremio.Ramas[ramas].Prima.Replace(".", ","));
                                        trfRama.PrimaNeta = Decimal.Parse(sDTWSNovedadesOut.SDTWSNovedadesOut.Novedades[i].DetallePremio.Ramas[ramas].PrimaNeta.Replace(".", ","));
                                        trfRama.rama = sDTWSNovedadesOut.SDTWSNovedadesOut.Novedades[i].DetallePremio.Ramas[ramas].rama;
                                        trfRama.RecargoAdm = Decimal.Parse(sDTWSNovedadesOut.SDTWSNovedadesOut.Novedades[i].DetallePremio.Ramas[ramas].RecargoAdm.Replace(".", ","));
                                        trfRama.RecargoFin = Decimal.Parse(sDTWSNovedadesOut.SDTWSNovedadesOut.Novedades[i].DetallePremio.Ramas[ramas].RecargoFin.Replace(".", ","));
                                        trfRama.RecuperoGastosAsoc = Decimal.Parse(sDTWSNovedadesOut.SDTWSNovedadesOut.Novedades[i].DetallePremio.Ramas[ramas].RecuperoGastosAsoc.Replace(".", ","));
                                        trfRama.SelladoEmpresa = Decimal.Parse(sDTWSNovedadesOut.SDTWSNovedadesOut.Novedades[i].DetallePremio.Ramas[ramas].SelladoEmpresa.Replace(".", ","));
                                        trfRama.SelladoRiesgo = Decimal.Parse(sDTWSNovedadesOut.SDTWSNovedadesOut.Novedades[i].DetallePremio.Ramas[ramas].SelladoRiesgo.Replace(".", ","));
                                        trfRama.ServiciosSociales = Decimal.Parse(sDTWSNovedadesOut.SDTWSNovedadesOut.Novedades[i].DetallePremio.Ramas[ramas].ServiciosSociales.Replace(".", ","));
                                        trfRama.TasaSSN = Decimal.Parse(sDTWSNovedadesOut.SDTWSNovedadesOut.Novedades[i].DetallePremio.Ramas[ramas].TasaSSN.Replace(".", ","));

                                        cTrfRamas.Add(trfRama);

                                        ramas = ramas + 1;
                                    }

                                    trfDetallePremio.TrfRama = cTrfRamas;
                                    ctrfDetallePremio.Add(trfDetallePremio);
                                    novedad.TrfDetallePremio = ctrfDetallePremio;

                                    /*TrfSdtcomision*/
                                    comisiones = 0;
                                    while (comisiones < sDTWSNovedadesOut.SDTWSNovedadesOut.Novedades[i].SDTComision.Count())
                                    {
                                        trfSdtcomision = new TrfSdtcomision();
                                        trfSdtcomision.Monto = Decimal.Parse(sDTWSNovedadesOut.SDTWSNovedadesOut.Novedades[i].SDTComision[comisiones].Monto.Replace(".", ","));
                                        trfSdtcomision.NIVC = sDTWSNovedadesOut.SDTWSNovedadesOut.Novedades[i].SDTComision[comisiones].NIVC;
                                        trfSdtcomision.NIVT = sDTWSNovedadesOut.SDTWSNovedadesOut.Novedades[i].SDTComision[comisiones].NIVT;
                                        trfSdtcomision.Porcentaje = Convert.ToDecimal(sDTWSNovedadesOut.SDTWSNovedadesOut.Novedades[i].SDTComision[comisiones].Porcentaje);
                                        trfSdtcomision.Rama = sDTWSNovedadesOut.SDTWSNovedadesOut.Novedades[i].SDTComision[comisiones].Rama;

                                        ctrfSdtcomision.Add(trfSdtcomision);
                                        comisiones = comisiones + 1;
                                    }

                                    novedad.TrfSdtcomision = ctrfSdtcomision;

                                    /*TrfSdtcuota*/
                                    cuotas = 0;
                                    while (cuotas < sDTWSNovedadesOut.SDTWSNovedadesOut.Novedades[i].SDTCuota.Count())
                                    {
                                        trfSdtcuota = new TrfSdtcuota();
                                        trfSdtcuota.Estado = sDTWSNovedadesOut.SDTWSNovedadesOut.Novedades[i].SDTCuota[cuotas].Estado;
                                        trfSdtcuota.FechaCancelada = sDTWSNovedadesOut.SDTWSNovedadesOut.Novedades[i].SDTCuota[cuotas].FechaCancelada;
                                        trfSdtcuota.FechaVtoCuota = sDTWSNovedadesOut.SDTWSNovedadesOut.Novedades[i].SDTCuota[cuotas].FechaVtoCuota;
                                        trfSdtcuota.ImporteCuota = Convert.ToDecimal(sDTWSNovedadesOut.SDTWSNovedadesOut.Novedades[i].SDTCuota[cuotas].ImporteCuota.Replace(".", ","));
                                        trfSdtcuota.NumeroCuota = sDTWSNovedadesOut.SDTWSNovedadesOut.Novedades[i].SDTCuota[cuotas].NumeroCuota;

                                        ctrfSdtcuota.Add(trfSdtcuota);
                                        cuotas = cuotas + 1;
                                    }

                                    novedad.TrfSdtcuota = ctrfSdtcuota;

                                    /*TrfVehiculoDatos*/
                                    vehiculo = 0;
                                    while (vehiculo < sDTWSNovedadesOut.SDTWSNovedadesOut.Novedades[i].SDTVehiculoDatos.Count())
                                    {
                                        trfVehiculoDatos = new TrfVehiculoDatos();
                                        trfVehiculoDatos.Anio = sDTWSNovedadesOut.SDTWSNovedadesOut.Novedades[i].SDTVehiculoDatos[vehiculo].Anio;
                                        trfVehiculoDatos.CeroKm = sDTWSNovedadesOut.SDTWSNovedadesOut.Novedades[i].SDTVehiculoDatos[vehiculo].CeroKm;
                                        trfVehiculoDatos.Chasis = sDTWSNovedadesOut.SDTWSNovedadesOut.Novedades[i].SDTVehiculoDatos[vehiculo].Chasis;
                                        trfVehiculoDatos.Cobertura = sDTWSNovedadesOut.SDTWSNovedadesOut.Novedades[i].SDTVehiculoDatos[vehiculo].Cobertura;
                                        trfVehiculoDatos.Dominio = sDTWSNovedadesOut.SDTWSNovedadesOut.Novedades[i].SDTVehiculoDatos[vehiculo].Dominio;
                                        trfVehiculoDatos.Marca = sDTWSNovedadesOut.SDTWSNovedadesOut.Novedades[i].SDTVehiculoDatos[vehiculo].Marca;
                                        trfVehiculoDatos.MarcaIA = sDTWSNovedadesOut.SDTWSNovedadesOut.Novedades[i].SDTVehiculoDatos[vehiculo].MarcaIA;
                                        trfVehiculoDatos.Modelo = sDTWSNovedadesOut.SDTWSNovedadesOut.Novedades[i].SDTVehiculoDatos[vehiculo].Modelo;
                                        trfVehiculoDatos.ModeloIA = sDTWSNovedadesOut.SDTWSNovedadesOut.Novedades[i].SDTVehiculoDatos[vehiculo].ModeloIA;
                                        trfVehiculoDatos.Motor = sDTWSNovedadesOut.SDTWSNovedadesOut.Novedades[i].SDTVehiculoDatos[vehiculo].Motor;
                                        trfVehiculoDatos.Origen = sDTWSNovedadesOut.SDTWSNovedadesOut.Novedades[i].SDTVehiculoDatos[vehiculo].Origen;
                                        trfVehiculoDatos.SubModelo = sDTWSNovedadesOut.SDTWSNovedadesOut.Novedades[i].SDTVehiculoDatos[vehiculo].SubModelo;
                                        trfVehiculoDatos.SumaAsegurada = Decimal.Parse(sDTWSNovedadesOut.SDTWSNovedadesOut.Novedades[i].SDTVehiculoDatos[vehiculo].SumaAsegurada.Replace(".", ","));
                                        trfVehiculoDatos.Tipo = sDTWSNovedadesOut.SDTWSNovedadesOut.Novedades[i].SDTVehiculoDatos[vehiculo].Tipo;
                                        trfVehiculoDatos.TipoN = sDTWSNovedadesOut.SDTWSNovedadesOut.Novedades[i].SDTVehiculoDatos[vehiculo].TipoN;
                                        trfVehiculoDatos.Uso = sDTWSNovedadesOut.SDTWSNovedadesOut.Novedades[i].SDTVehiculoDatos[vehiculo].Uso;
                                        trfVehiculoDatos.UsoN = sDTWSNovedadesOut.SDTWSNovedadesOut.Novedades[i].SDTVehiculoDatos[vehiculo].UsoN;
                                        ctrfVehiculoDatos.Add(trfVehiculoDatos);
                                        vehiculo = vehiculo + 1;
                                    }

                                    novedad.TrfVehiculoDatos = ctrfVehiculoDatos;

                                    _context.TrfNovedades.Add(novedad);

                                    try
                                    {
                                        await _context.SaveChangesAsync();
                                    }
                                    catch (Exception e)
                                    {
                                        error = e.InnerException.Message;
                                    }
                                    i = i + 1;

                                }
                            } else
                            {
                                //object[] error2 = sDTWSNovedadesOut.SDTWSNovedadesOut.Resultado.Mensajes;
                                //var error3 = ((Newtonsoft.Json.Linq.JValue)(new System.Linq.SystemCore_EnumerableDebugView<KeyValuePair<string, Newtonsoft.Json.Linq.JToken>>(error2).Items[0]).Value).Value.ToString();
                                //error = (sDTWSNovedadesOut.SDTWSNovedadesOut.Resultado.Mensajes[0]).Items[0]).Value).Value.ToString();
                                // error = ((Newtonsoft.Json.Linq.JValue)(new System.Linq.SystemCore_EnumerableDebugView<System.Collections.Generic.KeyValuePair<string, Newtonsoft.Json.Linq.JToken>>(sDTWSNovedadesOut.SDTWSNovedadesOut.Resultado.Mensajes[0]).Items[0]).Value).Value.ToString();
                                error = "No hay operaciones en el periodo seleccionado";
                                cantidadAux = cantidadAux + 1;

                            }
                        }
                        catch (Exception ex)
                        {
                            error = ex.Message.ToString();
                        }
/*
                        if (error == "")
                        {
                            return StatusCode(200, "OK");
                        } else
                        {
                            return StatusCode(205, "Error: " + error);
                        }
                        */

                    }
                }

            }//Finaliza el loop

            if (error == "")
            {
                return StatusCode(200, "OK");
            }
            else if (cantidadAux == cantidad)
            {
                return StatusCode(205, "Error: " + error);
            } else if (cantidadAux < cantidad)
            {
                return StatusCode(200, "OK");
            } else
            {
                return StatusCode(205, "Error: " + error);
            }

            //return StatusCode(200, "OK");

        }

    }
}
