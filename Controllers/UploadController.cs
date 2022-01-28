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

namespace eCore.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {

        private readonly SecureDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UploadController(SecureDbContext context, IMapper mapper, IUserService userService)
        {
            _context = context;
            _mapper = mapper;
            _userService = userService;
        }

        // GET: api/Upload
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1Upload", "value2Upload" };
        }

        // GET: api/Upload/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // GET: api/Upload/Procesado/"Archivo"
        [HttpGet("Procesado/{file}", Name = "File")]
        public async Task<string> Get(string file)
        //public async Task<ActionResult<DetalleDTO<AcceptedResult>>> Get(string file)
        {
            try
            {
                /*Buscar si el archivo ya fue procesado*/
                var encabezadoRepetido = _context.AllianzCarteraEnc.FirstOrDefault(x => x.Archivo.Equals(file));

                if (encabezadoRepetido == null)
                {
                    //return StatusCode(201, "No se proceso el archivo anteriormente");
                    return "201";
                } else
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

        // POST: api/Upload
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

                    await this.procesarAllianzCarteraAsync(fullPath, fileName, user);

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

        // PUT: api/Upload/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }


        public async Task<string> procesarAllianzCarteraAsync(string path, string fileName, string user)
        {

            string contenido = "";
            string error = "";
            //Recupero y valido el usuario del token
            //string userConnected = _userService.GetUserByToken(Request.Headers["Authorization"]);
            string userConnected = user;
            
            Entities.AllianzCarteraEnc allianzCarteraEnc = new Entities.AllianzCarteraEnc();
            allianzCarteraEnc.Archivo = fileName;//path;
            allianzCarteraEnc.Usuario = userConnected;
            allianzCarteraEnc.FechaProceso = DateTime.Today;

            /*Buscar si el archivo ya fue procesado*/
            var encabezadoRepetido = _context.AllianzCarteraEnc.FirstOrDefault(x => x.Archivo.Equals(fileName));

            if (encabezadoRepetido != null)
            {

                try
                {
                    _context.AllianzCarteraDet.Where(x => x.IdAllianzCarteraEnc.Equals(encabezadoRepetido.Id)).ToList().ForEach(p => _context.AllianzCarteraDet.Remove(p));
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



            _context.AllianzCarteraEnc.Add(allianzCarteraEnc);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                error = e.InnerException.Message;
            }

            Entities.AllianzCarteraDet allianzCarteraDet = new Entities.AllianzCarteraDet();

            using (StreamReader oSR = System.IO.File.OpenText(path))
            {
                string s = "";
                double reng = 0;
                string aux = "";
                //DateTime dBNull = System.DateTime.MinValue;
                DateTime dBNull = DateTime.Parse("01/01/1900");

                while ((s = oSR.ReadLine()) != null)
                {
                    //contenido = s;
                    /*Aca debo guardar cada registro*/
                    if (reng > 0)
                    {
                        String[] parametros = s.Split("\",", StringSplitOptions.RemoveEmptyEntries);
                        allianzCarteraDet.IdAllianzCarteraEnc = allianzCarteraEnc.Id;
                        allianzCarteraDet.Productor = parametros[0].Replace("\"", "");
                        allianzCarteraDet.Organizador = parametros[1].Replace("\"", "");
                        allianzCarteraDet.Seccion = parametros[2].Replace("\"", "");
                        allianzCarteraDet.Poliza = parametros[3].Replace("\"", "");
                        aux = ((parametros[4].Replace("\"", "")).Replace(",", "")).Replace(".", ",");
                        if (aux != "") { allianzCarteraDet.Endoso = Int32.Parse(aux); } else { allianzCarteraDet.Endoso = 0; };

                        allianzCarteraDet.ClaseEndoso = parametros[5].Replace("\"", "");
                        allianzCarteraDet.NombreDelAsegurado = parametros[6].Replace("\"", "");

                        aux = parametros[7].Replace("\"", "");
                        if (aux != "") { allianzCarteraDet.FecEmision = DateTime.Parse(aux); } else { allianzCarteraDet.FecEmision = dBNull; };
                        aux = parametros[8].Replace("\"", "");
                        if (aux != "") { allianzCarteraDet.FechaDesdeVigencia = DateTime.Parse(aux); } else { allianzCarteraDet.FechaDesdeVigencia = dBNull; };
                        aux = parametros[9].Replace("\"", "");
                        if (aux != "") { allianzCarteraDet.FechaHastaVigencia = DateTime.Parse(aux); } else { allianzCarteraDet.FechaHastaVigencia = dBNull; };

                        allianzCarteraDet.Estado = parametros[10].Replace("\"", "");
                        allianzCarteraDet.Moneda = parametros[11].Replace("\"", "");
                        aux = ((parametros[12].Replace("\"", "")).Replace(",", "")).Replace(".", ",");
                        if (aux != "") { allianzCarteraDet.TotalPrima = Decimal.Parse(aux); } else { allianzCarteraDet.TotalPrima = 0; };
                        aux = ((parametros[13].Replace("\"", "")).Replace(",", "")).Replace(".", ",");
                        if (aux.Trim() != "") { allianzCarteraDet.ComisionOrg = Decimal.Parse(aux); } else { allianzCarteraDet.ComisionOrg = 0; };
                        aux = ((parametros[14].Replace("\"", "")).Replace(",", "")).Replace(".", ",");
                        if (aux.Trim() != "") { allianzCarteraDet.ComisionProd = Decimal.Parse(aux); } else { allianzCarteraDet.ComisionProd = 0; };
                        aux = ((parametros[15].Replace("\"", "")).Replace(",", "")).Replace(".", ",");
                        if (aux.Trim() != "") { allianzCarteraDet.TotalPremio = Decimal.Parse(aux); } else { allianzCarteraDet.TotalPremio = 0; };
                        aux = ((parametros[16].Replace("\"", "")).Replace(",", "")).Replace(".", ",");
                        if (aux.Trim() != "") { allianzCarteraDet.TotalPagado = Decimal.Parse(aux); } else { allianzCarteraDet.TotalPagado = 0; };
                        aux = ((parametros[17].Replace("\"", "")).Replace(",", "")).Replace(".", ",");
                        if (aux.Trim() != "") { allianzCarteraDet.Saldo = Decimal.Parse(aux); } else { allianzCarteraDet.Saldo = 0; };

                        aux = ((parametros[18].Replace("\"", "")).Replace(",", "")).Replace(".", ",");
                        if (aux != "") { allianzCarteraDet.CantSiniestros = Int32.Parse(aux); } else { allianzCarteraDet.CantSiniestros = 0; };
                        aux = ((parametros[19].Replace("\"", "")).Replace(",", "")).Replace(".", ",");
                        if (aux != "") { allianzCarteraDet.CantDenuncias = Int32.Parse(aux); } else { allianzCarteraDet.CantDenuncias = 0; };

                        allianzCarteraDet.TipoDeDocumento = parametros[20].Replace("\"", "");
                        allianzCarteraDet.NumeroDeDocumento = parametros[21].Replace("\"", "");

                        aux = ((parametros[22].Replace("\"", "")).Replace(",", "")).Replace(".", ",");
                        if (aux != "") { allianzCarteraDet.CantCuotas = Int32.Parse(aux); } else { allianzCarteraDet.CantCuotas = 0; };

                        allianzCarteraDet.FormaDeCobro = parametros[23].Replace("\"", "");
                        allianzCarteraDet.TipoOperacion = parametros[24].Replace("\"", "");

                        aux = ((parametros[25].Replace("\"", "")).Replace(",", "")).Replace(".", ",");
                        if (aux.Trim() != "") { allianzCarteraDet.DerechoEmision = Decimal.Parse(aux); } else { allianzCarteraDet.DerechoEmision = 0; };
                        aux = ((parametros[26].Replace("\"", "")).Replace(",", "")).Replace(".", ",");
                        if (aux.Trim() != "") { allianzCarteraDet.GastosFinanc = Decimal.Parse(aux); } else { allianzCarteraDet.GastosFinanc = 0; };
                        aux = ((parametros[27].Replace("\"", "")).Replace(",", "")).Replace(".", ",");
                        if (aux.Trim() != "") { allianzCarteraDet.Iva = Decimal.Parse(aux); } else { allianzCarteraDet.Iva = 0; };
                        aux = ((parametros[28].Replace("\"", "")).Replace(",", "")).Replace(".", ",");
                        if (aux.Trim() != "") { allianzCarteraDet.Sellos = Decimal.Parse(aux); } else { allianzCarteraDet.Sellos = 0; };
                        aux = ((parametros[29].Replace("\"", "")).Replace(",", "")).Replace(".", ",");
                        if (aux.Trim() != "") { allianzCarteraDet.GastosAdm = Decimal.Parse(aux); } else { allianzCarteraDet.GastosAdm = 0; };
                        aux = ((parametros[30].Replace("\"", "")).Replace(",", "")).Replace(".", ",");
                        if (aux.Trim() != "") { allianzCarteraDet.SumaAsegurada = Decimal.Parse(aux); } else { allianzCarteraDet.SumaAsegurada = 0; };
                        aux = ((parametros[31].Replace("\"", "")).Replace(",", "")).Replace(".", ",");
                        if (aux.Trim() != "") { allianzCarteraDet.ValorDeReferencia = Decimal.Parse(aux); } else { allianzCarteraDet.ValorDeReferencia = 0; };

                        allianzCarteraDet.TipoPoliza = parametros[32].Replace("\"", "");
                        allianzCarteraDet.Cuatrimestre = parametros[33].Replace("\"", "");
                        allianzCarteraDet.EstadoSolicitud = parametros[34].Replace("\"", "");

                        aux = parametros[35].Replace("\"", "");
                        if (aux != "") { allianzCarteraDet.FechaDespImp = DateTime.Parse(aux); } else { allianzCarteraDet.FechaDespImp = dBNull; };

                        allianzCarteraDet.Propuesta = parametros[36].Replace("\"", "");
                        allianzCarteraDet.Linea = parametros[37].Replace("\"", "");
                        allianzCarteraDet.PolizaRenovada = parametros[38].Replace("\"", "");

                        aux = ((parametros[39].Replace("\"", "")).Replace(",", "")).Replace(".", ",");
                        if (aux != "") { allianzCarteraDet.CantCuotas2 = Int32.Parse(aux); } else { allianzCarteraDet.CantCuotas2 = 0; };

                        aux = ((parametros[40].Replace("\"", "")).Replace(",", "")).Replace(".", ",");
                        if (aux.Trim() != "") { allianzCarteraDet.Porc1erCuota = Decimal.Parse(aux); } else { allianzCarteraDet.Porc1erCuota = 0; };

                        aux = ((parametros[41].Replace("\"", "")).Replace(",", "")).Replace(".", ",");
                        if (aux != "") { allianzCarteraDet.Venc1eraCuota = Int32.Parse(aux); } else { allianzCarteraDet.Venc1eraCuota = 0; };

                        allianzCarteraDet.PlanPago = parametros[42].Replace("\"", "");
                        aux = ((parametros[43].Replace("\"", "")).Replace(",", "")).Replace(".", ",");
                        if (aux.Trim() != "") { allianzCarteraDet.NroInterno = Decimal.Parse(aux); } else { allianzCarteraDet.NroInterno = 0; };

                        aux = parametros[44].Replace("\"", "");
                        if (aux != "") { allianzCarteraDet.FechaVtoPoliza = DateTime.Parse(aux); } else { allianzCarteraDet.FechaVtoPoliza = dBNull; };

                        allianzCarteraDet.Patente = parametros[45].Replace("\"", "");
                        allianzCarteraDet.Marca = parametros[46].Replace("\"", "");
                        allianzCarteraDet.Modelo = parametros[47].Replace("\"", "");
                        allianzCarteraDet.Motor = parametros[48].Replace("\"", "");
                        allianzCarteraDet.Chasis = parametros[49].Replace("\"", "");
                        allianzCarteraDet.Uso = parametros[50].Replace("\"", "");
                        allianzCarteraDet.Cobertura = parametros[51].Replace("\"", "");

                        aux = ((parametros[52].Replace("\"", "")).Replace(",", "")).Replace(".", ",");
                        if (aux.Trim() != "") { allianzCarteraDet.SumaAsegurada2 = Decimal.Parse(aux); } else { allianzCarteraDet.SumaAsegurada2 = 0; };
                        aux = ((parametros[53].Replace("\"", "")).Replace(",", "")).Replace(".", ",");
                        if (aux.Trim() != "") { allianzCarteraDet.ValorDeReferencia2 = Decimal.Parse(aux); } else { allianzCarteraDet.ValorDeReferencia2 = 0; };
                        aux = ((parametros[54].Replace("\"", "")).Replace(",", "")).Replace(".", ",");
                        if (aux != "") { allianzCarteraDet.Item = Int32.Parse(aux); } else { allianzCarteraDet.Item = 0; };


                        aux = ((parametros[55].Replace("\"", "")).Replace(",", "")).Replace(".", ",");
                        if (aux.Trim() != "") { allianzCarteraDet.Infoauto = Decimal.Parse(aux); } else { allianzCarteraDet.Infoauto = 0; };

                        aux = parametros[56].Replace("\"", "");
                        if (aux != "") { allianzCarteraDet.FechaFinPrestamo = DateTime.Parse(aux); } else { allianzCarteraDet.FechaFinPrestamo = dBNull; };

                        _context.AllianzCarteraDet.Add(allianzCarteraDet);

                        try
                        {
                            await _context.SaveChangesAsync();
                            allianzCarteraDet = new Entities.AllianzCarteraDet();
                        }
                        catch (Exception e)
                        {
                            // return StatusCode(500, e.InnerException.Message);
                            error = error + " - " + e.InnerException.Message;
                        }

                    } //if (reng > 0)

                    reng = reng + 1;
                }
            }

            return contenido;
        }

    }
}
