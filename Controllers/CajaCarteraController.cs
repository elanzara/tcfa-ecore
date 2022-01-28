using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using eCore.Context;
using eCore.Services.WebApi.Services;
using AutoMapper;
using eCore.Models;
using System.Xml;

namespace eCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CajaCarteraController : ControllerBase
    {
        private readonly SecureDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public CajaCarteraController(SecureDbContext context, IMapper mapper, IUserService userService)
        {
            _context = context;
            _mapper = mapper;
            _userService = userService;
        }

        // GET: api/CajaCartera
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1Caja", "value2Caja" };
        }

        // GET: api/CajaCartera/5
        [HttpGet("{id}", Name = "GetCajaCartera")]
        public string Get(int id)
        {
            return "value";
        }

        // GET: api/CajaCartera/ProcesadoCartera/"Archivo"
        [HttpGet("ProcesadoCartera/{file}", Name = "FileCajaCartera")]
        public async Task<string> Get(string file)
        //public async Task<ActionResult<DetalleDTO<AcceptedResult>>> Get(string file)
        {
            try
            {
                /*Buscar si el archivo ya fue procesado*/
                var encabezadoRepetido = _context.CajaCarteraEnc.FirstOrDefault(x => x.Archivo.Equals(file));

                if (encabezadoRepetido == null)
                {
                    //return StatusCode(201, "No se proceso el archivo anteriormente");
                    return "201";
                }
                else
                {
                    //return StatusCode(200, "Se proceso el archivo anteriormente");
                    return "200";
                }

            }
            catch (Exception ex)
            {
                //return StatusCode (500, "Internal server error: " + ex.Message);
                return "500: Internal Server Error: " + ex.Message;

            }
            //return "value";
        }

        // POST: api/CajaCartera
        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> UploadAsync()
        {
            try
            {
                var file = Request.Form.Files[0];
                var user = Request.Cookies["username"];

                var folderName = Path.Combine("Resources", "Upload");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    await this.procesarCajaCarteraAsync(fullPath, fileName, user);

                    return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        // PUT: api/CajaCartera/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/CajaCartera/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }


        public async Task<IActionResult> procesarCajaCarteraAsync(string path, string fileName, string userName)
        {

            string contenido = "";
            string error = "";
            //Recupero y valido el usuario del token
            //string userConnected = _userService.GetUserByToken(Request.Headers["Authorization"]);
            string userConnected = userName; // "Edu";
            Entities.CajaCarteraEnc cajaCarteraEnc = new Entities.CajaCarteraEnc();
            cajaCarteraEnc.Archivo = fileName;//path;
            cajaCarteraEnc.Usuario = userConnected;
            cajaCarteraEnc.FechaProceso = DateTime.Today;

            //descomentar despues de la prueba
            ///*Buscar si el archivo ya fue procesado*/
            var encabezadoRepetido = _context.CajaCarteraEnc.FirstOrDefault(x => x.Archivo.Equals(fileName));

            if (encabezadoRepetido != null)
            {

                try
                {
                    _context.CajaCarteraDet.Where(x => x.IdCajaCarteraEnc.Equals(encabezadoRepetido.Id)).ToList().ForEach(p => _context.CajaCarteraDet.Remove(p));
                    _context.SaveChanges();

                }
                catch (Exception e)
                {
                    error = e.InnerException.Message;
                }


                try
                {
                    _context.Remove(encabezadoRepetido);
                    _context.SaveChanges();
                }
                catch (Exception e)
                {
                    error = e.InnerException.Message;
                }

            }



            _context.CajaCarteraEnc.Add(cajaCarteraEnc);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                error = e.InnerException.Message;
            }
            //FIN descomentar despues de la prueba

            /*Cambiar desde aca por el XML*/

            Entities.CajaCarteraDet cajaCarteraDet = new Entities.CajaCarteraDet();
            List<Entities.CajaCarteraDet> lCajaCarteraDet = new List<Entities.CajaCarteraDet>();
            Entities.CajaCarteraCliente cajaCarteraCliente = new Entities.CajaCarteraCliente();
            List<Entities.CajaCarteraCliente> lCajaCarteraCliente = new List<Entities.CajaCarteraCliente>();
            Entities.CajaCarteraDomicilio cajaCarteraDomicilio = new Entities.CajaCarteraDomicilio();
            List<Entities.CajaCarteraDomicilio> lCajaCarteraDomicilio = new List<Entities.CajaCarteraDomicilio>();
            Entities.CajaCarteraDomicilioCorresp cajaCarteraDomicilioCorresp = new Entities.CajaCarteraDomicilioCorresp();
            List<Entities.CajaCarteraDomicilioCorresp> lCajaCarteraDomicilioCorresp = new List<Entities.CajaCarteraDomicilioCorresp>();
            Entities.CajaCarteraAuto cajaCarteraAuto = new Entities.CajaCarteraAuto();
            List<Entities.CajaCarteraAuto> lCajaCarteraAuto = new List<Entities.CajaCarteraAuto>();
            Entities.CajaCarteraAccesorio cajaCarteraAccesorio = new Entities.CajaCarteraAccesorio();
            List<Entities.CajaCarteraAccesorio> lCajaCarteraAccesorio = new List<Entities.CajaCarteraAccesorio>();
            Entities.CajaCarteraCuota cajaCarteraCuota = new Entities.CajaCarteraCuota();
            List<Entities.CajaCarteraCuota> lCajaCarteraCuota = new List<Entities.CajaCarteraCuota>();
            Entities.CajaCarteraCuotas cajaCarteraCuotas = new Entities.CajaCarteraCuotas();
            List<Entities.CajaCarteraCuotas> lCajaCarteraCuotas = new List<Entities.CajaCarteraCuotas>();

            string aux = "";

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(path);

            XmlNodeList xPolizas = xDoc.GetElementsByTagName("POLIZAS");
            XmlNodeList xPoliza = ((XmlElement)xPolizas[0]).GetElementsByTagName("POLIZA");
            XmlNodeList xPolizaRow = ((XmlElement)xPoliza[0]).GetElementsByTagName("POLIZA_ROW");
            XmlNodeList XmlNodeList;
            XmlNodeList XmlNodeListCliente;
            XmlNodeList XmlNodeListDomicilio;
            XmlNodeList XmlNodeListDomicilioCorresp;
            XmlNodeList XmlNodeListAuto;
            XmlNodeList XmlNodeListAccesorio;
            XmlNodeList XmlNodeListAccesorios;
            XmlNodeList XmlNodeListCuota;
            XmlNodeList XmlNodeListCuotas;
            DateTime auxFecha = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue;

            foreach (XmlElement nodoPoliza in xPoliza)
            {
                xPolizaRow = ((XmlElement)nodoPoliza).GetElementsByTagName("POLIZA_ROW");
            
                foreach (XmlElement nodo in xPolizaRow)
            {
                    cajaCarteraDet = new Entities.CajaCarteraDet();
                    cajaCarteraDet.IdCajaCarteraEnc = cajaCarteraEnc.Id;/*Modificar con el id del encabezado*/
                
                /*COMPANIA*/
                XmlNodeList = nodo.GetElementsByTagName("COMPANIA");
                if (XmlNodeList.Count != 0)
                {
                    aux = XmlNodeList[0].InnerText;
                    if (aux != "") { cajaCarteraDet.Compania = Int32.Parse(aux); } else { cajaCarteraDet.Compania = 0; }
                }
                //cajaCarteraDet.Seccion
                XmlNodeList = nodo.GetElementsByTagName("SECCION");
                if (XmlNodeList.Count != 0)
                {
                    aux = XmlNodeList[0].InnerText;
                    if (aux != "") { cajaCarteraDet.Seccion = Int32.Parse(aux); } else { cajaCarteraDet.Seccion = 0; }
                }
                //cajaCarteraDet.Ramo
                XmlNodeList = nodo.GetElementsByTagName("RAMO");
                if (XmlNodeList.Count != 0)
                {
                    aux = XmlNodeList[0].InnerText;
                    if (aux != "") { cajaCarteraDet.Ramo = Int32.Parse(aux); } else { cajaCarteraDet.Ramo = 0; }
                }
                //cajaCarteraDet.Numero
                XmlNodeList = nodo.GetElementsByTagName("NUMERO");
                if (XmlNodeList.Count != 0)
                {
                    aux = XmlNodeList[0].InnerText;
                    if (aux != "") { cajaCarteraDet.Numero = Decimal.Parse(aux); } else { cajaCarteraDet.Numero = 0; }
                }
                //cajaCarteraDet.Referencia
                XmlNodeList = nodo.GetElementsByTagName("REFERENCIA");
                if (XmlNodeList.Count != 0) { 
                    aux = XmlNodeList[0].InnerText;
                    //if (aux != "") { cajaCarteraDet.Referencia = Decimal.Parse(aux); } else { cajaCarteraDet.Referencia = 0; }
                }

                //cajaCarteraDet.Observacion
                XmlNodeList = nodo.GetElementsByTagName("OBSERVACION");
                if (XmlNodeList.Count != 0)
                {
                    cajaCarteraDet.Observacion = XmlNodeList[0].InnerText;
                }
                //cajaCarteraDet.FechaVigencia
                XmlNodeList = nodo.GetElementsByTagName("FECHA_VIGENCIA");
                if (XmlNodeList.Count != 0)
                {
                    try {
                            auxFecha = DateTime.Parse(XmlNodeList[0].InnerText);
                            if (auxFecha == DateTime.Parse("01/01/0001"))
                            {
                                auxFecha = DateTime.Parse("01/01/1900");
                            }

                            cajaCarteraDet.FechaVigencia = auxFecha;
                        }
                    catch {
                            cajaCarteraDet.FechaVigencia = DateTime.Parse("01/01/1900");
                        }
                } else { cajaCarteraDet.FechaVigencia = DateTime.Parse("01/01/1900");  }
                //cajaCarteraDet.FechaVencimiento
                XmlNodeList = nodo.GetElementsByTagName("FECHA_VENCIMIENTO");
                if (XmlNodeList.Count != 0)
                {
                        try
                        {
                            auxFecha = DateTime.Parse(XmlNodeList[0].InnerText);
                            if (auxFecha == DateTime.Parse("01/01/0001"))
                            {
                                auxFecha = DateTime.Parse("01/01/1900");
                            }
                            cajaCarteraDet.FechaVencimiento = auxFecha;
                        }
                        catch
                        {
                            cajaCarteraDet.FechaVencimiento = DateTime.Parse("01/01/1900");
                        }
                        
                } else { cajaCarteraDet.FechaVencimiento = DateTime.Parse("01/01/1900"); }
                //cajaCarteraDet.FechaEmision
                XmlNodeList = nodo.GetElementsByTagName("FECHA_EMISION");
                if (XmlNodeList.Count != 0)
                {
                        try
                        {
                            auxFecha = DateTime.Parse(XmlNodeList[0].InnerText);
                            if (auxFecha == DateTime.Parse("01/01/0001"))
                            {
                                auxFecha = DateTime.Parse("01/01/1900");
                            }
                            cajaCarteraDet.FechaEmision = auxFecha;
                        }
                        catch
                        {
                            cajaCarteraDet.FechaEmision = DateTime.Parse("01/01/1900");
                        }
                        
                } else { cajaCarteraDet.FechaEmision = DateTime.Parse("01/01/1900"); }
                //cajaCarteraDet.FormaCobro
                XmlNodeList = nodo.GetElementsByTagName("FORMA_COBRO");
                if (XmlNodeList.Count != 0)
                {
                    cajaCarteraDet.FormaCobro = XmlNodeList[0].InnerText;
                }
                //cajaCarteraDet.CBU
                XmlNodeList = nodo.GetElementsByTagName("CBU");
                if (XmlNodeList.Count != 0)
                {
                    cajaCarteraDet.CBU = XmlNodeList[0].InnerText;
                }
                //cajaCarteraDet.NumEnd
                XmlNodeList = nodo.GetElementsByTagName("NUM_END");
                if (XmlNodeList.Count != 0)
                {
                    aux = XmlNodeList[0].InnerText;
                    if (aux != "") { cajaCarteraDet.NumEnd = Decimal.Parse(aux); } else { cajaCarteraDet.NumEnd = 0; }
                }
                //cajaCarteraDet.CodMon
                XmlNodeList = nodo.GetElementsByTagName("COD_MON");
                if (XmlNodeList.Count != 0)
                {
                    aux = XmlNodeList[0].InnerText;
                    if (aux != "") { cajaCarteraDet.CodMon = Int32.Parse(aux); } else { cajaCarteraDet.CodMon = 0; }
                }
                //cajaCarteraDet.CodProd
                XmlNodeList = nodo.GetElementsByTagName("COD_PROD");
                if (XmlNodeList.Count != 0)
                {
                    aux = XmlNodeList[0].InnerText;
                    if (aux != "") { cajaCarteraDet.CodProd = Int32.Parse(aux); } else { cajaCarteraDet.CodProd = 0; }
                }
                //cajaCarteraDet.PolizaAnterior
                XmlNodeList = nodo.GetElementsByTagName("POLIZA_ANTERIOR");
                if (XmlNodeList.Count != 0)
                {
                    cajaCarteraDet.PolizaAnterior = XmlNodeList[0].InnerText;
                }
                //cajaCarteraDet.Aglutinador
                XmlNodeList = nodo.GetElementsByTagName("AGLUTINADOR");
                if (XmlNodeList.Count != 0)
                {
                    cajaCarteraDet.Aglutinador = XmlNodeList[0].InnerText;
                }
                //cajaCarteraDet.Solicitud
                XmlNodeList = nodo.GetElementsByTagName("SOLICITUD");
                if (XmlNodeList.Count != 0)
                {
                    cajaCarteraDet.Solicitud = XmlNodeList[0].InnerText;
                }
                //cajaCarteraDet.Negocio
                XmlNodeList = nodo.GetElementsByTagName("NEGOCIO");
                if (XmlNodeList.Count != 0)
                {
                    cajaCarteraDet.Negocio = XmlNodeList[0].InnerText;
                }
                /*Aca falta agregar los objetos dentro del CajaCarteraDet*/

                //public virtual ICollection<CajaCarteraCliente> CajaCarteraCliente { get; set; }
                cajaCarteraCliente = new Entities.CajaCarteraCliente();
                
                XmlNodeListCliente = nodo.GetElementsByTagName("CLIENTE");
                foreach (XmlElement nodoCliente in XmlNodeListCliente)
                {
                        cajaCarteraCliente = new Entities.CajaCarteraCliente();
                       // cajaCarteraCliente.IdCajaCarteraEnc = cajaCarteraEnc.Id;/*Modificar con el id del encabezado*/
                        XmlNodeList = nodoCliente.GetElementsByTagName("TIPO_DOCUMENTO");
                    if (XmlNodeList.Count != 0)
                    {
                        cajaCarteraCliente.TipoDocumento = XmlNodeList[0].InnerText;
                    }
                    XmlNodeList = nodoCliente.GetElementsByTagName("NRO_DOCUMENTO");
                    if (XmlNodeList.Count != 0)
                    {
                        aux = XmlNodeList[0].InnerText;
                        if (aux != "") { cajaCarteraCliente.NroDocumento = decimal.Parse(aux); } else { cajaCarteraCliente.NroDocumento = 0; }
                    }
                    XmlNodeList = nodoCliente.GetElementsByTagName("APELLIDO");
                    if (XmlNodeList.Count != 0)
                    {
                        cajaCarteraCliente.Apellido = XmlNodeList[0].InnerText;
                    }
                    XmlNodeList = nodoCliente.GetElementsByTagName("NOMBRE");
                    if (XmlNodeList.Count != 0)
                    {
                        cajaCarteraCliente.Nombre = XmlNodeList[0].InnerText;
                    }
                    XmlNodeList = nodoCliente.GetElementsByTagName("FECHA_NACIMIENTO");
                    if (XmlNodeList.Count != 0)
                    {
                            try
                            {
                                auxFecha = DateTime.Parse(XmlNodeList[0].InnerText);
                                if (auxFecha == DateTime.Parse("01/01/0001"))
                                {
                                     auxFecha = DateTime.Parse("01/01/1900");
                                }
                                cajaCarteraCliente.FechaNacimiento = auxFecha;
                            }
                            catch
                            {
                                cajaCarteraCliente.FechaNacimiento = DateTime.Parse("01/01/1900");
                            }
                    }
                    else
                        { cajaCarteraCliente.FechaNacimiento = DateTime.Parse("01/01/1900"); }
                    XmlNodeList = nodoCliente.GetElementsByTagName("COD_IVA");
                    if (XmlNodeList.Count != 0)
                    {
                        aux = XmlNodeList[0].InnerText;
                        if (aux != "") { cajaCarteraCliente.CodIva = Int32.Parse(aux); } else { cajaCarteraCliente.CodIva = 0; }
                    }
                    XmlNodeList = nodoCliente.GetElementsByTagName("SEXO");
                    if (XmlNodeList.Count != 0)
                    {
                        cajaCarteraCliente.Sexo = XmlNodeList[0].InnerText;
                    }
                    XmlNodeList = nodoCliente.GetElementsByTagName("EST_CIVIL");
                    if (XmlNodeList.Count != 0)
                    {
                        cajaCarteraCliente.EstCivil = XmlNodeList[0].InnerText;
                    }
                    lCajaCarteraCliente.Add(cajaCarteraCliente);
                    
                }

                    cajaCarteraDet.CajaCarteraCliente = lCajaCarteraCliente;
                    lCajaCarteraCliente = new List<Entities.CajaCarteraCliente>();
                    cajaCarteraDomicilio = new Entities.CajaCarteraDomicilio();
                
                XmlNodeListDomicilio = nodo.GetElementsByTagName("DOMICILIO");
                foreach (XmlElement nodoDomicilio in XmlNodeListDomicilio)
                {
                        cajaCarteraDomicilio = new Entities.CajaCarteraDomicilio();
                       // cajaCarteraDomicilio.IdCajaCarteraEnc = cajaCarteraEnc.Id;/*Modificar con el id del encabezado*/
                        XmlNodeList = nodoDomicilio.GetElementsByTagName("DIRECCION");
                    if (XmlNodeList.Count != 0)
                    {
                        cajaCarteraDomicilio.Direccion = XmlNodeList[0].InnerText;
                    }

                    XmlNodeList = nodoDomicilio.GetElementsByTagName("LOCALIDAD");
                    if (XmlNodeList.Count != 0)
                    {
                        cajaCarteraDomicilio.Localidad = XmlNodeList[0].InnerText;
                    }

                    XmlNodeList = nodoDomicilio.GetElementsByTagName("CODIGO_POSTAL");
                    if (XmlNodeList.Count != 0)
                    {
                        cajaCarteraDomicilio.CodigoPostal = XmlNodeList[0].InnerText;
                    }

                    XmlNodeList = nodoDomicilio.GetElementsByTagName("CODIGO_PROVINCIA");
                    if (XmlNodeList.Count != 0)
                    {
                        aux = XmlNodeList[0].InnerText;
                        if (aux != "") { cajaCarteraDomicilio.CodigoProvincia = Int32.Parse(aux); } else { cajaCarteraDomicilio.CodigoProvincia = 0; }
                    }
                    lCajaCarteraDomicilio.Add(cajaCarteraDomicilio);
                    
                }
                    cajaCarteraDet.CajaCarteraDomicilio = lCajaCarteraDomicilio;
                    lCajaCarteraDomicilio = new List<Entities.CajaCarteraDomicilio>();
                    cajaCarteraDomicilioCorresp = new Entities.CajaCarteraDomicilioCorresp();
                    
                XmlNodeListDomicilioCorresp = nodo.GetElementsByTagName("DOMICILIO_CORRESP");
                foreach (XmlElement nodoDomicilioCorresp in XmlNodeListDomicilioCorresp)
                {
                        cajaCarteraDomicilioCorresp = new Entities.CajaCarteraDomicilioCorresp();
                      //  cajaCarteraDomicilioCorresp.IdCajaCarteraEnc = cajaCarteraEnc.Id;/*Modificar con el id del encabezado*/
                        XmlNodeList = nodoDomicilioCorresp.GetElementsByTagName("DIRECCION");
                    if (XmlNodeList.Count != 0)
                    {
                        cajaCarteraDomicilioCorresp.Direccion = XmlNodeList[0].InnerText;
                    }

                    XmlNodeList = nodoDomicilioCorresp.GetElementsByTagName("LOCALIDAD");
                    if (XmlNodeList.Count != 0)
                    {
                        cajaCarteraDomicilioCorresp.Localidad = XmlNodeList[0].InnerText;
                    }

                    XmlNodeList = nodoDomicilioCorresp.GetElementsByTagName("CODIGO_POSTAL");
                    if (XmlNodeList.Count != 0)
                    {
                        cajaCarteraDomicilioCorresp.CodigoPostal = XmlNodeList[0].InnerText;
                    }

                    XmlNodeList = nodoDomicilioCorresp.GetElementsByTagName("CODIGO_PROVINCIA");
                    if (XmlNodeList.Count != 0)
                    {
                        aux = XmlNodeList[0].InnerText;
                        if (aux != "") { cajaCarteraDomicilioCorresp.CodigoProvincia = Int32.Parse(aux); } else { cajaCarteraDomicilioCorresp.CodigoProvincia = 0; }
                    }

                    XmlNodeList = nodoDomicilioCorresp.GetElementsByTagName("TELEFONO");
                    if (XmlNodeList.Count != 0)
                    {
                        cajaCarteraDomicilioCorresp.Telefono = XmlNodeList[0].InnerText;
                    }
                    lCajaCarteraDomicilioCorresp.Add(cajaCarteraDomicilioCorresp);
                    

                }
                    cajaCarteraDet.CajaCarteraDomicilioCorresp = lCajaCarteraDomicilioCorresp;
                    lCajaCarteraDomicilioCorresp = new List<Entities.CajaCarteraDomicilioCorresp>();
                    cajaCarteraAuto = new Entities.CajaCarteraAuto();
                    
                    XmlNodeListAuto = nodo.GetElementsByTagName("AUTO");
                foreach (XmlElement nodoAuto in XmlNodeListAuto)
                {
                        cajaCarteraAuto = new Entities.CajaCarteraAuto();
                       // cajaCarteraAuto.IdCajaCarteraEnc = cajaCarteraEnc.Id;/*Modificar con el id del encabezado*/
                        XmlNodeList = nodoAuto.GetElementsByTagName("PATENTE");
                    if (XmlNodeList.Count != 0)
                    {
                        cajaCarteraAuto.Patente = XmlNodeList[0].InnerText;
                    }

                    XmlNodeList = nodoAuto.GetElementsByTagName("MARCA");
                    if (XmlNodeList.Count != 0)
                    {
                        aux = XmlNodeList[0].InnerText;
                        if (aux != "") { cajaCarteraAuto.Marca = Int32.Parse(aux); } else { cajaCarteraAuto.Marca = 0; }
                    }

                    XmlNodeList = nodoAuto.GetElementsByTagName("DESC_MARCA");
                    if (XmlNodeList.Count != 0)
                    {
                        cajaCarteraAuto.DescMarca = XmlNodeList[0].InnerText;
                    }

                    XmlNodeList = nodoAuto.GetElementsByTagName("MODELO");
                    if (XmlNodeList.Count != 0)
                    {
                        aux = XmlNodeList[0].InnerText;
                        if (aux != "") { cajaCarteraAuto.Modelo = Int32.Parse(aux); } else { cajaCarteraAuto.Modelo = 0; }
                    }
                    XmlNodeList = nodoAuto.GetElementsByTagName("SUMA_ASEGURADA");
                    if (XmlNodeList.Count != 0)
                    {
                        aux = XmlNodeList[0].InnerText;
                        if (aux != "") { cajaCarteraAuto.SumaAsegurada = Decimal.Parse(aux); } else { cajaCarteraAuto.SumaAsegurada = 0; }
                    }
                    XmlNodeList = nodoAuto.GetElementsByTagName("TIPO_VEHICULO");
                    if (XmlNodeList.Count != 0)
                    {
                        aux = XmlNodeList[0].InnerText;
                        if (aux != "") { cajaCarteraAuto.TipoVehiculo = Int32.Parse(aux); } else { cajaCarteraAuto.TipoVehiculo = 0; }
                    }
                    XmlNodeList = nodoAuto.GetElementsByTagName("USO_VEHICULO");
                    if (XmlNodeList.Count != 0)
                    {
                        aux = XmlNodeList[0].InnerText;
                        if (aux != "") { cajaCarteraAuto.UsoVehiculo = Int32.Parse(aux); } else { cajaCarteraAuto.UsoVehiculo = 0; }
                    }
                    XmlNodeList = nodoAuto.GetElementsByTagName("CLASE_VEHICULO");
                    if (XmlNodeList.Count != 0)
                    {
                        aux = XmlNodeList[0].InnerText;
                        if (aux != "") { cajaCarteraAuto.ClaseVehiculo = Int32.Parse(aux); } else { cajaCarteraAuto.ClaseVehiculo = 0; }
                    }
                    XmlNodeList = nodoAuto.GetElementsByTagName("MOTOR");
                    if (XmlNodeList.Count != 0)
                    {
                        cajaCarteraAuto.Motor = XmlNodeList[0].InnerText;
                    }
                    XmlNodeList = nodoAuto.GetElementsByTagName("CHASIS");
                    if (XmlNodeList.Count != 0)
                    {
                        cajaCarteraAuto.Chasis = XmlNodeList[0].InnerText;
                    }
                    XmlNodeList = nodoAuto.GetElementsByTagName("MCA_CERO_KM");
                    if (XmlNodeList.Count != 0)
                    {
                        cajaCarteraAuto.McaCeroKm = XmlNodeList[0].InnerText;
                    }
                    XmlNodeList = nodoAuto.GetElementsByTagName("COD_INFOAUTO");
                    if (XmlNodeList.Count != 0)
                    {
                        aux = XmlNodeList[0].InnerText;
                        if (aux != "") { cajaCarteraAuto.CodInfoauto = Int32.Parse(aux); } else { cajaCarteraAuto.CodInfoauto = 0; }
                    }
                    lCajaCarteraAuto.Add(cajaCarteraAuto);
                    

                }
                    cajaCarteraDet.CajaCarteraAuto = lCajaCarteraAuto;
                    lCajaCarteraAuto = new List<Entities.CajaCarteraAuto>();
                    cajaCarteraAccesorio = new Entities.CajaCarteraAccesorio();
                    
                    XmlNodeListAccesorios = nodo.GetElementsByTagName("ACCESORIOS");
                foreach (XmlElement nodoAccesorios in XmlNodeListAccesorios)
                {
                    foreach (XmlElement nodoAccesorio in nodoAccesorios)
                    {
                            cajaCarteraAccesorio = new Entities.CajaCarteraAccesorio();
                           // cajaCarteraAccesorio.IdCajaCarteraEnc = cajaCarteraEnc.Id;/*Modificar con el id del encabezado*/
                            XmlNodeList = nodoAccesorio.GetElementsByTagName("CODIGO");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { cajaCarteraAccesorio.Codigo = Int32.Parse(aux); } else { cajaCarteraAccesorio.Codigo = 0; }
                        }
                        XmlNodeList = nodoAccesorio.GetElementsByTagName("VALOR");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { cajaCarteraAccesorio.Valor = Int32.Parse(aux); } else { cajaCarteraAccesorio.Valor = 0; }
                        }
                        lCajaCarteraAccesorio.Add(cajaCarteraAccesorio);
                    }
                    
                }
                    cajaCarteraDet.CajaCarteraAccesorio = lCajaCarteraAccesorio;
                    lCajaCarteraAccesorio = new List<Entities.CajaCarteraAccesorio>();
                    cajaCarteraCuota = new Entities.CajaCarteraCuota();
                    
                    XmlNodeListCuota = nodo.GetElementsByTagName("CUOTA");
                foreach (XmlElement nodoCuota in XmlNodeListCuota)
                {
                        cajaCarteraCuota = new Entities.CajaCarteraCuota();
                       // cajaCarteraCuota.IdCajaCarteraEnc = cajaCarteraEnc.Id;/*Modificar con el id del encabezado*/
                        XmlNodeList = nodoCuota.GetElementsByTagName("NUM_CUOTA");
                    if (XmlNodeList.Count != 0)
                    {
                        aux = XmlNodeList[0].InnerText;
                        if (aux != "") { cajaCarteraCuota.NumCuota = Int32.Parse(aux); } else { cajaCarteraCuota.NumCuota = 0; }
                    }
                    XmlNodeList = nodoCuota.GetElementsByTagName("FECHAVTO");
                    if (XmlNodeList.Count != 0)
                    {
                        aux = XmlNodeList[0].InnerText;
                        try
                        {
                            auxFecha = DateTime.Parse(aux);
                            if (auxFecha == DateTime.Parse("01/01/0001"))
                            {
                                auxFecha = DateTime.Parse("01/01/1900");
                            }
                            cajaCarteraCuota.FechaVto = auxFecha;
                        }
                        catch
                        {
                            cajaCarteraCuota.FechaVto = DateTime.Parse("01/01/1900");
                        }

                            //if (aux != "") { cajaCarteraCuota.FechaVto = DateTime.Parse(aux); } /*else { cajaCarteraCuota.FechaVto = DateTime.Parse(null); }*/
                    } else { cajaCarteraCuota.FechaVto = DateTime.Parse("01/01/1900"); }
                    XmlNodeList = nodoCuota.GetElementsByTagName("SITUACION");
                    if (XmlNodeList.Count != 0)
                    {
                        cajaCarteraCuota.Situacion = XmlNodeList[0].InnerText;
                    }
                    XmlNodeList = nodoCuota.GetElementsByTagName("PRIMA");
                    if (XmlNodeList.Count != 0)
                    {
                        aux = XmlNodeList[0].InnerText;
                        if (aux != "") { aux = aux.Replace(".", ","); cajaCarteraCuota.Prima = Decimal.Parse(aux); } else { cajaCarteraCuota.Prima = 0; }
                    }
                    XmlNodeList = nodoCuota.GetElementsByTagName("COMISION");
                    if (XmlNodeList.Count != 0)
                    {
                        aux = XmlNodeList[0].InnerText;
                        if (aux != "") { aux = aux.Replace(".", ","); cajaCarteraCuota.Comision = Decimal.Parse(aux); } else { cajaCarteraCuota.Comision = 0; }
                    }
                    XmlNodeList = nodoCuota.GetElementsByTagName("PREMIO");
                    if (XmlNodeList.Count != 0)
                    {
                        aux = XmlNodeList[0].InnerText;
                        if (aux != "") { aux = aux.Replace(".", ","); cajaCarteraCuota.Premio = Decimal.Parse(aux); } else { cajaCarteraCuota.Premio = 0; }
                    }
                    XmlNodeList = nodoCuota.GetElementsByTagName("PORC_INFLACION");
                    if (XmlNodeList.Count != 0)
                    {
                        aux = XmlNodeList[0].InnerText;
                        if (aux != "") { aux = aux.Replace(".", ","); cajaCarteraCuota.PorcInflacion = Decimal.Parse(aux); } else { cajaCarteraCuota.PorcInflacion = 0; }
                    }
                    lCajaCarteraCuota.Add(cajaCarteraCuota);
                    

                }
                    cajaCarteraDet.CajaCarteraCuota = lCajaCarteraCuota;
                    lCajaCarteraCuota = new List<Entities.CajaCarteraCuota>();
                    cajaCarteraCuotas = new Entities.CajaCarteraCuotas();
                    
                    XmlNodeListCuotas = nodo.GetElementsByTagName("CUOTAS");
                foreach (XmlElement nodoCuotas in XmlNodeListCuotas)
                {
                    foreach (XmlElement nodoCuota in nodoCuotas)
                    {
                            cajaCarteraCuotas = new Entities.CajaCarteraCuotas();
                            //cajaCarteraCuotas.IdCajaCarteraEnc = cajaCarteraEnc.Id;/*Modificar con el id del encabezado*/
                            XmlNodeList = nodoCuota.GetElementsByTagName("NUM_CUOTA");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { cajaCarteraCuotas.NumCuota = Int32.Parse(aux); } else { cajaCarteraCuotas.NumCuota = 0; }
                        }
                        XmlNodeList = nodoCuota.GetElementsByTagName("FECHAVTO");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            try
                            {
                                    auxFecha = DateTime.Parse(aux);
                                    if (auxFecha == DateTime.Parse("01/01/0001"))
                                    {
                                        auxFecha = DateTime.Parse("01/01/1900");
                                    }
                                    cajaCarteraCuotas.FechaVto = auxFecha;
                            }
                            catch
                            {
                                cajaCarteraCuotas.FechaVto = DateTime.Parse("01/01/1900");
                            }

                             //   if (aux != "") { cajaCarteraCuotas.FechaVto = DateTime.Parse(aux); } /*else { cajaCarteraCuotas.FechaVto = DateTime.Parse(null); }*/
                        } else { cajaCarteraCuotas.FechaVto = DateTime.Parse("01/01/1900"); }
                        XmlNodeList = nodoCuota.GetElementsByTagName("SITUACION");
                        if (XmlNodeList.Count != 0)
                        {
                            cajaCarteraCuotas.Situacion = XmlNodeList[0].InnerText;
                        }
                        XmlNodeList = nodoCuota.GetElementsByTagName("PRIMA");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { aux = aux.Replace(".", ","); cajaCarteraCuotas.Prima = Decimal.Parse(aux); } else { cajaCarteraCuotas.Prima = 0; }
                        }
                        XmlNodeList = nodoCuota.GetElementsByTagName("COMISION");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { aux = aux.Replace(".", ","); cajaCarteraCuotas.Comision = Decimal.Parse(aux); } else { cajaCarteraCuotas.Comision = 0; }
                        }
                        XmlNodeList = nodoCuota.GetElementsByTagName("PREMIO");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { aux = aux.Replace(".", ","); cajaCarteraCuotas.Premio = Decimal.Parse(aux); } else { cajaCarteraCuotas.Premio = 0; }
                        }
                        XmlNodeList = nodoCuota.GetElementsByTagName("PORC_INFLACION");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { aux = aux.Replace(".", ","); cajaCarteraCuotas.PorcInflacion = Decimal.Parse(aux); } else { cajaCarteraCuotas.PorcInflacion = 0; }
                        }
                        lCajaCarteraCuotas.Add(cajaCarteraCuotas);
                    }
                    cajaCarteraDet.CajaCarteraCuotas = lCajaCarteraCuotas;
                        lCajaCarteraCuotas = new List<Entities.CajaCarteraCuotas>();
                    }

                    lCajaCarteraDet.Add(cajaCarteraDet);
                    cajaCarteraDet = new Entities.CajaCarteraDet();

                }
            }
            /*Aca cierra el ciclo principal*/
            /*Guarda cada detalle informado en el XML*/
            //_context.CajaCarteraDet.Add(lCajaCarteraDet);
            _context.CajaCarteraDet.AddRange(lCajaCarteraDet); // = lCajaCarteraDet;

            try
            {
                await _context.SaveChangesAsync();
                //await _context.BulkSaveChangesAsync();
                //_context.CajaCarteraDet.c
                //cajaCarteraDet = new Entities.CajaCarteraDet();


            }
            catch (Exception e)
            {
                //return StatusCode(500, e.InnerException.Message);
                error = error + " - " + e.InnerException.Message;
                //error = "Entro por error";
            }
                


            if (error != "")
            {
                return StatusCode(205, "Error: " + error);
            } else
            {
                return StatusCode(200, contenido);
            }
           
        }

    }
}
