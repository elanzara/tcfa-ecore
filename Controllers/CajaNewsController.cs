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
using System.Net.Http;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace eCore.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CajaNewsController : ControllerBase
    {

        private readonly SecureDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IConfiguration Configuration;

        public CajaNewsController(SecureDbContext context, IMapper mapper, IUserService userService, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _userService = userService;
            Configuration = configuration;
        }

        // GET: api/AllianzCommission
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1Commission", "value2Commission" };
        }

        // GET: api/AllianzCommission/5
        //[HttpGet("{id}", Name = "GetAllianzCommission")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // GET: api/AllianzCommission/ProcesadoCommission/"Archivo"
        //[HttpGet("Procesado/{file}", Name = "FileAllianzCommission")]
        //public async Task<string> Get(string file)
        ////public async Task<ActionResult<DetalleDTO<AcceptedResult>>> Get(string file)
        //{
        //    try
        //    {
        //        /*Buscar si el archivo ya fue procesado*/
        //        var encabezadoRepetido = _context.AllianzComisionesEnc.FirstOrDefault(x => x.Archivo.Equals(file));

        //        if (encabezadoRepetido == null)
        //        {
        //            //return StatusCode(201, "No se proceso el archivo anteriormente");
        //            return "201";
        //        }
        //        else
        //        {
        //            //return StatusCode(200, "Se proceso el archivo anteriormente");
        //            return "200";
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        //return StatusCode (500, "Internal server error: " + ex.Message);
        //        return "500: Internal Server Error: " + ex.Message;

        //    }
        //    //return "value";
        //}

        // POST: api/CajaNews
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

                    /*Aca ebo hacer el llamado al servicio*/
                    var resp = await this.procesarCajaNewsAsync(fullPath, fileName);

                    //return Ok(new { dbPath });
                    return Ok(new { resp });
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


        public async Task<string> procesarCajaNewsAsync(string path, string fileName)
        {

            string contenido = "";
            /*Aca engo que hacer el llamado al servicio*/
            string error = "";


            var url = Configuration["cajanew:url"];
            string xml =  "";
            xml = "<soap:Envelope xmlns:soap=\"http://www.w3.org/2003/05/soap-envelope\" xmlns:toy=\"http://www.toyotacfa.com.ar/\">";
            xml = xml + "<soap:Header/>";
            xml = xml + "<soap:Body>";
            xml = xml + "<toy:f_procesar_novedades>";
            xml = xml + "<toy:p_canal_c>WEB</toy:p_canal_c>";
            xml = xml + "</toy:f_procesar_novedades>";
            xml = xml + "</soap:Body>";
            xml = xml + "</soap:Envelope>";

            // Wrap our JSON inside a StringContent which then can be used by the HttpClient class
            HttpContent httpContent = new StringContent(xml, Encoding.UTF8, "application/xml");

            using (var httpClient = new HttpClient())
            {

                // Do the actual request and await the response
                var httpResponse = await httpClient.PostAsync(url, httpContent);

                if (httpResponse.Content != null)
                {
                    var responseContent = await httpResponse.Content.ReadAsStringAsync();
                    contenido = responseContent;
                }
            }
            return contenido;
        }

    }
}

