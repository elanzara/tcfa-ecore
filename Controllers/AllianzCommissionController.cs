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
    public class AllianzCommissionController : ControllerBase
    {

        private readonly SecureDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public AllianzCommissionController(SecureDbContext context, IMapper mapper, IUserService userService)
        {
            _context = context;
            _mapper = mapper;
            _userService = userService;
        }

        // GET: api/AllianzCommission
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1Commission", "value2Commission" };
        }

        // GET: api/AllianzCommission/5
        [HttpGet("{id}", Name = "GetAllianzCommission")]
        public string Get(int id)
        {
            return "value";
        }

        // GET: api/AllianzCommission/ProcesadoCommission/"Archivo"
        [HttpGet("ProcesadoCommission/{file}", Name = "FileAllianzCommission")]
        public async Task<string> Get(string file)
        //public async Task<ActionResult<DetalleDTO<AcceptedResult>>> Get(string file)
        {
            try
            {
                /*Buscar si el archivo ya fue procesado*/
                var encabezadoRepetido = _context.AllianzComisionesEnc.FirstOrDefault(x => x.Archivo.Equals(file));

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

        // POST: api/AllianzCommission
        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> UploadAsync()
        {
            try
            {
                var file = Request.Form.Files[0];
                //var user = (new System.Collections.Generic.IDictionaryDebugView<string, string>(((System.Collections.Generic.Dictionary<string, string>.KeyCollection)Request.Cookies.Keys)._dictionary).Items[0]).Value;
                // var user = (((Request.Cookies.Keys)._dictionary).Items[0]).Value;
                // var keys = Request.Cookies.Keys;

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

                    await this.procesarAllianzComisionesAsync(fullPath, fileName);

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

        // PUT: api/AllianzCommission/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/AllianzCommission/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }


        public async Task<string> procesarAllianzComisionesAsync(string path, string fileName)
        {

            string contenido = "";
            string error = "";
            //Recupero y valido el usuario del token
            //string userConnected = _userService.GetUserByToken(Request.Headers["Authorization"]);
            string userConnected = "Edu";
            Entities.AllianzComisionesEnc allianzComisionesEnc = new Entities.AllianzComisionesEnc();
            allianzComisionesEnc.Archivo = fileName;//path;
            allianzComisionesEnc.Usuario = userConnected;
            allianzComisionesEnc.FechaProceso = DateTime.Today;

            /*Buscar si el archivo ya fue procesado*/
            var encabezadoRepetido = _context.AllianzComisionesEnc.FirstOrDefault(x => x.Archivo.Equals(fileName));

            if (encabezadoRepetido != null)
            {

                try
                {
                    _context.AllianzComisionesDet.Where(x => x.IdAllianzComisionesEnc.Equals(encabezadoRepetido.Id)).ToList().ForEach(p => _context.AllianzComisionesDet.Remove(p));
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



            _context.AllianzComisionesEnc.Add(allianzComisionesEnc);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                error = e.InnerException.Message;
            }

            Entities.AllianzComisionesDet allianzComisionesDet = new Entities.AllianzComisionesDet();

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
                        allianzComisionesDet.IdAllianzComisionesEnc = allianzComisionesEnc.Id;
                        allianzComisionesDet.Organizador = parametros[0].Replace("\"", "");
                        allianzComisionesDet.Productor = parametros[1].Replace("\"", "");
                        allianzComisionesDet.Tipo = parametros[2].Replace("\"", "");
                        aux = parametros[3].Replace("\"", "");
                        if (aux != "") { allianzComisionesDet.Fecha = DateTime.Parse(aux); } else { allianzComisionesDet.Fecha = dBNull; };
                        allianzComisionesDet.Seccion = parametros[4].Replace("\"", "");
                        allianzComisionesDet.NroPoliza = parametros[5].Replace("\"", "");
                        aux = ((parametros[6].Replace("\"", "")).Replace(",", "")).Replace(".", ",");
                        if (aux != "") { allianzComisionesDet.Endoso = Int32.Parse(aux); } else { allianzComisionesDet.Endoso = 0; };
                        allianzComisionesDet.Asegurado = parametros[7].Replace("\"", "");
                        allianzComisionesDet.Mda = parametros[8].Replace("\"", "");
                        aux = ((parametros[9].Replace("\"", "")).Replace(",", "")).Replace(".", ",");
                        if (aux != "") { allianzComisionesDet.TipoCambio = Int32.Parse(aux); } else { allianzComisionesDet.TipoCambio = 0; };
                        aux = ((parametros[10].Replace("\"", "")).Replace(",", "")).Replace(".", ",");
                        if (aux.Trim() != "") { allianzComisionesDet.Premio = Decimal.Parse(aux); } else { allianzComisionesDet.Premio = 0; };
                        aux = ((parametros[11].Replace("\"", "")).Replace(",", "")).Replace(".", ",");
                        if (aux.Trim() != "") { allianzComisionesDet.Prima = Decimal.Parse(aux); } else { allianzComisionesDet.Prima = 0; };
                        aux = ((parametros[12].Replace("\"", "")).Replace(",", "")).Replace(".", ",");
                        if (aux.Trim() != "") { allianzComisionesDet.ComisionesDevengadas = Decimal.Parse(aux); } else { allianzComisionesDet.ComisionesDevengadas = 0; };
                        aux = ((parametros[13].Replace("\"", "")).Replace(",", "")).Replace(".", ",");
                        if (aux.Trim() != "") { allianzComisionesDet.ComisionesDevengadasPesos = Decimal.Parse(aux); } else { allianzComisionesDet.ComisionesDevengadasPesos = 0; };
                        aux = ((parametros[14].Replace("\"", "")).Replace(",", "")).Replace(".", ",");
                        if (aux.Trim() != "") { allianzComisionesDet.OSSEG = Decimal.Parse(aux); } else { allianzComisionesDet.OSSEG = 0; };
                        aux = ((parametros[15].Replace("\"", "")).Replace(",", "")).Replace(".", ",");
                        if (aux.Trim() != "") { allianzComisionesDet.IBAgente = Decimal.Parse(aux); } else { allianzComisionesDet.IBAgente = 0; };
                        aux = ((parametros[16].Replace("\"", "")).Replace(",", "")).Replace(".", ",");
                        if (aux.Trim() != "") { allianzComisionesDet.IBRiesgo = Decimal.Parse(aux); } else { allianzComisionesDet.IBRiesgo = 0; };
                        allianzComisionesDet.ProvinciaRiesgo = parametros[17].Replace("\"", "");
                        aux = ((parametros[18].Replace("\"", "")).Replace(",", "")).Replace(".", ",");
                        if (aux.Trim() != "") { allianzComisionesDet.NetoAcreditado = Decimal.Parse(aux); } else { allianzComisionesDet.NetoAcreditado = 0; };
                        aux = ((parametros[19].Replace("\"", "")).Replace(",", "")).Replace(".", ",");
                        if (aux.Trim() != "") { allianzComisionesDet.NetoAcreditadoPesos = Decimal.Parse(aux); } else { allianzComisionesDet.NetoAcreditadoPesos = 0; };
                        allianzComisionesDet.FPago = parametros[20].Replace("\"", "");

                        _context.AllianzComisionesDet.Add(allianzComisionesDet);

                        try
                        {
                            await _context.SaveChangesAsync();
                            allianzComisionesDet = new Entities.AllianzComisionesDet();
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

