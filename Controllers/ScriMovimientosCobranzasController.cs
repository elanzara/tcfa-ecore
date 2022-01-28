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
    public class ScriMovimientosCobranzasController : ControllerBase
    {
        private readonly SecureDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IConfiguration Configuration;

        public ScriMovimientosCobranzasController(SecureDbContext context, IMapper mapper, IUserService userService, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _userService = userService;
            Configuration = configuration;
        }

        //Task<ActionResult<IEnumerable<TrfNovedades>>>
        // GET: api/ScriMovimientos/
        [HttpGet("fechamax/", Name = "scrimovimientoscobranzas")]
        public async Task<IEnumerable<ScriMovimientosCobranzas>> GetScriMovimientosCobranzas()
        {
            /*CAMBIAR EL QUERY Y LA CLASE*/
            return _context.ScriMovimientosCobranzas.FromSql("SELECT top 1 * from scri_movimientosCobranzas order by Ext_ApplicationDate desc");
        }

        // GET: api/ScriMovimientosCobranzas/
        [HttpGet("taxcobranzas/", Name = "scritaxcobranzas")]
        //public async Task<IEnumerable<ScriTaxId>> GetTax()
        //public async Task<ActionResult<ScriTaxId>> GetTax()
        public async Task<List<ScriTaxId>> GetTaxCobranzas()
        {
            //var scriTaxId = new ScriTaxId();
            //scriTaxId =  _context.ScriTaxId.FromSql("SELECT Id, TaxId from ScriTaxId order by Id");
            var scriTaxId = _context.ScriTaxId.FromSql("SELECT Id, TaxId from ScriTaxId order by Id").ToListAsync();
            //var scriTaxId = await _context.ScriTaxId.AllAsync(); //.FirstOrDefault();
            return await scriTaxId;
            //return _mapper.Map<List<ScriTaxId>>(scriTaxId);
        }

        // GET: api/ScriMovimientos/
        //[HttpGet("codigoproductor/", Name = "scricodigoproductor")]
        //public async Task<IEnumerable<ScriCodigoProductor>> GetCodigoProductor()
        //{
        //    return _context.ScriCodigoProductor.FromSql("SELECT id, CodigoProductor from scri_codigo_productor order by id");
        //}

        // GET: api/ScriMovimientosCobranzas
        [HttpGet, DisableRequestSizeLimit]
        public async Task<IActionResult> PostScriMovimientosCobranzas(string TaxID, string StartDate, string EndDate)
        {

            var url = Configuration["sancristobalcob:url"];
            var user = Configuration["sancristobalcob:user"];
            var pass = Configuration["sancristobalcob:pass"];
            //ScriMovimientosCobranzasOut scriMovimientosCobranzasOut = new ScriMovimientosCobranzasOut();

//            ScriDetMovimintosCobranzasOut scriDetMovimintosCobranzasOut2 = new ScriDetMovimintosCobranzasOut();

            var error = "";

            try
            {

                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("USERNAME", user);
                    httpClient.DefaultRequestHeaders.Add("PASS", pass);
                    //ScriMovimientosCobranzasOut scriMovimintosCobranzasOut = new ScriMovimientosCobranzasOut();
                    eCore.Services.Models.ScriCobranzasOut scriMovimintosCobranzasOut = new eCore.Services.Models.ScriCobranzasOut();

                    // Do the actual request and await the response
                    //var httpResponse = await httpClient.PostAsync(url, httpContent);
                    url = url + "?cuit=" + TaxID + "&fechaDesde=" + StartDate + "&fechaHasta=" + EndDate;
                    var httpResponse = await httpClient.GetAsync(url);

                    if (httpResponse.Content != null)
                    {
                        var responseContent = await httpResponse.Content.ReadAsStringAsync();

                        scriMovimintosCobranzasOut = JsonConvert.DeserializeObject<ScriCobranzasOut>(responseContent);


                        ScriMovimientos scriMovimientos = new ScriMovimientos();
                        Entities.ScriMessages scriMessages = new Entities.ScriMessages();
                        List<Entities.ScriMessages> cscriMessages = new List<Entities.ScriMessages>();
                        Entities.ScriMovimientosCobranzas scriMovimientosCobranzas = new Entities.ScriMovimientosCobranzas();
                        List < Entities.ScriMovimientosCobranzas> cscriMovimientosCobranzas = new List<Entities.ScriMovimientosCobranzas>();

                        int i = 0;
                        int aux = 0;
                        string saux;
                        DateTime min = new DateTime();
                        min = DateTime.Parse("01/01/1900");
                        DateTime daux;
                        scriMovimientos.HasError = scriMovimintosCobranzasOut.HasError;
                        scriMovimientos.HasInformation = scriMovimintosCobranzasOut.HasInformation;
                        scriMovimientos.HasWarning = scriMovimintosCobranzasOut.HasWarning;

                        while (i < scriMovimintosCobranzasOut.Messages.Count())
                        {
                            scriMessages = new Entities.ScriMessages();
                            scriMessages.NombreServicio = scriMovimintosCobranzasOut.Messages[i].NombreServicio;
                            scriMessages.VersionServicio = scriMovimintosCobranzasOut.Messages[i].VersionServicio;
                            scriMessages.Description = scriMovimintosCobranzasOut.Messages[i].Description;
                            scriMessages.MessageBeautiful = scriMovimintosCobranzasOut.Messages[i].MessageBeautiful;
                            scriMessages.StackTrace = scriMovimintosCobranzasOut.Messages[i].StackTrace;
                            scriMessages.ErrorLevel = scriMovimintosCobranzasOut.Messages[i].ErrorLevel;

                            cscriMessages.Add(scriMessages);
                            if (scriMovimintosCobranzasOut.HasError == true)
                            {
                                error = scriMessages.Description;
                            }
                            i = i + 1;
                        }
                        scriMovimientos.ScriMessages = cscriMessages;

                        i = 0;
                        while (i < scriMovimintosCobranzasOut.MovimientosCobranzas.Count())
                        {
                            //scriMovimientosCobranzas.poliza = scriMovimintosCobranzasOut.MovimientosCobranzas[i].Poliza;
                            aux = 0;
                            while (aux < scriMovimintosCobranzasOut.MovimientosCobranzas[i].Pagos.Count())
                            {
                                scriMovimientosCobranzas.poliza = scriMovimintosCobranzasOut.MovimientosCobranzas[i].Poliza;
                                saux = scriMovimintosCobranzasOut.MovimientosCobranzas[i].Pagos[aux].PaymentAmount.Replace(" ars", "");
                                saux = saux.Replace(".", ",");
                                scriMovimientosCobranzas.PaymentAmount = Decimal.Parse(saux);
                                scriMovimientosCobranzas.Ext_ApplicationDate = DateTime.Parse(scriMovimintosCobranzasOut.MovimientosCobranzas[i].Pagos[aux].Ext_ApplicationDate);
                                daux = DateTime.Parse(scriMovimintosCobranzasOut.MovimientosCobranzas[i].Pagos[aux].ReversedDate);
                                
                                if (daux > min)
                                {
                                    scriMovimientosCobranzas.ReversedDate = DateTime.Parse(scriMovimintosCobranzasOut.MovimientosCobranzas[i].Pagos[aux].ReversedDate);
                                } 

                                //cscriMovimientosCobranzas.Add(scriMovimientosCobranzas);
                                _context.ScriMovimientosCobranzas.Add(scriMovimientosCobranzas);
                                //await _context.SaveChangesAsync();
                                scriMovimientosCobranzas = new ScriMovimientosCobranzas();
                                aux = aux + 1;
                            }
                            i = i + 1;
                        }

                        _context.ScriMovimientos.Add(scriMovimientos);
                        //scriMovimientos.ScriListJobSummary = cscriListJobSummary;
                        //_context.ScriMovimientos.Add(scriMovimientos);
                        try
                        {
                            await _context.SaveChangesAsync();
                        }
                        catch (Exception e)
                        {
                            error = e.InnerException.Message;
                        }
                    } //Cantidad
                    else
                    {
                        error = "No hay datos para los valores enviados";
                    }

                } //using

            }
            catch (Exception ex)
            {
                error = ex.Message.ToString();
            }

            if (error == "No hay datos para los valores enviados")
            {
                return StatusCode(204, error.ToString());
            }
            else if (error != "")
            {
                return StatusCode(205, new { data = error });

                //return StatusCode(StatusCodes.Status205ResetContent, StatusCodeResult.Equals(data, error));

                //return StatusCode(205, { "data": error.ToString()});
            }
            else
            {
                return StatusCode(200, "OK");
            }

        } //Metodo

    }
}
