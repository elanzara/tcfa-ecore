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
using System.Net.Http.Headers;

namespace eCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    [EnableCors("AllowOrigin")]
    public class MapNovedadesController : ControllerBase
    {
        private readonly SecureDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IConfiguration Configuration;

        public MapNovedadesController(SecureDbContext context, IMapper mapper, IUserService userService, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _userService = userService;
            Configuration = configuration;
        }

        //Task<ActionResult<IEnumerable<TrfNovedades>>>
        // GET: api/MapNovedades/
        [HttpGet("fechamax/", Name = "mapnovedades")]
        public async Task<IEnumerable<MapNovedades>> GetMapNovedades()
        {
            return _context.MapNovedades.FromSql("select id, compania, sector, ramo, poliza, productor, endoso, (convert(varchar(250), (convert(datetime,(case len(fechaEmiSpto) when  8 then substring(fechaEmiSpto, 1, 2) + '/' + substring(fechaEmiSpto, 3, 2) + '/' + substring(fechaEmiSpto, 5, 4) when  7 then substring(fechaEmiSpto, 1, 1) + '/' + substring(fechaEmiSpto, 3, 2) + '/' + substring(fechaEmiSpto, 5, 4) end), 103) + 1), 103)) as fechaEmiSpto, fechaEfecPol, fechaVctoPol, fechaEfecSpto, fechaVctoSpto, codEndoso, subEndoso, motivo, tipoDocumentoTom, codigoDocumentoTom, planPago, polizaAnterior, polizaMadre, polizaSiguiente, facturacion, fechaRenov FROM map_novedades mn WHERE mn.id = (select top 1 a.id from( SELECT id, fechaEmiSpto, (convert(datetime, (case len(fechaEmiSpto) when  8 then substring(fechaEmiSpto, 1, 2) + '/' + substring(fechaEmiSpto, 3, 2) + '/' + substring(fechaEmiSpto, 5, 4) when  7 then substring(fechaEmiSpto, 1, 1) + '/' + substring(fechaEmiSpto, 3, 2) + '/' + substring(fechaEmiSpto, 5, 4) end), 103) + 1) as fecha from map_novedades) as a order by a.fecha desc)");
        }

        // GET: api/MapNovedades
        [HttpGet, DisableRequestSizeLimit]
        public async Task<IActionResult> PostMapNovedades(DateTime desde, DateTime hasta)
        {

            var urlToken = Configuration["mapfre:urlToken"];
            var urlNovedades = Configuration["mapfre:urlNovedades"];
            var codAgt = Configuration["mapfre:codAgt"];
            var claveAcceso = Configuration["mapfre:claveAcceso"];
            var claveProcedencia = Configuration["mapfre:claveProcedencia"];
            MapNovedadesTokenOut mapNovedadesTokenOut = new MapNovedadesTokenOut();
            MapNovedadesOut mapNovedadesOut = new MapNovedadesOut();
            var error = "";
            string accessToken = "";

            var codigoConexion = _context.MapCodigoConexion.FromSql("Select * from map_codigo_conexion").ToArray();

            //var id = codigoConexion[0].Id;

            int cantidad = codigoConexion.Count();
            
            /*Recorro todos los codigo de productor para hacer los llamados*/
            for(int aux = 0; aux < cantidad; aux++)
            {
                /*Obtengo el Token*/
                var body = new
                {
    /*                    codAgt = codAgt,
                    claveAcceso = claveAcceso,
                    claveProcedencia = claveProcedencia
    */
                    codAgt = codigoConexion[aux].codAgt,
                    claveAcceso = codigoConexion[aux].claveAcceso,
                    claveProcedencia = codigoConexion[aux].claveProcedencia

                };

                // Serialize our concrete class into a JSON String
                var stringBody = await Task.Run(() => JsonConvert.SerializeObject(body));

                // Wrap our JSON inside a StringContent which then can be used by the HttpClient class
                HttpContent httpContent = new StringContent(stringBody, Encoding.UTF8, "application/json");

                using (var httpClient = new HttpClient())
                {
                    // Do the actual request and await the response
                    var httpResponse = await httpClient.PostAsync(urlToken, httpContent);

                    if (httpResponse.Content != null)
                    {
                        var responseContent = await httpResponse.Content.ReadAsStringAsync();

                        mapNovedadesTokenOut = JsonConvert.DeserializeObject<MapNovedadesTokenOut>(responseContent);
                        accessToken = mapNovedadesTokenOut.data.accessToken.ToString();
                    }
                }

                /*Invoco el metodo de novedades*/
                urlNovedades = urlNovedades + "?fecDesde=" + desde.ToString("ddMMyyyy") + "&fecHasta=" + hasta.ToString("ddMMyyyy");

                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                    var httpResponse = await httpClient.GetAsync(urlNovedades);

                    if (httpResponse.Content != null)
                    {
                        try
                        {
                            var responseContent = await httpResponse.Content.ReadAsStringAsync();

                            try
                            {
                                var mapNovedadesOutErr = JsonConvert.DeserializeObject<MapNovedadesOutErr>(responseContent);
                                if (mapNovedadesOutErr.error != null)
                                {
                                    return StatusCode(206, mapNovedadesOutErr.error.code + " - " + mapNovedadesOutErr.error.message);
                                }
                            }
                            catch
                            {
                                error = "";
                            }

                            mapNovedadesOut = JsonConvert.DeserializeObject<MapNovedadesOut>(responseContent);

                            if (mapNovedadesOut.data.Length > 0)
                            {

                                int i = 0;
                                int cuotas = 0;
                                int coberturas = 0;
                                int variables = 0;
                                int riesgo = 0;
                                int asegurados = 0;
                                int impuestos = 0;
                                MapNovedades mapNovedades = new MapNovedades();
                                MapCuotas mapCuotas = new MapCuotas();
                                List<MapCuotas> cmapCuotas = new List<MapCuotas>();
                                MapCoberturas mapCoberturas = new MapCoberturas();
                                List<MapCoberturas> cmapCoberturas = new List<MapCoberturas>();
                                MapDatosVariables mapDatosVariables = new MapDatosVariables();
                                List<MapDatosVariables> cmapDatosVariables = new List<MapDatosVariables>();
                                MapDatosRiesgo mapDatosRiesgo = new MapDatosRiesgo();
                                List<MapDatosRiesgo> cmapDatosRiesgo = new List<MapDatosRiesgo>();
                                MapAsegurados mapAsegurados = new MapAsegurados();
                                List<MapAsegurados> cmapAsegurados = new List<MapAsegurados>();
                                MapImpuestos mapImpuestos = new MapImpuestos();
                                List<MapImpuestos> cmapImpuestos = new List<MapImpuestos>();


                                while (i < mapNovedadesOut.data.Count())
                                {
                                    cuotas = 0;
                                    coberturas = 0;
                                    variables = 0;
                                    riesgo = 0;
                                    asegurados = 0;
                                    impuestos = 0;
                                    mapNovedades = new MapNovedades();
                                    mapCuotas = new MapCuotas();
                                    cmapCuotas = new List<MapCuotas>();
                                    mapCoberturas = new MapCoberturas();
                                    cmapCoberturas = new List<MapCoberturas>();
                                    mapDatosVariables = new MapDatosVariables();
                                    cmapDatosVariables = new List<MapDatosVariables>();
                                    mapDatosRiesgo = new MapDatosRiesgo();
                                    cmapDatosRiesgo = new List<MapDatosRiesgo>();
                                    mapAsegurados = new MapAsegurados();
                                    cmapAsegurados = new List<MapAsegurados>();
                                    mapImpuestos = new MapImpuestos();
                                    cmapImpuestos = new List<MapImpuestos>();

                                    mapNovedades.compania = mapNovedadesOut.data[i].compania;
                                    mapNovedades.sector = mapNovedadesOut.data[i].sector;
                                    mapNovedades.ramo = mapNovedadesOut.data[i].ramo;
                                    mapNovedades.poliza = mapNovedadesOut.data[i].poliza;
                                    if (mapNovedadesOut.data[i].productor != null)
                                    {
                                        mapNovedades.productor = mapNovedadesOut.data[i].productor;
                                    }
                                    else
                                    {
                                        mapNovedades.productor = 0;
                                    }
                                    if (mapNovedadesOut.data[i].endoso != null)
                                    {
                                        mapNovedades.endoso = mapNovedadesOut.data[i].endoso;
                                    }
                                    else
                                    {
                                        mapNovedades.endoso = 0;
                                    }
                                    mapNovedades.fechaEmiSpto = mapNovedadesOut.data[i].fechaEmiSpto;
                                    mapNovedades.fechaEfecPol = mapNovedadesOut.data[i].fechaEfecPol;
                                    mapNovedades.fechaVctoPol = mapNovedadesOut.data[i].fechaVctoPol;
                                    mapNovedades.fechaEfecSpto = mapNovedadesOut.data[i].fechaEfecSpto;
                                    mapNovedades.fechaVctoSpto = mapNovedadesOut.data[i].fechaVctoSpto;
                                    if (mapNovedadesOut.data[i].codEndoso != null)
                                    {
                                        mapNovedades.codEndoso = mapNovedadesOut.data[i].codEndoso;
                                    }
                                    else
                                    {
                                        mapNovedades.codEndoso = 0;
                                    }

                                    if (mapNovedadesOut.data[i].subEndoso != null)
                                    {
                                        mapNovedades.subEndoso = mapNovedadesOut.data[i].subEndoso;
                                    }
                                    else
                                    {
                                        mapNovedades.subEndoso = 0;
                                    }
                                    if (mapNovedadesOut.data[i].motivo != null)
                                    {
                                        mapNovedades.motivo = mapNovedadesOut.data[i].motivo;
                                    }
                                    else
                                    {
                                        mapNovedades.motivo = "";
                                    }
                                    mapNovedades.tipoDocumentoTom = mapNovedadesOut.data[i].tipoDocumentoTom;
                                    mapNovedades.codigoDocumentoTom = mapNovedadesOut.data[i].codigoDocumentoTom;
                                    if (mapNovedadesOut.data[i].planPago != null)
                                    {
                                        mapNovedades.planPago = mapNovedadesOut.data[i].planPago;
                                    }
                                    else
                                    {
                                        mapNovedades.planPago = 0;
                                    }
                                    mapNovedades.polizaAnterior = mapNovedadesOut.data[i].polizaAnterior;
                                    mapNovedades.polizaMadre = mapNovedadesOut.data[i].polizaMadre;
                                    mapNovedades.polizaSiguiente = mapNovedadesOut.data[i].polizaSiguiente;
                                    mapNovedades.facturacion = mapNovedadesOut.data[i].facturacion;
                                    mapNovedades.fechaRenov = mapNovedadesOut.data[i].fechaRenov;

                                    while (cuotas < mapNovedadesOut.data[i].cuotas.Count())
                                    {
                                        mapCuotas = new MapCuotas();
                                        mapCuotas.poliza = mapNovedadesOut.data[i].cuotas[cuotas].poliza;
                                        mapCuotas.endoso = mapNovedadesOut.data[i].cuotas[cuotas].endoso;
                                        if (mapNovedadesOut.data[i].cuotas[cuotas].numeroRecibo != null)
                                        {
                                            mapCuotas.numeroRecibo = mapNovedadesOut.data[i].cuotas[cuotas].numeroRecibo;
                                        }
                                        else
                                        {
                                            mapCuotas.numeroRecibo = 0;
                                        }
                                        mapCuotas.convenio = mapNovedadesOut.data[i].cuotas[cuotas].convenio;
                                        mapCuotas.vctoRecibo = mapNovedadesOut.data[i].cuotas[cuotas].vctoRecibo;
                                        mapCuotas.fecCobro = mapNovedadesOut.data[i].cuotas[cuotas].fecCobro;
                                        if (mapNovedadesOut.data[i].cuotas[cuotas].agrpImpositivo != null)
                                        {
                                            mapCuotas.agrpImpositivo = mapNovedadesOut.data[i].cuotas[cuotas].agrpImpositivo;
                                        }
                                        else
                                        {
                                            mapCuotas.agrpImpositivo = 0;
                                        }
                                        mapCuotas.medioPago = mapNovedadesOut.data[i].cuotas[cuotas].medioPago;
                                        if (mapNovedadesOut.data[i].cuotas[cuotas].moneda != null)
                                        {
                                            mapCuotas.moneda = mapNovedadesOut.data[i].cuotas[cuotas].moneda;
                                        }
                                        else
                                        {
                                            mapCuotas.moneda = 0;
                                        }
                                        if (mapNovedadesOut.data[i].cuotas[cuotas].importe != null)
                                        {
                                            mapCuotas.importe = mapNovedadesOut.data[i].cuotas[cuotas].importe;
                                        }
                                        else
                                        {
                                            mapCuotas.importe = 0;
                                        }
                                        if (mapNovedadesOut.data[i].cuotas[cuotas].cobroAnticipado != null)
                                        {
                                            mapCuotas.cobroAnticipado = mapNovedadesOut.data[i].cuotas[cuotas].cobroAnticipado;
                                        }
                                        else
                                        {
                                            mapCuotas.cobroAnticipado = 0;
                                        }
                                        if (mapNovedadesOut.data[i].cuotas[cuotas].impComisiones != null)
                                        {
                                            mapCuotas.impComisiones = mapNovedadesOut.data[i].cuotas[cuotas].impComisiones;
                                        }
                                        else
                                        {
                                            mapCuotas.impComisiones = 0;
                                        }
                                        mapCuotas.situacionRecibo = mapNovedadesOut.data[i].cuotas[cuotas].situacionRecibo;
                                        cmapCuotas.Add(mapCuotas);
                                        cuotas = cuotas + 1;
                                    }

                                    mapNovedades.MapCuotas = cmapCuotas;

                                    /*ERROR*/
                                    while (coberturas < mapNovedadesOut.data[i].coberturas.Count())
                                    {
                                        mapCoberturas = new MapCoberturas();
                                        mapCoberturas.poliza = mapNovedadesOut.data[i].coberturas[coberturas].poliza;
                                        mapCoberturas.endoso = mapNovedadesOut.data[i].coberturas[coberturas].endoso;
                                        if (mapNovedadesOut.data[i].coberturas[coberturas].riesgo != null)
                                        {
                                            mapCoberturas.riesgo = mapNovedadesOut.data[i].coberturas[coberturas].riesgo;
                                        }
                                        else
                                        {
                                            mapCoberturas.riesgo = 0;
                                        }
                                        if (mapNovedadesOut.data[i].coberturas[coberturas].secu != null)
                                        {
                                            mapCoberturas.secu = mapNovedadesOut.data[i].coberturas[coberturas].secu;
                                        }
                                        else
                                        {
                                            mapCoberturas.secu = 0;
                                        }
                                        if (mapNovedadesOut.data[i].coberturas[coberturas].cobertura != null)
                                        {
                                            mapCoberturas.cobertura = mapNovedadesOut.data[i].coberturas[coberturas].cobertura;
                                        }
                                        else
                                        {
                                            mapCoberturas.cobertura = 0;
                                        }
                                        if (mapNovedadesOut.data[i].coberturas[coberturas].sumaAseg != null)
                                        {
                                            mapCoberturas.sumaAseg = mapNovedadesOut.data[i].coberturas[coberturas].sumaAseg;
                                        }
                                        else
                                        {
                                            mapCoberturas.sumaAseg = 0;
                                        }
                                        cmapCoberturas.Add(mapCoberturas);
                                        coberturas = coberturas + 1;
                                    }

                                    mapNovedades.MapCoberturas = cmapCoberturas;

                                    while (variables < mapNovedadesOut.data[i].datosVariables.Count())
                                    {
                                        mapDatosVariables = new MapDatosVariables();
                                        mapDatosVariables.poliza = mapNovedadesOut.data[i].datosVariables[variables].poliza;
                                        mapDatosVariables.endoso = mapNovedadesOut.data[i].datosVariables[variables].endoso;
                                        if (mapNovedadesOut.data[i].datosVariables[variables].riesgo != null)
                                        {
                                            mapDatosVariables.riesgo = mapNovedadesOut.data[i].datosVariables[variables].riesgo;
                                        }
                                        else
                                        {
                                            mapDatosVariables.riesgo = 0;
                                        }
                                        mapDatosVariables.campo = mapNovedadesOut.data[i].datosVariables[variables].campo;
                                        mapDatosVariables.valor = mapNovedadesOut.data[i].datosVariables[variables].valor;
                                        mapDatosVariables.descripcion = mapNovedadesOut.data[i].datosVariables[variables].descripcion;
                                        if (mapNovedadesOut.data[i].datosVariables[variables].nivel != null)
                                        {
                                            mapDatosVariables.nivel = mapNovedadesOut.data[i].datosVariables[variables].nivel;
                                        }
                                        else
                                        {
                                            mapDatosVariables.nivel = 0;
                                        }
                                        if (mapNovedadesOut.data[i].datosVariables[variables].secu != null)
                                        {
                                            mapDatosVariables.secu = mapNovedadesOut.data[i].datosVariables[variables].secu;
                                        }
                                        else
                                        {
                                            mapDatosVariables.secu = 0;
                                        }
                                        cmapDatosVariables.Add(mapDatosVariables);
                                        variables = variables + 1;
                                    }

                                    mapNovedades.MapDatosVariables = cmapDatosVariables;

                                    while (riesgo < mapNovedadesOut.data[i].datosRiesgo.Count())
                                    {
                                        mapDatosRiesgo = new MapDatosRiesgo();
                                        mapDatosRiesgo.poliza = mapNovedadesOut.data[i].datosRiesgo[riesgo].poliza;
                                        mapDatosRiesgo.endoso = mapNovedadesOut.data[i].datosRiesgo[riesgo].endoso;
                                        if (mapNovedadesOut.data[i].datosRiesgo[riesgo].riesgo != null)
                                        {
                                            mapDatosRiesgo.riesgo = mapNovedadesOut.data[i].datosRiesgo[riesgo].riesgo;
                                        }
                                        else
                                        {
                                            mapDatosVariables.riesgo = 0;
                                        }
                                        mapDatosRiesgo.nombreRiesgo = mapNovedadesOut.data[i].datosRiesgo[riesgo].nombreRiesgo;
                                        mapDatosRiesgo.vigencia = mapNovedadesOut.data[i].datosRiesgo[riesgo].vigencia;
                                        mapDatosRiesgo.vencimiento = mapNovedadesOut.data[i].datosRiesgo[riesgo].vencimiento;
                                        mapDatosRiesgo.baja = mapNovedadesOut.data[i].datosRiesgo[riesgo].baja;
                                        cmapDatosRiesgo.Add(mapDatosRiesgo);
                                        riesgo = riesgo + 1;
                                    }

                                    mapNovedades.MapDatosRiesgo = cmapDatosRiesgo;

                                    while (asegurados < mapNovedadesOut.data[i].asegurados.Count())
                                    {
                                        mapAsegurados = new MapAsegurados();
                                        mapAsegurados.poliza = mapNovedadesOut.data[i].asegurados[asegurados].poliza;
                                        if (mapNovedadesOut.data[i].asegurados[asegurados].endoso != null)
                                        {
                                            mapAsegurados.endoso = mapNovedadesOut.data[i].asegurados[asegurados].endoso;
                                        }
                                        else
                                        {
                                            mapAsegurados.endoso = 0;
                                        }
                                        if (mapNovedadesOut.data[i].asegurados[asegurados].riesgo != null)
                                        {
                                            mapAsegurados.riesgo = mapNovedadesOut.data[i].asegurados[asegurados].riesgo;
                                        }
                                        else
                                        {
                                            mapAsegurados.riesgo = 0;
                                        }
                                        mapAsegurados.tipoBenef = mapNovedadesOut.data[i].asegurados[asegurados].tipoBenef;
                                        mapAsegurados.tipoDoc = mapNovedadesOut.data[i].asegurados[asegurados].tipoDoc;
                                        mapAsegurados.codDoc = mapNovedadesOut.data[i].asegurados[asegurados].codDoc;
                                        mapAsegurados.asegurado = mapNovedadesOut.data[i].asegurados[asegurados].asegurado;
                                        mapAsegurados.domicilio = mapNovedadesOut.data[i].asegurados[asegurados].domicilio;
                                        if (mapNovedadesOut.data[i].asegurados[asegurados].localidad != null)
                                        {
                                            mapAsegurados.localidad = mapNovedadesOut.data[i].asegurados[asegurados].localidad;
                                        }
                                        else
                                        {
                                            mapAsegurados.localidad = 0;
                                        }
                                        if (mapNovedadesOut.data[i].asegurados[asegurados].postal != null)
                                        {
                                            mapAsegurados.postal = mapNovedadesOut.data[i].asegurados[asegurados].postal;
                                        }
                                        else
                                        {
                                            mapAsegurados.postal = 0;
                                        }
                                        if (mapNovedadesOut.data[i].asegurados[asegurados].provincia != null)
                                        {
                                            mapAsegurados.provincia = mapNovedadesOut.data[i].asegurados[asegurados].provincia;
                                        }
                                        else
                                        {
                                            mapAsegurados.provincia = 0;
                                        }
                                        if (mapNovedadesOut.data[i].asegurados[asegurados].telefonoPais != null)
                                        {
                                            mapAsegurados.telefonoPais = mapNovedadesOut.data[i].asegurados[asegurados].telefonoPais;
                                        }
                                        else
                                        {
                                            mapAsegurados.telefonoPais = 0;
                                        }
                                        if (mapNovedadesOut.data[i].asegurados[asegurados].telefonoZona != null)
                                        {
                                            mapAsegurados.telefonoZona = mapNovedadesOut.data[i].asegurados[asegurados].telefonoZona;
                                        }
                                        else
                                        {
                                            mapAsegurados.telefonoZona = 0;
                                        }
                                        if (mapNovedadesOut.data[i].asegurados[asegurados].telefono != null)
                                        {
                                            mapAsegurados.telefono = mapNovedadesOut.data[i].asegurados[asegurados].telefono;
                                        }
                                        else
                                        {
                                            mapAsegurados.telefono = 0;
                                        }
                                        mapAsegurados.domicilioCom = mapNovedadesOut.data[i].asegurados[asegurados].domicilioCom;
                                        if (mapNovedadesOut.data[i].asegurados[asegurados].localidadCom != null)
                                        {
                                            mapAsegurados.localidadCom = mapNovedadesOut.data[i].asegurados[asegurados].localidadCom;
                                        }
                                        else
                                        {
                                            mapAsegurados.localidadCom = 0;
                                        }
                                        if (mapNovedadesOut.data[i].asegurados[asegurados].postalCom != null)
                                        {
                                            mapAsegurados.postalCom = mapNovedadesOut.data[i].asegurados[asegurados].postalCom;
                                        }
                                        else
                                        {
                                            mapAsegurados.postalCom = 0;
                                        }
                                        if (mapNovedadesOut.data[i].asegurados[asegurados].provinciaCom != null)
                                        {
                                            mapAsegurados.provinciaCom = mapNovedadesOut.data[i].asegurados[asegurados].provinciaCom;
                                        }
                                        else
                                        {
                                            mapAsegurados.provinciaCom = 0;
                                        }
                                        mapAsegurados.nacimiento = mapNovedadesOut.data[i].asegurados[asegurados].nacimiento;
                                        if (mapNovedadesOut.data[i].asegurados[asegurados].iva != null)
                                        {
                                            mapAsegurados.iva = mapNovedadesOut.data[i].asegurados[asegurados].iva;
                                        }
                                        else
                                        {
                                            mapAsegurados.iva = 0;
                                        }
                                        mapAsegurados.mcaSexo = mapNovedadesOut.data[i].asegurados[asegurados].mcaSexo;
                                        mapAsegurados.nomina = mapNovedadesOut.data[i].asegurados[asegurados].nomina;
                                        mapAsegurados.baja = mapNovedadesOut.data[i].asegurados[asegurados].baja;

                                        cmapAsegurados.Add(mapAsegurados);
                                        asegurados = asegurados + 1;
                                    }

                                    mapNovedades.MapAsegurados = cmapAsegurados;

                                    while (impuestos < mapNovedadesOut.data[i].impuestos.Count())
                                    {
                                        mapImpuestos = new MapImpuestos();
                                        mapImpuestos.poliza = mapNovedadesOut.data[i].impuestos[impuestos].poliza;
                                        if (mapNovedadesOut.data[i].impuestos[impuestos].endoso != null)
                                        {
                                            mapImpuestos.endoso = mapNovedadesOut.data[i].impuestos[impuestos].endoso;
                                        }
                                        else
                                        {
                                            mapImpuestos.endoso = 0;
                                        }
                                        if (mapNovedadesOut.data[i].impuestos[impuestos].primaComisionable != null)
                                        {
                                            mapImpuestos.primaComisionable = mapNovedadesOut.data[i].impuestos[impuestos].primaComisionable;
                                        }
                                        else
                                        {
                                            mapImpuestos.primaComisionable = 0;
                                        }
                                        if (mapNovedadesOut.data[i].impuestos[impuestos].primaNoComisionable != null)
                                        {
                                            mapImpuestos.primaNoComisionable = mapNovedadesOut.data[i].impuestos[impuestos].primaNoComisionable;
                                        }
                                        else
                                        {
                                            mapImpuestos.primaNoComisionable = 0;
                                        }
                                        if (mapNovedadesOut.data[i].impuestos[impuestos].derEmis != null)
                                        {
                                            mapImpuestos.derEmis = mapNovedadesOut.data[i].impuestos[impuestos].derEmis;
                                        }
                                        else
                                        {
                                            mapImpuestos.derEmis = 0;
                                        }
                                        if (mapNovedadesOut.data[i].impuestos[impuestos].recAdmin != null)
                                        {
                                            mapImpuestos.recAdmin = mapNovedadesOut.data[i].impuestos[impuestos].recAdmin;
                                        }
                                        else
                                        {
                                            mapImpuestos.recAdmin = 0;
                                        }
                                        if (mapNovedadesOut.data[i].impuestos[impuestos].recFinan != null)
                                        {
                                            mapImpuestos.recFinan = mapNovedadesOut.data[i].impuestos[impuestos].recFinan;
                                        }
                                        else
                                        {
                                            mapImpuestos.recFinan = 0;
                                        }
                                        if (mapNovedadesOut.data[i].impuestos[impuestos].bonificaciones != null)
                                        {
                                            mapImpuestos.bonificaciones = mapNovedadesOut.data[i].impuestos[impuestos].bonificaciones;
                                        }
                                        else
                                        {
                                            mapImpuestos.bonificaciones = 0;
                                        }
                                        if (mapNovedadesOut.data[i].impuestos[impuestos].bonifAdic != null)
                                        {
                                            mapImpuestos.bonifAdic = mapNovedadesOut.data[i].impuestos[impuestos].bonifAdic;
                                        }
                                        else
                                        {
                                            mapImpuestos.bonifAdic = 0;
                                        }
                                        if (mapNovedadesOut.data[i].impuestos[impuestos].otrosImptos != null)
                                        {
                                            mapImpuestos.otrosImptos = mapNovedadesOut.data[i].impuestos[impuestos].otrosImptos;
                                        }
                                        else
                                        {
                                            mapImpuestos.otrosImptos = 0;
                                        }
                                        if (mapNovedadesOut.data[i].impuestos[impuestos].servSociales != null)
                                        {
                                            mapImpuestos.servSociales = mapNovedadesOut.data[i].impuestos[impuestos].servSociales;
                                        }
                                        else
                                        {
                                            mapImpuestos.servSociales = 0;
                                        }
                                        if (mapNovedadesOut.data[i].impuestos[impuestos].imptosInternos != null)
                                        {
                                            mapImpuestos.imptosInternos = mapNovedadesOut.data[i].impuestos[impuestos].imptosInternos;
                                        }
                                        else
                                        {
                                            mapImpuestos.imptosInternos = 0;
                                        }
                                        if (mapNovedadesOut.data[i].impuestos[impuestos].ingBrutos != null)
                                        {
                                            mapImpuestos.ingBrutos = mapNovedadesOut.data[i].impuestos[impuestos].ingBrutos;
                                        }
                                        else
                                        {
                                            mapImpuestos.ingBrutos = 0;
                                        }

                                        if (mapNovedadesOut.data[i].impuestos[impuestos].premio != null)
                                        {
                                            mapImpuestos.premio = mapNovedadesOut.data[i].impuestos[impuestos].premio;
                                        }
                                        else
                                        {
                                            mapImpuestos.premio = 0;
                                        }
                                        if (mapNovedadesOut.data[i].impuestos[impuestos].porComision != null)
                                        {
                                            mapImpuestos.porComision = mapNovedadesOut.data[i].impuestos[impuestos].porComision;
                                        }
                                        else
                                        {
                                            mapImpuestos.porComision = 0;
                                        }
                                        cmapImpuestos.Add(mapImpuestos);
                                        impuestos = impuestos + 1;
                                    }

                                    mapNovedades.MapImpuestos = cmapImpuestos;

                                    _context.MapNovedades.Add(mapNovedades);

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
                                /*AGREGAR CONTROL DE CANTIDAD DE REGISTROS*/
                            }
                            else
                            {

                                return StatusCode(204, "No hay informacion para el periodo");
                            }

                        }
                        catch (Exception ex)
                        {
                            error = ex.Message.ToString();
                        }

                        if (error == "")
                        {
                            return StatusCode(200, "OK");
                        }
                        else
                        {
                            return StatusCode(205, "Error: " + error);
                        }
                    }
                }

            }

            return StatusCode(200, "OK");

        }

    }
}
