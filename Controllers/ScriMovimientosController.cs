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
    public class ScriMovimientosController : ControllerBase
    {
        private readonly SecureDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IConfiguration Configuration;

        public ScriMovimientosController(SecureDbContext context, IMapper mapper, IUserService userService, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _userService = userService;
            Configuration = configuration;
        }

        //Task<ActionResult<IEnumerable<TrfNovedades>>>
        // GET: api/ScriMovimientos/
        [HttpGet("fechamax/", Name = "scrimovimientos")]
        public async Task<IEnumerable<ScriListJobSummary>> GetScriMovimientos()
        {
            return _context.ScriListJobSummary.FromSql("SELECT top 1 * from scri_list_job_summary order by StartDate desc");
        }

        // GET: api/ScriMovimientos/
        [HttpGet("tax/", Name = "scritax")]
        //public async Task<IEnumerable<ScriTaxId>> GetTax()
        //public async Task<ActionResult<ScriTaxId>> GetTax()
        public async Task<List<ScriTaxId>> GetTax()
        {
            //var scriTaxId = new ScriTaxId();
            //scriTaxId =  _context.ScriTaxId.FromSql("SELECT Id, TaxId from ScriTaxId order by Id");
            var scriTaxId = _context.ScriTaxId.FromSql("SELECT Id, TaxId from ScriTaxId order by Id").ToListAsync();
            //var scriTaxId = await _context.ScriTaxId.AllAsync(); //.FirstOrDefault();
            return await scriTaxId;
            //return _mapper.Map<List<ScriTaxId>>(scriTaxId);
        }

        // GET: api/ScriMovimientos/
        [HttpGet("codigoproductor/", Name = "scricodigoproductor")]
        public async Task<IEnumerable<ScriCodigoProductor>> GetCodigoProductor()
        {
            return _context.ScriCodigoProductor.FromSql("SELECT id, CodigoProductor from scri_codigo_productor order by id");
        }

        // GET: api/ScriMovimientos
        [HttpGet, DisableRequestSizeLimit]
        public async Task<IActionResult> PostScriMovimientos(string TaxID, string StartDate, string EndDate, string ProducerCode)
        {

            var url = Configuration["sancristobal:url"];
            var user = Configuration["sancristobal:user"];
            var pass = Configuration["sancristobal:pass"];
            ScriMovimientosOut scriMovimientosOut = new ScriMovimientosOut();

            var error = "";


            var parametros = new
            {
               TaxID = TaxID, 
               StartDate = StartDate, 
               EndDate = EndDate, 
               ProducerCode = ProducerCode
            };

            // Serialize our concrete class into a JSON String
            var stringParametros = await Task.Run(() => JsonConvert.SerializeObject(parametros));

            HttpContent httpContent = new StringContent(stringParametros, Encoding.UTF8, "application/json");

            try { 

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("USERNAME", user);
                httpClient.DefaultRequestHeaders.Add("PASS", pass);

                // Do the actual request and await the response
                var httpResponse = await httpClient.PostAsync(url, httpContent);

                if (httpResponse.Content != null)
                {
                    var responseContent = await httpResponse.Content.ReadAsStringAsync();

                    scriMovimientosOut = JsonConvert.DeserializeObject<ScriMovimientosOut>(responseContent);

                    ScriMovimientos scriMovimientos = new ScriMovimientos();
                    Entities.ScriMessages scriMessages = new Entities.ScriMessages();
                    List<Entities.ScriMessages> cscriMessages = new List<Entities.ScriMessages>();
                    Entities.ScriListJobSummary scriListJobSummary = new Entities.ScriListJobSummary();
                    List<Entities.ScriListJobSummary> cscriListJobSummary = new List<Entities.ScriListJobSummary>();
                    Entities.ScriOffering scriOffering = new Entities.ScriOffering();
                    List<Entities.ScriOffering> cscriOffering = new List<Entities.ScriOffering>();
                    Entities.ScriPolicyType scriPolicyType = new Entities.ScriPolicyType();
                    List<Entities.ScriPolicyType> cscriPolicyType = new List<Entities.ScriPolicyType>();
                    Entities.ScriProduct scriProduct = new Entities.ScriProduct();
                    List<Entities.ScriProduct> cscriProduct = new List<Entities.ScriProduct>();

                    int i = 0;
                    int aux = 0;
                    scriMovimientos.HasError = scriMovimientosOut.HasError;
                    scriMovimientos.HasInformation = scriMovimientosOut.HasInformation;
                    scriMovimientos.HasWarning = scriMovimientosOut.HasWarning;

                    while (i < scriMovimientosOut.Messages.Count())
                    {
                        scriMessages = new Entities.ScriMessages();
                        scriMessages.NombreServicio = scriMovimientosOut.Messages[i].NombreServicio;
                        scriMessages.VersionServicio = scriMovimientosOut.Messages[i].VersionServicio;
                        scriMessages.Description = scriMovimientosOut.Messages[i].Description;
                        scriMessages.MessageBeautiful = scriMovimientosOut.Messages[i].MessageBeautiful;
                        scriMessages.StackTrace = scriMovimientosOut.Messages[i].StackTrace;
                        scriMessages.ErrorLevel  = scriMovimientosOut.Messages[i].ErrorLevel;

                        cscriMessages.Add(scriMessages);
                        i = i + 1;
                    }
                    scriMovimientos.ScriMessages = cscriMessages;

                    i = 0;
                    while (i < scriMovimientosOut.ListJobSummary.Count())
                    {
                        scriListJobSummary = new Entities.ScriListJobSummary();
                        scriListJobSummary.OfferingPlan = scriMovimientosOut.ListJobSummary[i].OfferingPlan;
                        scriListJobSummary.PolicyPeriodID = scriMovimientosOut.ListJobSummary[i].PolicyPeriodID;
                        scriListJobSummary.ScopeCoverage = scriMovimientosOut.ListJobSummary[i].ScopeCoverage;
                        scriListJobSummary.StartDate = scriMovimientosOut.ListJobSummary[i].StartDate;
                        scriListJobSummary.Status = scriMovimientosOut.ListJobSummary[i].Status;
                        scriListJobSummary.TransactionJob = scriMovimientosOut.ListJobSummary[i].TransactionJob;
                        scriListJobSummary.Subtype = scriMovimientosOut.ListJobSummary[i].Subtype;
                        scriListJobSummary.EffectiveDate = scriMovimientosOut.ListJobSummary[i].EffectiveDate;
                        scriListJobSummary.PeriodEnd = scriMovimientosOut.ListJobSummary[i].PeriodEnd;
                        scriListJobSummary.PolicyStartDate = scriMovimientosOut.ListJobSummary[i].PolicyStartDate;
                        scriListJobSummary.PolicyNumber = scriMovimientosOut.ListJobSummary[i].PolicyNumber;

//                        aux = 0;

                        scriOffering = new Entities.ScriOffering();
                        cscriOffering = new List<Entities.ScriOffering>();
                        scriOffering.Code = scriMovimientosOut.ListJobSummary[i].Offering.Code;
                        scriOffering.Description = scriMovimientosOut.ListJobSummary[i].Offering.Description;
                        scriPolicyType = new Entities.ScriPolicyType();
                        cscriPolicyType = new List<Entities.ScriPolicyType>();
                        scriPolicyType.Code = scriMovimientosOut.ListJobSummary[i].PolicyType.Code;
                        scriPolicyType.Description = scriMovimientosOut.ListJobSummary[i].PolicyType.Description;
                        scriProduct = new Entities.ScriProduct();
                        cscriProduct = new List<Entities.ScriProduct>();
                        scriProduct.Code = scriMovimientosOut.ListJobSummary[i].Product.Code;
                        scriProduct.Description = scriMovimientosOut.ListJobSummary[i].Product.Description;
                        cscriOffering.Add(scriOffering);
                        scriListJobSummary.ScriOffering = cscriOffering;
                        cscriPolicyType.Add(scriPolicyType);
                        scriListJobSummary.ScriPolicyType = cscriPolicyType;
                        cscriProduct.Add(scriProduct);
                        scriListJobSummary.ScriProduct = cscriProduct;

                        i = i + 1;
                        cscriListJobSummary.Add(scriListJobSummary);
                    }
                    
                    scriMovimientos.ScriListJobSummary = cscriListJobSummary;
                    _context.ScriMovimientos.Add(scriMovimientos);
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
                return StatusCode(205, error.ToString());
            }
            else
            {
                return StatusCode(200, "OK");
            }

        } //Metodo

    }
}
