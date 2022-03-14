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
using System.Xml;
using System.Xml.XPath;

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
        public async Task<IEnumerable<ScriPolicy>> GetScriMovimientos()
        {
            //return _context.ScriListJobSummary.FromSql("SELECT top 1 * from scri_list_job_summary order by StartDate desc");

            return _context.ScriPolicy.FromSql("select top 1 * from scri_policy order by JobDate desc");
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
                        if (scriMovimientosOut.ListJobSummary[i].StartDate !=  null && scriMovimientosOut.ListJobSummary[i].StartDate.ToString() != "1/1/0001 00:00:00" && scriMovimientosOut.ListJobSummary[i].StartDate.ToString() != "01/01/0001 0:00:00") {
                            scriListJobSummary.StartDate = scriMovimientosOut.ListJobSummary[i].StartDate;
                        } else
                        {
                            scriListJobSummary.StartDate = DateTime.Parse("01/01/1900");
                        }
                        scriListJobSummary.Status = scriMovimientosOut.ListJobSummary[i].Status;
                        scriListJobSummary.TransactionJob = scriMovimientosOut.ListJobSummary[i].TransactionJob;
                        scriListJobSummary.Subtype = scriMovimientosOut.ListJobSummary[i].Subtype;
                        if (scriMovimientosOut.ListJobSummary[i].EffectiveDate != null && scriMovimientosOut.ListJobSummary[i].EffectiveDate.ToString() != "1/1/0001 00:00:00" && scriMovimientosOut.ListJobSummary[i].EffectiveDate.ToString() != "1/01/0001 0:00:00")
                        {
                            scriListJobSummary.EffectiveDate = scriMovimientosOut.ListJobSummary[i].EffectiveDate;
                        } else
                        {
                            scriListJobSummary.EffectiveDate = DateTime.Parse("01/01/1900");
                        }
                        if (scriMovimientosOut.ListJobSummary[i].PeriodEnd != null && scriMovimientosOut.ListJobSummary[i].PeriodEnd.ToString() != "1/1/0001 00:00:00" && scriMovimientosOut.ListJobSummary[i].PeriodEnd.ToString() != "01/01/0001 0:00:00")
                        {
                            scriListJobSummary.PeriodEnd = scriMovimientosOut.ListJobSummary[i].PeriodEnd;
                        } else
                        {
                            scriListJobSummary.PeriodEnd = DateTime.Parse("01/01/1900");
                        }
                        if (scriMovimientosOut.ListJobSummary[i].PolicyStartDate != null && scriMovimientosOut.ListJobSummary[i].PolicyStartDate.ToString() != "1/1/0001 00:00:00" && scriMovimientosOut.ListJobSummary[i].PolicyStartDate.ToString() != "01/01/0001 0:00:00")
                        {
                            scriListJobSummary.PolicyStartDate = scriMovimientosOut.ListJobSummary[i].PolicyStartDate;
                        } else
                        {
                            scriListJobSummary.PolicyStartDate = DateTime.Parse("01/01/1900");
                        }
                        scriListJobSummary.PolicyNumber = scriMovimientosOut.ListJobSummary[i].PolicyNumber;

//                        aux = 0;

                        if (scriMovimientosOut.ListJobSummary[i].Offering != null) { 
                            scriOffering = new Entities.ScriOffering();
                            cscriOffering = new List<Entities.ScriOffering>();
                            scriOffering.Code = scriMovimientosOut.ListJobSummary[i].Offering.Code;
                            scriOffering.Description = scriMovimientosOut.ListJobSummary[i].Offering.Description;
                        }
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

                            /*Recorrer el listado de polizas y llamar a detalle para procesarlas*/
                            i = 0;
                            while (i < cscriListJobSummary.Count())
                            {
                                /*Por cada poliza se llama al detalle*/
                                var resp = await procesarDetalle(cscriListJobSummary[i].PolicyNumber);

                                if (resp != "200") { error = resp; }
                                i = i + 1;
                            }

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


        public async Task<String>  procesarDetalle(string PolicyNumber) /*Task<IActionResult>*/
        {
            var url = Configuration["sancristobal:urldet"];
            var user = Configuration["sancristobal:user"];
            var pass = Configuration["sancristobal:pass"];

            string respuesta = "";

            string parametro = "?PolicyNumber="+ PolicyNumber;

            ScriPolizab2b scriPolizab2b = new ScriPolizab2b();
            ScriPolicy scriPolicy = new ScriPolicy();
            ScriAffinityGroupSub scriAffinityGroupSub = new ScriAffinityGroupSub();
            ScriAffinityGroupType scriAffinityGroupType = new ScriAffinityGroupType();
            ScriContact scriContact = new ScriContact();
            ScriSubtype scriSubtype = new ScriSubtype();
            ScriAddress scriAddress = new ScriAddress();
            ScriAddressType scriAddressType = new ScriAddressType();
            ScriAddressCountry scriAddressCountry = new ScriAddressCountry();
            ScriState scriState = new ScriState();
            ScriPhone scriPhone = new ScriPhone();
            ScriPhoneType scriPhoneType = new ScriPhoneType();
            ScriContactType scriContactType = new ScriContactType();
            ScriGender scriGender = new ScriGender();
            ScriMaritalStatus scriMaritalStatus = new ScriMaritalStatus();
            ScriNationality scriNationality = new ScriNationality();
            ScriOccupation scriOccupation = new ScriOccupation();
            ScriOfficialIDType scriOfficialIDType = new ScriOfficialIDType();
            ScriPreferredSettlementCurrency scriPreferredSettlementCurrency = new ScriPreferredSettlementCurrency();
            ScriSchoolLevel scriSchoolLevel = new ScriSchoolLevel();
            ScriCountry scriCountry = new ScriCountry();
            ScriMaillingAddress scriMaillingAddress = new ScriMaillingAddress();
            ScriTaxStatus scriTaxStatus = new ScriTaxStatus();
            ScriEnrollementStatus scriEnrollementStatus = new ScriEnrollementStatus();
            ScriRetentionAgent scriRetentionAgent = new ScriRetentionAgent();
            ScriMaillingAddressAddressType scriMaillingAddressAddressType = new ScriMaillingAddressAddressType();
            ScriMaillingAddressCountry scriMaillingAddressCountry = new ScriMaillingAddressCountry();
            ScriMaillingAddressState scriMaillingAddressState = new ScriMaillingAddressState();
            ScriExt_RamoSSN scriExt_RamoSSN = new ScriExt_RamoSSN();
            ScriExt_PolicyType scriExt_PolicyType = new ScriExt_PolicyType();
            ScriProducerCode scriProducerCode = new ScriProducerCode();
            ScriPaymentMethod scriPaymentMethod = new ScriPaymentMethod();
            ScriPolicyTerm scriPolicyTerm = new ScriPolicyTerm();
            ScriCurrency scriCurrency = new ScriCurrency();
            ScriProducerAgent scriProducerAgent = new ScriProducerAgent();
            ScriProducerOfService scriProducerOfService = new ScriProducerOfService();
            ScriServiceOrganizer scriServiceOrganizer = new ScriServiceOrganizer();
            ScriChannelEntry scriChannelEntry = new ScriChannelEntry();
            ScriStatus scriStatus = new ScriStatus();
            ScriVehicle scriVehicle = new ScriVehicle();
            ScriAutomaticAdjust scriAutomaticAdjust = new ScriAutomaticAdjust();
            ScriCategory scriCategory = new ScriCategory();
            ScriColor scriColor = new ScriColor();
            ScriFuelType scriFuelType = new ScriFuelType();
            ScriJurisdiction scriJurisdiction = new ScriJurisdiction();
            ScriOriginCountry scriOriginCountry = new ScriOriginCountry();
            ScriProductOffering scriProductOffering = new ScriProductOffering();
            ScriRiskLocation scriRiskLocation = new ScriRiskLocation();
            ScriUsage scriUsage = new ScriUsage();
            ScriReasonCancelDTO scriReasonCancelDTO = new ScriReasonCancelDTO();
            XmlNodeList XmlNodeList;
            XmlNodeList XmlPolicy;
            XmlNodeList XmlAffinityGroupSub;
            XmlNodeList XmlAffinityGroupType;
            XmlNodeList XmlSubtype;
            XmlNodeList XmlContact;
            XmlNodeList XmlAddress;
            XmlNodeList XmlAddressType;
            XmlNodeList XmlAddressCountry;
            XmlNodeList XmlState;
            XmlNodeList XmlPhone;
            XmlNodeList XmlPhoneType;
            XmlNodeList XmlContactType;
            XmlNodeList XmlGender;
            XmlNodeList XmlMaritalStatus;
            XmlNodeList XmlNationality;
            XmlNodeList XmlOccupation;
            XmlNodeList XmlOfficialIDType;
            XmlNodeList XmlPreferredSettlementCurrency;
            XmlNodeList XmlSchoolLevel;
            XmlNodeList XmlCountry;
            XmlNodeList XmlMaillingAddress;
            XmlNodeList XmlTaxStatus;
            XmlNodeList XmlEnrollementStatus;
            XmlNodeList XmlRetentionAgent;
            XmlNodeList XmlMaillingAddressAddressType;
            XmlNodeList XmlMaillingAddressCountry;
            XmlNodeList XmlMaillingAddressState;
            XmlNodeList XmlExt_RamoSSN;
            XmlNodeList XmlExt_PolicyType;
            XmlNodeList XmlProducerCode;
            XmlNodeList XmlPaymentMethod;
            XmlNodeList XmlPolicyTerm;
            XmlNodeList XmlCurrency;
            XmlNodeList XmlProducerAgent;
            XmlNodeList XmlProducerOfService;
            XmlNodeList XmlServiceOrganizer;
            XmlNodeList XmlChannelEntry;
            XmlNodeList XmlStatus;
            XmlNodeList XmlVehicle;
            XmlNodeList XmlAutomaticAdjust;
            XmlNodeList XmlCategory;
            XmlNodeList XmlColor;
            XmlNodeList XmlFuelType;
            XmlNodeList XmlJurisdiction;
            XmlNodeList XmlOriginCountry;
            XmlNodeList XmlProductOffering;
            XmlNodeList XmlRiskLocation;
            XmlNodeList XmlUsage;
            XmlNodeList XmlReasonCancelDTO;
            

            string aux = "";
            DateTime auxFecha = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue;

            try
            {

                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("USERNAME", user);
                    httpClient.DefaultRequestHeaders.Add("PASS", pass);
                    //Sacar de aca, es solo una prueba
                    //var httpResponse2 = await httpClient.GetAsync("https://ws-externos.sancristobal.com.ar/ServiciosGateway/B2BGateway/api/searchPolicyDetails/SearchPolicyDetails?PolicyNumber=01-05-01-30179007");

                    var httpResponse2 = await httpClient.GetAsync(url + parametro);

                    var responseContent2 = await httpResponse2.Content.ReadAsStringAsync();
                    //Parsear el xml desde el texto
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(responseContent2);

                    //scriPolizab2b.AlternativaComercial = doc.GetElementsByTagName("AlternativaComercial");


                    /***************/
                    /*scriPolizab2b*/
                    /***************/

                    /*AlternativaComercial*/
                    XmlNodeList = doc.GetElementsByTagName("AlternativaComercial");
                    if (XmlNodeList.Count != 0)
                    {
                        aux = XmlNodeList[0].InnerText;
                        if (aux != "") { scriPolizab2b.AlternativaComercial = aux; } else { scriPolizab2b.AlternativaComercial = ""; }
                    }

                    /*Updated*/
                    XmlNodeList = doc.GetElementsByTagName("Updated");
                    if (XmlNodeList.Count != 0)
                    {
                        aux = XmlNodeList[0].InnerText;
                        if (aux != "") { scriPolizab2b.Updated = aux; } else { scriPolizab2b.Updated = ""; }
                    }

                    /*HasError*/
                    XmlNodeList = doc.GetElementsByTagName("HasError");
                    if (XmlNodeList.Count != 0)
                    {
                        aux = XmlNodeList[0].InnerText;
                        if (aux != "") { scriPolizab2b.HasError = aux; } else { scriPolizab2b.HasError = ""; }
                    }

                    /*HasWarning*/
                    XmlNodeList = doc.GetElementsByTagName("HasWarning");
                    if (XmlNodeList.Count != 0)
                    {
                        aux = XmlNodeList[0].InnerText;
                        if (aux != "") { scriPolizab2b.HasWarning = aux; } else { scriPolizab2b.HasWarning = ""; }
                    }

                    /*HasInformation*/
                    XmlNodeList = doc.GetElementsByTagName("HasInformation");
                    if (XmlNodeList.Count != 0)
                    {
                        aux = XmlNodeList[0].InnerText;
                        if (aux != "") { scriPolizab2b.HasInformation = aux; } else { scriPolizab2b.HasInformation = ""; }
                    }

                    /*Messages*/
                    XmlNodeList = doc.GetElementsByTagName("Messages");
                    if (XmlNodeList.Count != 0)
                    {
                        aux = XmlNodeList[0].InnerText;
                        if (aux != "") { scriPolizab2b.Messages = aux; } else { scriPolizab2b.Messages = ""; }
                    }


                    try
                    {
                        _context.ScriPolizab2b.Add(scriPolizab2b);
                        await _context.SaveChangesAsync();
                    } catch (Exception e)
                    {
                        respuesta = "208";
                        return respuesta;//StatusCode(208, e.Message);
                    }


                    /***************/
                    /*scriPolicy*/
                    /***************/
                    XmlPolicy = doc.GetElementsByTagName("Policy");

                    foreach (XmlElement nodo in XmlPolicy)
                    {

                        scriPolicy.idPolizaB2B = scriPolizab2b.Id;
                        /*JobDate DATETIME*/
                        XmlNodeList = nodo.GetElementsByTagName("JobDate");
                        if (XmlNodeList.Count != 0)
                        {
                            try
                            {
                                auxFecha = DateTime.Parse(XmlNodeList[0].InnerText);
                                if (auxFecha == DateTime.Parse("01/01/0001"))
                                {
                                    auxFecha = DateTime.Parse("01/01/1900");
                                }

                                scriPolicy.JobDate = auxFecha;
                            }
                            catch
                            {
                                scriPolicy.JobDate = DateTime.Parse("01/01/1900");
                            }
                        }
                        else { scriPolicy.JobDate = DateTime.Parse("01/01/1900"); }

                        /*MaxAgeEntry    INT*/
                        XmlNodeList = nodo.GetElementsByTagName("MaxAgeEntry");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriPolicy.MaxAgeEntry = Int32.Parse(aux); } else { scriPolicy.MaxAgeEntry = 0; }
                        }

                        /*MaxAgeStayAdditional INT*/
                        XmlNodeList = nodo.GetElementsByTagName("MaxAgeStayAdditional");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriPolicy.MaxAgeStayAdditional = Int32.Parse(aux); } else { scriPolicy.MaxAgeStayAdditional = 0; }
                        }

                        /*MaxAgeStayBasic    INT*/
                        XmlNodeList = nodo.GetElementsByTagName("MaxAgeStayBasic");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriPolicy.MaxAgeStayBasic = Int32.Parse(aux); } else { scriPolicy.MaxAgeStayBasic = 0; }
                        }

                        /*MaximaCompensation DECIMAL(12,2)*/
                        XmlNodeList = nodo.GetElementsByTagName("MaximaCompensation");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriPolicy.MaximaCompensation = Decimal.Parse(aux.Replace(".", ",")); } else { scriPolicy.MaximaCompensation = 0; }
                        }

                        /*FirstPolicyDate DATETIME*/
                        XmlNodeList = nodo.GetElementsByTagName("FirstPolicyDate");
                        if (XmlNodeList.Count != 0)
                        {
                            try
                            {
                                auxFecha = DateTime.Parse(XmlNodeList[0].InnerText);
                                if (auxFecha == DateTime.Parse("01/01/0001"))
                                {
                                    auxFecha = DateTime.Parse("01/01/1900");
                                }

                                scriPolicy.FirstPolicyDate = auxFecha;
                            }
                            catch
                            {
                                scriPolicy.FirstPolicyDate = DateTime.Parse("01/01/1900");
                            }
                        }
                        else { scriPolicy.FirstPolicyDate = DateTime.Parse("01/01/1900"); }

                        /*RamoDescripcion    VARCHAR(255)*/
                        XmlNodeList = nodo.GetElementsByTagName("RamoDescripcion");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriPolicy.RamoDescripcion = aux; } else { scriPolicy.RamoDescripcion = ""; }
                        }

                        /*AccountNumber VARCHAR(255)*/
                        XmlNodeList = nodo.GetElementsByTagName("AccountNumber");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriPolicy.AccountNumber = aux; } else { scriPolicy.AccountNumber = ""; }
                        }
                        /*MinAgeEntry INT*/
                        XmlNodeList = nodo.GetElementsByTagName("MinAgeEntry");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriPolicy.MinAgeEntry = Int32.Parse(aux); } else { scriPolicy.MinAgeEntry = 0; }
                        }
                        /*PaymentFees    DECIMAL(12, 2)*/
                        XmlNodeList = nodo.GetElementsByTagName("PaymentFees");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriPolicy.PaymentFees = Decimal.Parse(aux.Replace(".", ",")); } else { scriPolicy.PaymentFees = 0; }
                        }
                        /*PeriodEnd DATETIME*/
                        XmlNodeList = nodo.GetElementsByTagName("PeriodEnd");
                        if (XmlNodeList.Count != 0)
                        {
                            try
                            {
                                auxFecha = DateTime.Parse(XmlNodeList[0].InnerText);
                                if (auxFecha == DateTime.Parse("01/01/0001"))
                                {
                                    auxFecha = DateTime.Parse("01/01/1900");
                                }

                                scriPolicy.PeriodEnd = auxFecha;
                            }
                            catch
                            {
                                scriPolicy.PeriodEnd = DateTime.Parse("01/01/1900");
                            }
                        }
                        else { scriPolicy.PeriodEnd = DateTime.Parse("01/01/1900"); }
                        /*PeriodStart    DATETIME*/
                        XmlNodeList = nodo.GetElementsByTagName("PeriodStart");
                        if (XmlNodeList.Count != 0)
                        {
                            try
                            {
                                auxFecha = DateTime.Parse(XmlNodeList[0].InnerText);
                                if (auxFecha == DateTime.Parse("01/01/0001"))
                                {
                                    auxFecha = DateTime.Parse("01/01/1900");
                                }

                                scriPolicy.PeriodStart = auxFecha;
                            }
                            catch
                            {
                                scriPolicy.PeriodStart = DateTime.Parse("01/01/1900");
                            }
                        }
                        else { scriPolicy.PeriodStart = DateTime.Parse("01/01/1900"); }
                        /*PolicyPeriodID VARCHAR(255)*/
                        XmlNodeList = nodo.GetElementsByTagName("PolicyPeriodID");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriPolicy.PolicyPeriodID = aux; } else { scriPolicy.PolicyPeriodID = ""; }
                        }
                        /*TripEndDate DATETIME*/
                        XmlNodeList = nodo.GetElementsByTagName("TripEndDate");
                        if (XmlNodeList.Count != 0)
                        {
                            try
                            {
                                auxFecha = DateTime.Parse(XmlNodeList[0].InnerText);
                                if (auxFecha == DateTime.Parse("01/01/0001"))
                                {
                                    auxFecha = DateTime.Parse("01/01/1900");
                                }

                                scriPolicy.TripEndDate = auxFecha;
                            }
                            catch
                            {
                                scriPolicy.TripEndDate = DateTime.Parse("01/01/1900");
                            }
                        }
                        else { scriPolicy.TripEndDate = DateTime.Parse("01/01/1900"); }
                        /*TripStarDate   DATETIME*/
                        XmlNodeList = nodo.GetElementsByTagName("TripStarDate");
                        if (XmlNodeList.Count != 0)
                        {
                            try
                            {
                                auxFecha = DateTime.Parse(XmlNodeList[0].InnerText);
                                if (auxFecha == DateTime.Parse("01/01/0001"))
                                {
                                    auxFecha = DateTime.Parse("01/01/1900");
                                }

                                scriPolicy.TripStarDate = auxFecha;
                            }
                            catch
                            {
                                scriPolicy.TripStarDate = DateTime.Parse("01/01/1900");
                            }
                        }
                        else { scriPolicy.TripStarDate = DateTime.Parse("01/01/1900"); }
                        /*FacultativePolicy VARCHAR(255)*/
                        XmlNodeList = nodo.GetElementsByTagName("FacultativePolicy");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriPolicy.FacultativePolicy = aux; } else { scriPolicy.FacultativePolicy = ""; }
                        }
                        /*ProvisionalGuard VARCHAR(255)*/
                        XmlNodeList = nodo.GetElementsByTagName("ProvisionalGuard");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriPolicy.ProvisionalGuard = aux; } else { scriPolicy.ProvisionalGuard = ""; }
                        }
                        /*JobNumber VARCHAR(255)*/
                        XmlNodeList = nodo.GetElementsByTagName("JobNumber");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriPolicy.JobNumber = aux; } else { scriPolicy.JobNumber = ""; }
                        }
                        /*BranchNumber*/
                        XmlNodeList = nodo.GetElementsByTagName("BranchNumber");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriPolicy.BranchNumber = Int32.Parse(aux); } else { scriPolicy.BranchNumber = 0; }
                        }
                        /*RenewTo    VARCHAR(255)*/
                        XmlNodeList = nodo.GetElementsByTagName("RenewTo");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriPolicy.RenewTo = aux; } else { scriPolicy.RenewTo = ""; }
                        }

                    }

                   // _context.ScriPolizab2b.Add(scriPolizab2b);
                    _context.ScriPolicy.Add(scriPolicy);

                    try
                    {
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception e)
                    {
                        respuesta = "206";
                        return respuesta;//StatusCode(208, e.Message);
                    }

                    /***************/
                    /*AffinityGroupSub*/
                    /***************/
                    XmlAffinityGroupSub = doc.GetElementsByTagName("AffinityGroupSub");

                    foreach (XmlElement nodo in XmlAffinityGroupSub)
                    {
                        scriAffinityGroupSub.idPolicy = scriPolicy.Id;

                        /*,DisplayName VARCHAR(255)*/
                        XmlNodeList = nodo.GetElementsByTagName("DisplayName");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriAffinityGroupSub.DisplayName = aux; } else { scriAffinityGroupSub.DisplayName = ""; }
                        }
                        /*,EndDate DATETIME*/
                        XmlNodeList = nodo.GetElementsByTagName("EndDate");
                        if (XmlNodeList.Count != 0)
                        {
                            try
                            {
                                auxFecha = DateTime.Parse(XmlNodeList[0].InnerText);
                                if (auxFecha == DateTime.Parse("01/01/0001"))
                                {
                                    auxFecha = DateTime.Parse("01/01/1900");
                                }

                                scriAffinityGroupSub.EndDate = auxFecha;
                            }
                            catch
                            {
                                scriAffinityGroupSub.EndDate = DateTime.Parse("01/01/1900");
                            }
                        }
                        else { scriAffinityGroupSub.EndDate = DateTime.Parse("01/01/1900"); }
                        /*, PublicID   VARCHAR(255)*/
                        XmlNodeList = nodo.GetElementsByTagName("PublicID");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriAffinityGroupSub.PublicID = aux; } else { scriAffinityGroupSub.PublicID = ""; }
                        }
                        /*,StartDate DATETIME*/
                        XmlNodeList = nodo.GetElementsByTagName("StartDate");
                        if (XmlNodeList.Count != 0)
                        {
                            try
                            {
                                auxFecha = DateTime.Parse(XmlNodeList[0].InnerText);
                                if (auxFecha == DateTime.Parse("01/01/0001"))
                                {
                                    auxFecha = DateTime.Parse("01/01/1900");
                                }

                                scriAffinityGroupSub.StartDate = auxFecha;
                            }
                            catch
                            {
                                scriAffinityGroupSub.StartDate = DateTime.Parse("01/01/1900");
                            }
                        }
                        else { scriAffinityGroupSub.StartDate = DateTime.Parse("01/01/1900"); }

                        /***************/
                        /*AffinityGroupType*/
                        /***************/
                        XmlAffinityGroupType = doc.GetElementsByTagName("AffinityGroupType");
                        foreach (XmlElement nodoType in XmlAffinityGroupType)
                        {
                            /*,idAffinityGroupSub INT*/
                            /*, Code   VARCHAR(255)*/
                            XmlNodeList = nodo.GetElementsByTagName("Code");
                            if (XmlNodeList.Count != 0)
                            {
                                aux = XmlNodeList[0].InnerText;
                                if (aux != "") { scriAffinityGroupType.Code = aux; } else { scriAffinityGroupType.Code = ""; }
                            }
                            /*,"Description"  VARCHAR(255)*/
                            XmlNodeList = nodo.GetElementsByTagName("Description");
                            if (XmlNodeList.Count != 0)
                            {
                                aux = XmlNodeList[0].InnerText;
                                if (aux != "") { scriAffinityGroupType.Description = aux; } else { scriAffinityGroupType.Description = ""; }
                            }

                        }


                    }//foreach (XmlElement nodo in XmlAffinityGroupSub)
                   

                    try
                    {
                        if (scriAffinityGroupSub.idPolicy != 0) { 
                            _context.ScriAffinityGroupSub.Add(scriAffinityGroupSub);
                            await _context.SaveChangesAsync();
                            scriAffinityGroupType.idAffinityGroupSub = scriAffinityGroupSub.Id;
                            _context.ScriAffinityGroupType.Add(scriAffinityGroupType);
                            await _context.SaveChangesAsync();
                        }
                    }
                    catch (Exception e)
                    {

                        respuesta = "208";
                        return respuesta;//StatusCode(208, e.Message);
                    }


                    /***************/
                    /*Subtype*/
                    /***************/
                    XmlSubtype = doc.GetElementsByTagName("Subtype");
                    foreach (XmlElement nodo in XmlSubtype)
                    {
                        scriSubtype.idPolicy = scriPolicy.Id;

                        /*,Code VARCHAR(255)*/
                        XmlNodeList = nodo.GetElementsByTagName("Code");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriSubtype.Code = aux; } else { scriSubtype.Code = ""; }
                        }
                        /*,Description VARCHAR(255)*/
                        XmlNodeList = nodo.GetElementsByTagName("Description");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriSubtype.Description = aux; } else { scriSubtype.Description = ""; }
                        }
                    }
                    _context.ScriSubtype.Add(scriSubtype);

                    try
                    {
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception e)
                    {
                        respuesta = "209";
                        return respuesta;//StatusCode(208, e.Message);
                    }

                    /***************/
                    /*Contact*/
                    /***************/
                    XmlContact = doc.GetElementsByTagName("Contact");
                    foreach (XmlElement nodo in XmlContact)
                    {
                        scriContact.idPolicy = scriPolicy.Id;

                        /*,Activitystartdate DATETIME*/
                        XmlNodeList = nodo.GetElementsByTagName("Activitystartdate");
                        if (XmlNodeList.Count != 0)
                        {
                            try
                            {
                                auxFecha = DateTime.Parse(XmlNodeList[0].InnerText);
                                if (auxFecha == DateTime.Parse("01/01/0001"))
                                {
                                    auxFecha = DateTime.Parse("01/01/1900");
                                }

                                scriContact.Activitystartdate = auxFecha;
                            }
                            catch
                            {
                                scriContact.Activitystartdate = DateTime.Parse("01/01/1900");
                            }
                        }
                        else { scriContact.Activitystartdate = DateTime.Parse("01/01/1900"); }
                        /*, CUIL   VARCHAR(255)*/
                        XmlNodeList = nodo.GetElementsByTagName("CUIL");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriContact.CUIL = aux; } else { scriContact.CUIL = ""; }
                        }
                        /*,DateOfBirth DATETIME*/
                        XmlNodeList = nodo.GetElementsByTagName("DateOfBirth");
                        if (XmlNodeList.Count != 0)
                        {
                            try
                            {
                                auxFecha = DateTime.Parse(XmlNodeList[0].InnerText);
                                if (auxFecha == DateTime.Parse("01/01/0001"))
                                {
                                    auxFecha = DateTime.Parse("01/01/1900");
                                }

                                scriContact.DateOfBirth = auxFecha;
                            }
                            catch
                            {
                                scriContact.DateOfBirth = DateTime.Parse("01/01/1900");
                            }
                        }
                        else { scriContact.DateOfBirth = DateTime.Parse("01/01/1900"); }
                        /*, EmailAddress1  VARCHAR(255)*/
                        XmlNodeList = nodo.GetElementsByTagName("EmailAddress1");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriContact.EmailAddress1 = aux; } else { scriContact.EmailAddress1 = ""; }
                        }
                        /*,FirstName VARCHAR(255)*/
                        XmlNodeList = nodo.GetElementsByTagName("FirstName");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriContact.FirstName = aux; } else { scriContact.FirstName = ""; }
                        }
                        /*,InsuredNumberFormated VARCHAR(255)*/
                        XmlNodeList = nodo.GetElementsByTagName("InsuredNumberFormated");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriContact.InsuredNumberFormated = aux; } else { scriContact.InsuredNumberFormated = ""; }
                        }
                        /*,LastName VARCHAR(255)*/
                        XmlNodeList = nodo.GetElementsByTagName("LastName");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriContact.LastName = aux; } else { scriContact.LastName = ""; }
                        }
                        /*,Name VARCHAR(255)*/
                        XmlNodeList = nodo.GetElementsByTagName("Name");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriContact.Name = aux; } else { scriContact.Name = ""; }
                        }
                        /*,PEP VARCHAR(255)*/
                        XmlNodeList = nodo.GetElementsByTagName("PEP");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriContact.PEP = aux; } else { scriContact.PEP = ""; }
                        }
                        /*,PrimaryNamedInsured VARCHAR(255)*/
                        XmlNodeList = nodo.GetElementsByTagName("PrimaryNamedInsured");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriContact.PrimaryNamedInsured = aux; } else { scriContact.PrimaryNamedInsured = ""; }
                        }
                        /*,PublicID VARCHAR(255)*/
                        XmlNodeList = nodo.GetElementsByTagName("PublicID");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriContact.PublicID = aux; } else { scriContact.PublicID = ""; }
                        }
                        /*,Resident VARCHAR(255)*/
                        XmlNodeList = nodo.GetElementsByTagName("Resident");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriContact.Resident = aux; } else { scriContact.Resident = ""; }
                        }
                        /*,TaxID VARCHAR(255)*/
                        XmlNodeList = nodo.GetElementsByTagName("TaxID");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriContact.TaxID = aux; } else { scriContact.TaxID = ""; }
                        }
                        /*,UIFFormSubmitted VARCHAR(255)*/
                        XmlNodeList = nodo.GetElementsByTagName("UIFFormSubmitted");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriContact.UIFFormSubmitted = aux; } else { scriContact.UIFFormSubmitted = ""; }
                        }


                        /***************/
                        /*Address*/
                        /***************/
                        XmlAddress = nodo.GetElementsByTagName("Address");
                        foreach (XmlElement nodoAddress in XmlAddress)
                        {


                            /*,updateLinkedAddresses VARCHAR(255)*/
                            XmlNodeList = nodoAddress.GetElementsByTagName("updateLinkedAddresses");
                            if (XmlNodeList.Count != 0)
                            {
                                aux = XmlNodeList[0].InnerText;
                                if (aux != "") { scriAddress.updateLinkedAddresses = aux; } else { scriAddress.updateLinkedAddresses = ""; }
                            }
                            /*,AddressLine1 VARCHAR(255)*/
                            XmlNodeList = nodoAddress.GetElementsByTagName("AddressLine1");
                            if (XmlNodeList.Count != 0)
                            {
                                aux = XmlNodeList[0].InnerText;
                                if (aux != "") { scriAddress.AddressLine1 = aux; } else { scriAddress.AddressLine1 = ""; }
                            }
                            /*,AddressLine2 VARCHAR(255)*/
                            XmlNodeList = nodoAddress.GetElementsByTagName("AddressLine2");
                            if (XmlNodeList.Count != 0)
                            {
                                aux = XmlNodeList[0].InnerText;
                                if (aux != "") { scriAddress.AddressLine2 = aux; } else { scriAddress.AddressLine2 = ""; }
                            }
                            /*,City VARCHAR(255)*/
                            XmlNodeList = nodoAddress.GetElementsByTagName("City");
                            if (XmlNodeList.Count != 0)
                            {
                                aux = XmlNodeList[0].InnerText;
                                if (aux != "") { scriAddress.City = aux; } else { scriAddress.City = ""; }
                            }
                            /*,County VARCHAR(255)*/
                            XmlNodeList = nodoAddress.GetElementsByTagName("County");
                            if (XmlNodeList.Count != 0)
                            {
                                aux = XmlNodeList[0].InnerText;
                                if (aux != "") { scriAddress.County = aux; } else { scriAddress.County = ""; }
                            }
                            /*,DisplayText VARCHAR(255)*/
                            XmlNodeList = nodoAddress.GetElementsByTagName("DisplayText");
                            if (XmlNodeList.Count != 0)
                            {
                                aux = XmlNodeList[0].InnerText;
                                if (aux != "") { scriAddress.DisplayText = aux; } else { scriAddress.DisplayText = ""; }
                            }
                            /*,PolicyAddress VARCHAR(255)*/
                            XmlNodeList = nodoAddress.GetElementsByTagName("PolicyAddress");
                            if (XmlNodeList.Count != 0)
                            {
                                aux = XmlNodeList[0].InnerText;
                                if (aux != "") { scriAddress.PolicyAddress = aux; } else { scriAddress.PolicyAddress = ""; }
                            }
                            /*,PostalCode VARCHAR(255)*/
                            XmlNodeList = nodoAddress.GetElementsByTagName("PostalCode");
                            if (XmlNodeList.Count != 0)
                            {
                                aux = XmlNodeList[0].InnerText;
                                if (aux != "") { scriAddress.PostalCode = aux; } else { scriAddress.PostalCode = ""; }
                            }
                            /*,PrimaryAddress VARCHAR(255)*/
                            XmlNodeList = nodoAddress.GetElementsByTagName("PrimaryAddress");
                            if (XmlNodeList.Count != 0)
                            {
                                aux = XmlNodeList[0].InnerText;
                                if (aux != "") { scriAddress.PrimaryAddress = aux; } else { scriAddress.PrimaryAddress = ""; }
                            }
                            /*,PublicID VARCHAR(255)*/
                            XmlNodeList = nodoAddress.GetElementsByTagName("PublicID");
                            if (XmlNodeList.Count != 0)
                            {
                                aux = XmlNodeList[0].InnerText;
                                if (aux != "") { scriAddress.PublicID = aux; } else { scriAddress.PublicID = ""; }
                            }
                            /*,StreetNumber VARCHAR(255)*/
                            XmlNodeList = nodoAddress.GetElementsByTagName("StreetNumber");
                            if (XmlNodeList.Count != 0)
                            {
                                aux = XmlNodeList[0].InnerText;
                                if (aux != "") { scriAddress.StreetNumber = aux; } else { scriAddress.StreetNumber = ""; }
                            }

                            /***************/
                            /*AddressType*/
                            /***************/
                            XmlAddressType = nodoAddress.GetElementsByTagName("AddressType");
                            foreach (XmlElement nodoAddressType in XmlAddress)
                            {

                                /*,Code VARCHAR(255)*/
                                XmlNodeList = nodoAddressType.GetElementsByTagName("Code");
                                if (XmlNodeList.Count != 0)
                                {
                                    aux = XmlNodeList[0].InnerText;
                                    if (aux != "") { scriAddressType.Code = aux; } else { scriAddressType.Code = ""; }
                                }
                                /*,"Description"  VARCHAR(255)*/
                                XmlNodeList = nodoAddressType.GetElementsByTagName("Description");
                                if (XmlNodeList.Count != 0)
                                {
                                    aux = XmlNodeList[0].InnerText;
                                    if (aux != "") { scriAddressType.Description = aux; } else { scriAddressType.Description = ""; }
                                }

                            } //AddressType

                            /***************/
                            /*AddressCountry*/
                            /***************/
                            XmlAddressCountry = nodoAddress.GetElementsByTagName("Country");
                            foreach (XmlElement nodoAddressCountry in XmlAddressCountry)
                            {

                                /*,Code VARCHAR(255)*/
                                XmlNodeList = nodoAddressCountry.GetElementsByTagName("Code");
                                if (XmlNodeList.Count != 0)
                                {
                                    aux = XmlNodeList[0].InnerText;
                                    if (aux != "") { scriAddressCountry.Code = aux; } else { scriAddressCountry.Code = ""; }
                                }
                                /*,"Description"  VARCHAR(255)*/
                                XmlNodeList = nodoAddressCountry.GetElementsByTagName("Description");
                                if (XmlNodeList.Count != 0)
                                {
                                    aux = XmlNodeList[0].InnerText;
                                    if (aux != "") { scriAddressCountry.Description = aux; } else { scriAddressCountry.Description = ""; }
                                }

                            } //AddressCountry

                            /***************/
                            /*State*/
                            /***************/
                            XmlState = nodoAddress.GetElementsByTagName("State");
                            foreach (XmlElement nodoState in XmlState)
                            {

                                /*,Code VARCHAR(255)*/
                                XmlNodeList = nodoState.GetElementsByTagName("Code");
                                if (XmlNodeList.Count != 0)
                                {
                                    aux = XmlNodeList[0].InnerText;
                                    if (aux != "") { scriState.Code = aux; } else { scriState.Code = ""; }
                                }
                                /*,"Description"  VARCHAR(255)*/
                                XmlNodeList = nodoState.GetElementsByTagName("Description");
                                if (XmlNodeList.Count != 0)
                                {
                                    aux = XmlNodeList[0].InnerText;
                                    if (aux != "") { scriState.Description = aux; } else { scriState.Description = ""; }
                                }

                            } //State
                        }//Adress

                        /***************/
                        /*Phone*/
                        /***************/
                        XmlPhone = nodo.GetElementsByTagName("Phone");
                        foreach (XmlElement nodoPhone in XmlPhone)
                        {

                            /*PhoneNumber VARCHAR(255)*/
                            XmlNodeList = nodoPhone.GetElementsByTagName("PhoneNumber");
                            if (XmlNodeList.Count != 0)
                            {
                                aux = XmlNodeList[0].InnerText;
                                if (aux != "") { scriPhone.PhoneNumber = aux; } else { scriPhone.PhoneNumber = ""; }
                            }

                            /***************/
                            /*PhoneType*/
                            /***************/
                            XmlPhoneType = nodoPhone.GetElementsByTagName("PhoneType");
                            foreach (XmlElement nodoPhoneType in XmlPhoneType)
                            {
                                /*,Code VARCHAR(255)*/
                                XmlNodeList = nodoPhoneType.GetElementsByTagName("Code");
                                if (XmlNodeList.Count != 0)
                                {
                                    aux = XmlNodeList[0].InnerText;
                                    if (aux != "") { scriPhoneType.Code = aux; } else { scriPhoneType.Code = ""; }
                                }
                                /*,"Description"  VARCHAR(255)*/
                                XmlNodeList = nodoPhoneType.GetElementsByTagName("Description");
                                if (XmlNodeList.Count != 0)
                                {
                                    aux = XmlNodeList[0].InnerText;
                                    if (aux != "") { scriPhoneType.Description = aux; } else { scriPhoneType.Description = ""; }
                                }
                            }

                        } //Phone

                        /***************/
                        /*ContactType*/
                        /***************/
                        XmlContactType = nodo.GetElementsByTagName("ContactType");
                        foreach (XmlElement nodoContactType in XmlContactType)
                        {

                            /*,Code VARCHAR(255)*/
                            XmlNodeList = nodoContactType.GetElementsByTagName("Code");
                            if (XmlNodeList.Count != 0)
                            {
                                aux = XmlNodeList[0].InnerText;
                                if (aux != "") { scriContactType.Code = aux; } else { scriContactType.Code = ""; }
                            }
                            /*,"Description"  VARCHAR(255)*/
                            XmlNodeList = nodoContactType.GetElementsByTagName("Description");
                            if (XmlNodeList.Count != 0)
                            {
                                aux = XmlNodeList[0].InnerText;
                                if (aux != "") { scriContactType.Description = aux; } else { scriContactType.Description = ""; }
                            }

                        } //ContactType

                        /***************/
                        /*Gender*/
                        /***************/
                        XmlGender = nodo.GetElementsByTagName("Gender");
                        foreach (XmlElement nodoGender in XmlGender)
                        {

                            /*,Code VARCHAR(255)*/
                            XmlNodeList = nodoGender.GetElementsByTagName("Code");
                            if (XmlNodeList.Count != 0)
                            {
                                aux = XmlNodeList[0].InnerText;
                                if (aux != "") { scriGender.Code = aux; } else { scriGender.Code = ""; }
                            }
                            /*,"Description"  VARCHAR(255)*/
                            XmlNodeList = nodoGender.GetElementsByTagName("Description");
                            if (XmlNodeList.Count != 0)
                            {
                                aux = XmlNodeList[0].InnerText;
                                if (aux != "") { scriGender.Description = aux; } else { scriGender.Description = ""; }
                            }

                        } //Gender

                        /***************/
                        /*MaritalStatus*/
                        /***************/
                        XmlMaritalStatus = nodo.GetElementsByTagName("MaritalStatus");
                        foreach (XmlElement nodoMaritalStatus in XmlMaritalStatus)
                        {

                            /*,Code VARCHAR(255)*/
                            XmlNodeList = nodoMaritalStatus.GetElementsByTagName("Code");
                            if (XmlNodeList.Count != 0)
                            {
                                aux = XmlNodeList[0].InnerText;
                                if (aux != "") { scriMaritalStatus.Code = aux; } else { scriMaritalStatus.Code = ""; }
                            }
                            /*,"Description"  VARCHAR(255)*/
                            XmlNodeList = nodoMaritalStatus.GetElementsByTagName("DisplayName");
                            if (XmlNodeList.Count != 0)
                            {
                                aux = XmlNodeList[0].InnerText;
                                if (aux != "") { scriMaritalStatus.DisplayName = aux; } else { scriMaritalStatus.DisplayName = ""; }
                            }

                        } //MaritalStatus

                        /***************/
                        /*Nationality*/
                        /***************/
                        XmlNationality = nodo.GetElementsByTagName("Nationality");
                        foreach (XmlElement nodoNationality in XmlNationality)
                        {

                            /*,Code VARCHAR(255)*/
                            XmlNodeList = nodoNationality.GetElementsByTagName("Code");
                            if (XmlNodeList.Count != 0)
                            {
                                aux = XmlNodeList[0].InnerText;
                                if (aux != "") { scriNationality.Code = aux; } else { scriNationality.Code = ""; }
                            }
                            /*,"Description"  VARCHAR(255)*/
                            XmlNodeList = nodoNationality.GetElementsByTagName("Description");
                            if (XmlNodeList.Count != 0)
                            {
                                aux = XmlNodeList[0].InnerText;
                                if (aux != "") { scriNationality.Description = aux; } else { scriNationality.Description = ""; }
                            }

                        } //Nationality

                        /***************/
                        /*Occupation*/
                        /***************/
                        XmlOccupation = nodo.GetElementsByTagName("Occupation");
                        foreach (XmlElement nodoOccupation in XmlOccupation)
                        {

                            /*,Code VARCHAR(255)*/
                            XmlNodeList = nodoOccupation.GetElementsByTagName("Code");
                            if (XmlNodeList.Count != 0)
                            {
                                aux = XmlNodeList[0].InnerText;
                                if (aux != "") { scriOccupation.Code = aux; } else { scriOccupation.Code = ""; }
                            }
                            /*,"Description"  VARCHAR(255)*/
                            XmlNodeList = nodoOccupation.GetElementsByTagName("Description");
                            if (XmlNodeList.Count != 0)
                            {
                                aux = XmlNodeList[0].InnerText;
                                if (aux != "") { scriOccupation.Description = aux; } else { scriOccupation.Description = ""; }
                            }

                        } //Occupation

                        /***************/
                        /*OfficialIDType*/
                        /***************/
                        XmlOfficialIDType = nodo.GetElementsByTagName("OfficialIDType");
                        foreach (XmlElement nodoOfficialIDType in XmlOfficialIDType)
                        {

                            /*,Code VARCHAR(255)*/
                            XmlNodeList = nodoOfficialIDType.GetElementsByTagName("Code");
                            if (XmlNodeList.Count != 0)
                            {
                                aux = XmlNodeList[0].InnerText;
                                if (aux != "") { scriOfficialIDType.Code = aux; } else { scriOfficialIDType.Code = ""; }
                            }
                            /*,"Description"  VARCHAR(255)*/
                            XmlNodeList = nodoOfficialIDType.GetElementsByTagName("Description");
                            if (XmlNodeList.Count != 0)
                            {
                                aux = XmlNodeList[0].InnerText;
                                if (aux != "") { scriOfficialIDType.Description = aux; } else { scriOfficialIDType.Description = ""; }
                            }

                        } //OfficialIDType

                        /***************/
                        /*PreferredSettlementCurrency*/
                        /***************/
                        XmlPreferredSettlementCurrency = nodo.GetElementsByTagName("PreferredSettlementCurrency");
                        foreach (XmlElement nodoPreferredSettlementCurrency in XmlPreferredSettlementCurrency)
                        {

                            /*,Code VARCHAR(255)*/
                            XmlNodeList = nodoPreferredSettlementCurrency.GetElementsByTagName("Code");
                            if (XmlNodeList.Count != 0)
                            {
                                aux = XmlNodeList[0].InnerText;
                                if (aux != "") { scriPreferredSettlementCurrency.Code = aux; } else { scriPreferredSettlementCurrency.Code = ""; }
                            }
                            /*,"Description"  VARCHAR(255)*/
                            XmlNodeList = nodoPreferredSettlementCurrency.GetElementsByTagName("Description");
                            if (XmlNodeList.Count != 0)
                            {
                                aux = XmlNodeList[0].InnerText;
                                if (aux != "") { scriPreferredSettlementCurrency.Description = aux; } else { scriPreferredSettlementCurrency.Description = ""; }
                            }
                            /*, selected 	VARCHAR(255)*/
                            XmlNodeList = nodoPreferredSettlementCurrency.GetElementsByTagName("Selected");
                            if (XmlNodeList.Count != 0)
                            {
                                aux = XmlNodeList[0].InnerText;
                                if (aux != "") { scriPreferredSettlementCurrency.selected = aux; } else { scriPreferredSettlementCurrency.selected = ""; }
                            }

                        } //PreferredSettlementCurrency

                        /***************/
                        /*SchoolLevel*/
                        /***************/
                        XmlSchoolLevel = nodo.GetElementsByTagName("SchoolLevel");
                        foreach (XmlElement nodoSchoolLevel in XmlSchoolLevel)
                        {

                            /*,Code VARCHAR(255)*/
                            XmlNodeList = nodoSchoolLevel.GetElementsByTagName("Code");
                            if (XmlNodeList.Count != 0)
                            {
                                aux = XmlNodeList[0].InnerText;
                                if (aux != "") { scriSchoolLevel.Code = aux; } else { scriSchoolLevel.Code = ""; }
                            }
                            /*,"Description"  VARCHAR(255)*/
                            XmlNodeList = nodoSchoolLevel.GetElementsByTagName("Description");
                            if (XmlNodeList.Count != 0)
                            {
                                aux = XmlNodeList[0].InnerText;
                                if (aux != "") { scriSchoolLevel.Description = aux; } else { scriSchoolLevel.Description = ""; }
                            }

                        } //SchoolLevel

                        /***************/
                        /*TaxStatus*/
                        /***************/
                        XmlTaxStatus = nodo.GetElementsByTagName("TaxStatus");
                        foreach (XmlElement nodoTaxStatus in XmlTaxStatus)
                        {

                            /*[PublicID] [varchar](255) NULL,*/
                            XmlNodeList = nodoTaxStatus.GetElementsByTagName("PublicID");
                            if (XmlNodeList.Count != 0)
                            {
                                aux = XmlNodeList[0].InnerText;
                                if (aux != "") { scriTaxStatus.PublicID = aux; } else { scriTaxStatus.PublicID = ""; }
                            }
                            /*[StatusValue] [varchar](255) NULL,*/
                            XmlNodeList = nodoTaxStatus.GetElementsByTagName("StatusValue");
                            if (XmlNodeList.Count != 0)
                            {
                                aux = XmlNodeList[0].InnerText;
                                if (aux != "") { scriTaxStatus.StatusValue = aux; } else { scriTaxStatus.StatusValue = ""; }
                            }

                            /*[TaxPercentage] [decimal](5, 2) NULL,*/
                            XmlNodeList = nodo.GetElementsByTagName("TaxPercentage");
                            if (XmlNodeList.Count != 0)
                            {
                                aux = XmlNodeList[0].InnerText;
                                if (aux != "") { scriTaxStatus.TaxPercentage = Decimal.Parse(aux.Replace(".", ",")); } else { scriTaxStatus.TaxPercentage = 0; }
                            }

                            /***************/
                            /*EnrollementStatus*/
                            /***************/
                            XmlEnrollementStatus = nodo.GetElementsByTagName("EnrollementStatus");
                            foreach (XmlElement nodoEnrollementStatus in XmlEnrollementStatus)
                            {
                                /*,Code VARCHAR(255)*/
                                XmlNodeList = nodoEnrollementStatus.GetElementsByTagName("Code");
                                if (XmlNodeList.Count != 0)
                                {
                                    aux = XmlNodeList[0].InnerText;
                                    if (aux != "") { scriEnrollementStatus.Code = aux; } else { scriEnrollementStatus.Code = ""; }
                                }
                                /*,"Description"  VARCHAR(255)*/
                                XmlNodeList = nodoEnrollementStatus.GetElementsByTagName("Description");
                                if (XmlNodeList.Count != 0)
                                {
                                    aux = XmlNodeList[0].InnerText;
                                    if (aux != "") { scriEnrollementStatus.Description = aux; } else { scriEnrollementStatus.Description = ""; }
                                }
                            } //EnrollementStatus

                            /***************/
                            /*RetentionAgent*/
                            /***************/
                            XmlRetentionAgent = nodo.GetElementsByTagName("RetentionAgent");
                            foreach (XmlElement nodoRetentionAgent in XmlRetentionAgent)
                            {
                                /*,Code VARCHAR(255)*/
                                XmlNodeList = nodoRetentionAgent.GetElementsByTagName("Code");
                                if (XmlNodeList.Count != 0)
                                {
                                    aux = XmlNodeList[0].InnerText;
                                    if (aux != "") { scriRetentionAgent.Code = aux; } else { scriRetentionAgent.Code = ""; }
                                }
                                /*,"Description"  VARCHAR(255)*/
                                XmlNodeList = nodoRetentionAgent.GetElementsByTagName("Description");
                                if (XmlNodeList.Count != 0)
                                {
                                    aux = XmlNodeList[0].InnerText;
                                    if (aux != "") { scriRetentionAgent.Description = aux; } else { scriRetentionAgent.Description = ""; }
                                }
                            } //RetentionAgent

                        } //TaxStatus 

                    } //Contact
                    _context.ScriContact.Add(scriContact);

                    try
                    {
                        await _context.SaveChangesAsync();
                        scriAddress.idContact = scriContact.Id;
                        _context.ScriAddress.Add(scriAddress);
                        scriPhone.idContact = scriContact.Id;
                        _context.ScriPhone.Add(scriPhone);
                        scriContactType.idContact = scriContact.Id;
                        _context.ScriContactType.Add(scriContactType);
                        scriGender.idContact = scriContact.Id;
                        _context.ScriGender.Add(scriGender);
                        scriMaritalStatus.idContact = scriContact.Id;
                        _context.ScriMaritalStatus.Add(scriMaritalStatus);
                        scriNationality.idContact = scriContact.Id;
                        _context.ScriNationality.Add(scriNationality);
                        scriOccupation.idContact = scriContact.Id;
                        _context.ScriOccupation.Add(scriOccupation);
                        scriOfficialIDType.idContact = scriContact.Id;
                        _context.ScriOfficialIDType.Add(scriOfficialIDType);
                        scriPreferredSettlementCurrency.idContact = scriContact.Id;
                        _context.ScriPreferredSettlementCurrency.Add(scriPreferredSettlementCurrency);
                        scriSchoolLevel.idContact = scriContact.Id;
                        _context.ScriSchoolLevel.Add(scriSchoolLevel);
                        scriTaxStatus.idContact = scriContact.Id;
                        _context.ScriTaxStatus.Add(scriTaxStatus);

                        await _context.SaveChangesAsync();
                        scriAddressType.idAddress = scriAddress.Id;
                        _context.ScriAddressType.Add(scriAddressType);
                        scriAddressCountry.idAddress = scriAddress.Id;
                        _context.ScriAddressCountry.Add(scriAddressCountry);
                        scriState.idAddress = scriAddress.Id;
                        _context.ScriState.Add(scriState);
                        scriPhoneType.idPhone = scriPhone.Id;
                        _context.ScriPhoneType.Add(scriPhoneType);
                        scriEnrollementStatus.idTaxStatus = scriTaxStatus.Id;
                        _context.ScriEnrollementStatus.Add(scriEnrollementStatus);
                        scriRetentionAgent.idTaxStatus = scriTaxStatus.Id;
                        _context.ScriRetentionAgent.Add(scriRetentionAgent);

                        await _context.SaveChangesAsync();
                    }
                    catch (Exception e)
                    {
                        respuesta = "210";
                        return respuesta;//StatusCode(208, e.Message);
                    }

                    /***************/
                    /*Country*/
                    /***************/
                    XmlCountry = doc.GetElementsByTagName("Country");
                    foreach (XmlElement nodo in XmlCountry)
                    {
                        scriCountry.idPolicy = scriPolicy.Id;

                        /*,Code VARCHAR(255)*/
                        XmlNodeList = nodo.GetElementsByTagName("Code");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriCountry.Code = aux; } else { scriCountry.Code = ""; }
                        }
                        /*,"Description"  VARCHAR(255)*/
                        XmlNodeList = nodo.GetElementsByTagName("Description");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriCountry.Description = aux; } else { scriCountry.Description = ""; }
                        }

                    }//Country
                    try
                    {
                        _context.ScriCountry.Add(scriCountry);
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception e)
                    {
                        respuesta = "211";
                        return respuesta;
                    }

                    /***************/
                    /*MaillingAddress*/
                    /***************/
                    XmlMaillingAddress = doc.GetElementsByTagName("MaillingAddress");
                    foreach (XmlElement nodo in XmlMaillingAddress)
                    {
                        scriMaillingAddress.idPolicy = scriPolicy.Id;

                        /*,updateLinkedAddresses VARCHAR(255)*/
                        XmlNodeList = nodo.GetElementsByTagName("updateLinkedAddresses");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriMaillingAddress.updateLinkedAddresses = aux; } else { scriMaillingAddress.updateLinkedAddresses = ""; }
                        }
                        /*,"AddressLine1"  VARCHAR(255)*/
                        XmlNodeList = nodo.GetElementsByTagName("AddressLine1");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriMaillingAddress.AddressLine1 = aux; } else { scriMaillingAddress.AddressLine1 = ""; }
                        }

                        /*,AddressLine2 VARCHAR(255)*/
                        XmlNodeList = nodo.GetElementsByTagName("AddressLine2");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriMaillingAddress.AddressLine2 = aux; } else { scriMaillingAddress.AddressLine2 = ""; }
                        }
                        /*,City VARCHAR(255)*/
                        XmlNodeList = nodo.GetElementsByTagName("City");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriMaillingAddress.City = aux; } else { scriMaillingAddress.City = ""; }
                        }
                        /*,County VARCHAR(255)*/
                        XmlNodeList = nodo.GetElementsByTagName("County");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriMaillingAddress.County = aux; } else { scriMaillingAddress.County = ""; }
                        }
                        /*,DisplayText VARCHAR(255)*/
                        XmlNodeList = nodo.GetElementsByTagName("DisplayText");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriMaillingAddress.DisplayText = aux; } else { scriMaillingAddress.DisplayText = ""; }
                        }
                        /*,PolicyAddress VARCHAR(255)*/
                        XmlNodeList = nodo.GetElementsByTagName("PolicyAddress");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriMaillingAddress.PolicyAddress = aux; } else { scriMaillingAddress.PolicyAddress = ""; }
                        }
                        /*,PostalCode VARCHAR(255)*/
                        XmlNodeList = nodo.GetElementsByTagName("PostalCode");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriMaillingAddress.PostalCode = aux; } else { scriMaillingAddress.PostalCode = ""; }
                        }
                        /*,PrimaryAddress VARCHAR(255)*/
                        XmlNodeList = nodo.GetElementsByTagName("PrimaryAddress");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriMaillingAddress.PrimaryAddress = aux; } else { scriMaillingAddress.PrimaryAddress = ""; }
                        }
                        /*,PublicID VARCHAR(255)*/
                        XmlNodeList = nodo.GetElementsByTagName("PublicID");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriMaillingAddress.PublicID = aux; } else { scriMaillingAddress.PublicID = ""; }
                        }
                        /*,StreetNumber VARCHAR(255)*/
                        XmlNodeList = nodo.GetElementsByTagName("StreetNumber");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriMaillingAddress.StreetNumber = aux; } else { scriMaillingAddress.StreetNumber = ""; }
                        }

                        /***************/
                        /*MaillingAddressAddressType*/
                        /***************/
                        XmlMaillingAddressAddressType = nodo.GetElementsByTagName("AddressType");
                        foreach (XmlElement nodoMaillingAddressAddressType in XmlMaillingAddressAddressType)
                        {

                            /*,Code VARCHAR(255)*/
                            XmlNodeList = nodoMaillingAddressAddressType.GetElementsByTagName("Code");
                            if (XmlNodeList.Count != 0)
                            {
                                aux = XmlNodeList[0].InnerText;
                                if (aux != "") { scriMaillingAddressAddressType.Code = aux; } else { scriMaillingAddressAddressType.Code = ""; }
                            }
                            /*,"Description"  VARCHAR(255)*/
                            XmlNodeList = nodoMaillingAddressAddressType.GetElementsByTagName("Description");
                            if (XmlNodeList.Count != 0)
                            {
                                aux = XmlNodeList[0].InnerText;
                                if (aux != "") { scriMaillingAddressAddressType.Description = aux; } else { scriMaillingAddressAddressType.Description = ""; }
                            }

                        }//MaillingAddressAddressType

                        /***************/
                        /*MaillingAddressCountry*/
                        /***************/
                        XmlMaillingAddressCountry = nodo.GetElementsByTagName("Country");
                        foreach (XmlElement nodoMaillingAddressCountry in XmlMaillingAddressCountry)
                        {

                            /*,Code VARCHAR(255)*/
                            XmlNodeList = nodoMaillingAddressCountry.GetElementsByTagName("Code");
                            if (XmlNodeList.Count != 0)
                            {
                                aux = XmlNodeList[0].InnerText;
                                if (aux != "") { scriMaillingAddressCountry.Code = aux; } else { scriMaillingAddressCountry.Code = ""; }
                            }
                            /*,"Description"  VARCHAR(255)*/
                            XmlNodeList = nodoMaillingAddressCountry.GetElementsByTagName("Description");
                            if (XmlNodeList.Count != 0)
                            {
                                aux = XmlNodeList[0].InnerText;
                                if (aux != "") { scriMaillingAddressCountry.Description = aux; } else { scriMaillingAddressCountry.Description = ""; }
                            }

                        }//MaillingAddressCountry

                        /***************/
                        /*MaillingAddressState*/
                        /***************/
                        XmlMaillingAddressState = nodo.GetElementsByTagName("State");
                        foreach (XmlElement nodoMaillingAddressState in XmlMaillingAddressState)
                        {

                            /*,Code VARCHAR(255)*/
                            XmlNodeList = nodoMaillingAddressState.GetElementsByTagName("Code");
                            if (XmlNodeList.Count != 0)
                            {
                                aux = XmlNodeList[0].InnerText;
                                if (aux != "") { scriMaillingAddressState.Code = aux; } else { scriMaillingAddressState.Code = ""; }
                            }
                            /*,"Description"  VARCHAR(255)*/
                            XmlNodeList = nodoMaillingAddressState.GetElementsByTagName("Description");
                            if (XmlNodeList.Count != 0)
                            {
                                aux = XmlNodeList[0].InnerText;
                                if (aux != "") { scriMaillingAddressState.Description = aux; } else { scriMaillingAddressState.Description = ""; }
                            }

                        }//MaillingAddressState

                    }//MaillingAddress
                    try
                    {
                        _context.ScriMaillingAddress.Add(scriMaillingAddress);
                        await _context.SaveChangesAsync();
                        scriMaillingAddressAddressType.idMaillingAddress = scriMaillingAddress.Id;
                        _context.ScriMaillingAddressAddressType.Add(scriMaillingAddressAddressType);

                        scriMaillingAddressCountry.idMaillingAddress = scriMaillingAddress.Id;
                        _context.ScriMaillingAddressCountry.Add(scriMaillingAddressCountry);
                        scriMaillingAddressState.idMaillingAddress = scriMaillingAddress.Id;
                        _context.ScriMaillingAddressState.Add(scriMaillingAddressState);
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception e)
                    {
                        respuesta = "212";
                        return respuesta;
                    }

                    /***************/
                    /*Ext_RamoSSN*/
                    /***************/
                    XmlExt_RamoSSN = doc.GetElementsByTagName("Ext_RamoSSN");
                    foreach (XmlElement nodo in XmlExt_RamoSSN)
                    {
                        scriExt_RamoSSN.idPolicy = scriPolicy.Id;

                        /*,Code VARCHAR(255)*/
                        XmlNodeList = nodo.GetElementsByTagName("Code");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriExt_RamoSSN.Code = aux; } else { scriExt_RamoSSN.Code = ""; }
                        }
                        /*,"Description"  VARCHAR(255)*/
                        XmlNodeList = nodo.GetElementsByTagName("Description");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriExt_RamoSSN.Description = aux; } else { scriExt_RamoSSN.Description = ""; }
                        }

                    }//Ext_RamoSSN
                    try
                    {
                        _context.ScriExt_RamoSSN.Add(scriExt_RamoSSN);
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception e)
                    {
                        respuesta = "213";
                        return respuesta;
                    }

                    /***************/
                    /*Ext_PolicyType*/
                    /***************/
                    XmlExt_PolicyType = doc.GetElementsByTagName("Ext_PolicyType");
                    foreach (XmlElement nodo in XmlExt_PolicyType)
                    {
                        scriExt_PolicyType.idPolicy = scriPolicy.Id;

                        /*,Code VARCHAR(255)*/
                        XmlNodeList = nodo.GetElementsByTagName("Code");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriExt_PolicyType.Code = aux; } else { scriExt_PolicyType.Code = ""; }
                        }
                        /*,"Description"  VARCHAR(255)*/
                        XmlNodeList = nodo.GetElementsByTagName("Description");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriExt_PolicyType.Description = aux; } else { scriExt_PolicyType.Description = ""; }
                        }

                    }//Ext_PolicyType
                    try
                    {
                        _context.ScriExt_PolicyType.Add(scriExt_PolicyType);
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception e)
                    {
                        respuesta = "214";
                        return respuesta;
                    }

                    /***************/
                    /*ProducerCode*/
                    /***************/
                    XmlProducerCode = doc.GetElementsByTagName("ProducerCode");
                    foreach (XmlElement nodo in XmlProducerCode)
                    {
                        scriProducerCode.idPolicy = scriPolicy.Id;

                        /*,Code VARCHAR(255)*/
                        XmlNodeList = nodo.GetElementsByTagName("Code");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriProducerCode.Code = aux; } else { scriProducerCode.Code = ""; }
                        }
                        /* [OrganizationDisplayName] [varchar] (255) NULL,*/
                        XmlNodeList = nodo.GetElementsByTagName("OrganizationDisplayName");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriProducerCode.OrganizationDisplayName = aux; } else { scriProducerCode.OrganizationDisplayName = ""; }
                        }

                        /*[OrganizationPublicID] [varchar] (255) NULL,*/
                        XmlNodeList = nodo.GetElementsByTagName("OrganizationPublicID");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriProducerCode.OrganizationPublicID = aux; } else { scriProducerCode.OrganizationPublicID = ""; }
                        }
                        /*[PublicID] [varchar] (255) NULL,*/
                        XmlNodeList = nodo.GetElementsByTagName("PublicID");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriProducerCode.PublicID = aux; } else { scriProducerCode.PublicID = ""; }
                        }
                        /*[Selected] [varchar] (255) NULL,*/
                        XmlNodeList = nodo.GetElementsByTagName("Selected");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriProducerCode.Selected = aux; } else { scriProducerCode.Selected = ""; }
                        }

                    }//ProducerCode
                    try
                    {
                        _context.ScriProducerCode.Add(scriProducerCode);
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception e)
                    {
                        respuesta = "215";
                        return respuesta;
                    }

                    /***************/
                    /*PaymentMethod*/
                    /***************/
                    XmlPaymentMethod = doc.GetElementsByTagName("PaymentMethod");
                    foreach (XmlElement nodo in XmlPaymentMethod)
                    {
                        scriPaymentMethod.idPolicy = scriPolicy.Id;

                        /*,Description VARCHAR(255)*/
                        XmlNodeList = nodo.GetElementsByTagName("Description");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriPaymentMethod.Description = aux; } else { scriPaymentMethod.Description = ""; }
                        }
                        /* [IdentificationCode] [varchar] (255) NULL,*/
                        XmlNodeList = nodo.GetElementsByTagName("IdentificationCode");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriPaymentMethod.IdentificationCode = aux; } else { scriPaymentMethod.IdentificationCode = ""; }
                        }

                        /*[Selected] [varchar] (255) NULL,*/
                        XmlNodeList = nodo.GetElementsByTagName("Selected");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriPaymentMethod.Selected = aux; } else { scriPaymentMethod.Selected = ""; }
                        }
                        /*[CBUTarjetaCredito] [varchar] (255) NULL,*/
                        XmlNodeList = nodo.GetElementsByTagName("CBUTarjetaCredito");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriPaymentMethod.CBUTarjetaCredito = aux; } else { scriPaymentMethod.CBUTarjetaCredito = ""; }
                        }

                    }//PaymentMethod
                    try
                    {
                        _context.ScriPaymentMethod.Add(scriPaymentMethod);
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception e)
                    {
                        respuesta = "215";
                        return respuesta;
                    }

                    /***************/
                    /*PolicyTerm*/
                    /***************/
                    XmlPolicyTerm = doc.GetElementsByTagName("PolicyTerm");
                    foreach (XmlElement nodo in XmlPolicyTerm)
                    {
                        scriPolicyTerm.idPolicy = scriPolicy.Id;

                        /*,Description VARCHAR(255)*/
                        XmlNodeList = nodo.GetElementsByTagName("Description");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriPolicyTerm.Description = aux; } else { scriPolicyTerm.Description = ""; }
                        }
                        /* [IdentificationCode] [varchar] (255) NULL,*/
                        XmlNodeList = nodo.GetElementsByTagName("IdentificationCode");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriPolicyTerm.IdentificationCode = aux; } else { scriPolicyTerm.IdentificationCode = ""; }
                        }

                        /*[Selected] [varchar] (255) NULL,*/
                        XmlNodeList = nodo.GetElementsByTagName("Selected");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriPolicyTerm.Selected = aux; } else { scriPolicyTerm.Selected = ""; }
                        }

                    }//PolicyTerm
                    try
                    {
                        _context.ScriPolicyTerm.Add(scriPolicyTerm);
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception e)
                    {
                        respuesta = "216";
                        return respuesta;
                    }

                    /***************/
                    /*Currency*/
                    /***************/
                    XmlCurrency = doc.GetElementsByTagName("Currency");
                    foreach (XmlElement nodo in XmlCurrency)
                    {
                        scriCurrency.idPolicy = scriPolicy.Id;
                        
                        /* [Code] [varchar] (255) NULL,*/
                        XmlNodeList = nodo.GetElementsByTagName("Code");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriCurrency.Code = aux; } else { scriCurrency.Code = ""; }
                        }
                        /*,Description VARCHAR(255)*/
                        XmlNodeList = nodo.GetElementsByTagName("Description");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriCurrency.Description = aux; } else { scriCurrency.Description = ""; }
                        }


                        /*[Selected] [varchar] (255) NULL,*/
                        XmlNodeList = nodo.GetElementsByTagName("Selected");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriCurrency.Selected = aux; } else { scriCurrency.Selected = ""; }
                        }

                    }//Currency
                    try
                    {
                        _context.ScriCurrency.Add(scriCurrency);
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception e)
                    {
                        respuesta = "217";
                        return respuesta;
                    }

                    /***************/
                    /*ProducerAgent*/
                    /***************/
                    XmlProducerAgent = doc.GetElementsByTagName("ProducerAgent");
                    foreach (XmlElement nodo in XmlProducerAgent)
                    {
                        scriProducerAgent.idPolicy = scriPolicy.Id;

                        /* [Code] [varchar] (255) NULL,*/
                        XmlNodeList = nodo.GetElementsByTagName("Code");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriProducerAgent.Code = aux; } else { scriProducerAgent.Code = ""; }
                        }
                        /*,Description VARCHAR(255)*/
                        XmlNodeList = nodo.GetElementsByTagName("OrganizationDisplayName");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriProducerAgent.OrganizationDisplayName = aux; } else { scriProducerAgent.OrganizationDisplayName = ""; }
                        }


                        /*[Selected] [varchar] (255) NULL,*/
                        XmlNodeList = nodo.GetElementsByTagName("OrganizationPublicID");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriProducerAgent.OrganizationPublicID = aux; } else { scriProducerAgent.OrganizationPublicID = ""; }
                        }

                        /*[PublicID] [varchar] (255) NULL,*/
                        XmlNodeList = nodo.GetElementsByTagName("PublicID");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriProducerAgent.PublicID = aux; } else { scriProducerAgent.PublicID = ""; }
                        }

                        /*[Selected] [varchar] (255) NULL,*/
                        XmlNodeList = nodo.GetElementsByTagName("Selected");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriProducerAgent.Selected = aux; } else { scriProducerAgent.Selected = ""; }
                        }

                    }//ProducerAgent
                    try
                    {
                        _context.ScriProducerAgent.Add(scriProducerAgent);
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception e)
                    {
                        respuesta = "217";
                        return respuesta;
                    }

                    /***************/
                    /*ProducerOfService*/
                    /***************/
                    XmlProducerOfService = doc.GetElementsByTagName("ProducerOfService");
                    foreach (XmlElement nodo in XmlProducerOfService)
                    {
                        scriProducerOfService.idPolicy = scriPolicy.Id;

                        /* [Code] [varchar] (255) NULL,*/
                        XmlNodeList = nodo.GetElementsByTagName("Code");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriProducerOfService.Code = aux; } else { scriProducerOfService.Code = ""; }
                        }
                        /*,Description VARCHAR(255)*/
                        XmlNodeList = nodo.GetElementsByTagName("OrganizationDisplayName");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriProducerOfService.OrganizationDisplayName = aux; } else { scriProducerOfService.OrganizationDisplayName = ""; }
                        }


                        /*[Selected] [varchar] (255) NULL,*/
                        XmlNodeList = nodo.GetElementsByTagName("OrganizationPublicID");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriProducerOfService.OrganizationPublicID = aux; } else { scriProducerOfService.OrganizationPublicID = ""; }
                        }

                        /*[PublicID] [varchar] (255) NULL,*/
                        XmlNodeList = nodo.GetElementsByTagName("PublicID");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriProducerOfService.PublicID = aux; } else { scriProducerOfService.PublicID = ""; }
                        }

                        /*[Selected] [varchar] (255) NULL,*/
                        XmlNodeList = nodo.GetElementsByTagName("Selected");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriProducerOfService.Selected = aux; } else { scriProducerOfService.Selected = ""; }
                        }

                    }//ProducerOfService
                    try
                    {
                        _context.ScriProducerOfService.Add(scriProducerOfService);
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception e)
                    {
                        respuesta = "217";
                        return respuesta;
                    }

                    /***************/
                    /*ServiceOrganizer*/
                    /***************/
                    XmlServiceOrganizer = doc.GetElementsByTagName("ServiceOrganizer");
                    foreach (XmlElement nodo in XmlServiceOrganizer)
                    {
                        scriServiceOrganizer.idPolicy = scriPolicy.Id;

                        /* [Code] [varchar] (255) NULL,*/
                        XmlNodeList = nodo.GetElementsByTagName("Code");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriServiceOrganizer.Code = aux; } else { scriServiceOrganizer.Code = ""; }
                        }
                        /*,Description VARCHAR(255)*/
                        XmlNodeList = nodo.GetElementsByTagName("OrganizationDisplayName");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriServiceOrganizer.OrganizationDisplayName = aux; } else { scriServiceOrganizer.OrganizationDisplayName = ""; }
                        }


                        /*[Selected] [varchar] (255) NULL,*/
                        XmlNodeList = nodo.GetElementsByTagName("OrganizationPublicID");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriServiceOrganizer.OrganizationPublicID = aux; } else { scriServiceOrganizer.OrganizationPublicID = ""; }
                        }

                        /*[PublicID] [varchar] (255) NULL,*/
                        XmlNodeList = nodo.GetElementsByTagName("PublicID");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriServiceOrganizer.PublicID = aux; } else { scriServiceOrganizer.PublicID = ""; }
                        }

                        /*[Selected] [varchar] (255) NULL,*/
                        XmlNodeList = nodo.GetElementsByTagName("Selected");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriServiceOrganizer.Selected = aux; } else { scriServiceOrganizer.Selected = ""; }
                        }

                    }//ServiceOrganizer
                    try
                    {
                        _context.ScriServiceOrganizer.Add(scriServiceOrganizer);
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception e)
                    {
                        respuesta = "217";
                        return respuesta;
                    }

                    /***************/
                    /*ChannelEntry*/
                    /***************/
                    XmlChannelEntry = doc.GetElementsByTagName("ChannelEntry");
                    foreach (XmlElement nodo in XmlChannelEntry)
                    {
                        scriChannelEntry.idPolicy = scriPolicy.Id;

                        /* [Code] [varchar] (255) NULL,*/
                        XmlNodeList = nodo.GetElementsByTagName("Code");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriChannelEntry.Code = aux; } else { scriChannelEntry.Code = ""; }
                        }
                        /*,Description VARCHAR(255)*/
                        XmlNodeList = nodo.GetElementsByTagName("Description");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriChannelEntry.Description = aux; } else { scriChannelEntry.Description = ""; }
                        }

                    }//ChannelEntry
                    try
                    {
                        _context.ScriChannelEntry.Add(scriChannelEntry);
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception e)
                    {
                        respuesta = "217";
                        return respuesta;
                    }

                    /***************/
                    /*Status*/
                    /***************/
                    XmlStatus = doc.GetElementsByTagName("Status");
                    foreach (XmlElement nodo in XmlStatus)
                    {
                        scriStatus.idPolicy = scriPolicy.Id;

                        /* [Code] [varchar] (255) NULL,*/
                        XmlNodeList = nodo.GetElementsByTagName("Code");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriStatus.Code = aux; } else { scriStatus.Code = ""; }
                        }
                        /*,Description VARCHAR(255)*/
                        XmlNodeList = nodo.GetElementsByTagName("Description");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriStatus.Description = aux; } else { scriStatus.Description = ""; }
                        }

                    }//Status
                    try
                    {
                        _context.ScriStatus.Add(scriStatus);
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception e)
                    {
                        respuesta = "217";
                        return respuesta;
                    }

                    /***************/
                    /*Vehicle*/
                    /***************/
                    XmlVehicle = doc.GetElementsByTagName("Vehicle");
                    foreach (XmlElement nodo in XmlVehicle)
                    {
                        scriVehicle.idPolicy = scriPolicy.Id;

                        /*[BrandCode] [int] NULL,*/
                        XmlNodeList = nodo.GetElementsByTagName("BrandCode");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriVehicle.BrandCode = Int32.Parse(aux); } else { scriVehicle.BrandCode = 0; }
                        }
                        /*[BrandName] [varchar] (255) NULL,*/
                        XmlNodeList = nodo.GetElementsByTagName("BrandName");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriVehicle.BrandName = aux; } else { scriVehicle.BrandName = ""; }
                        }
                        /*[DeductibleValueDescription] [varchar] (255) NULL,*/
                        XmlNodeList = nodo.GetElementsByTagName("DeductibleValueDescription");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriVehicle.DeductibleValueDescription = aux; } else { scriVehicle.DeductibleValueDescription = ""; }
                        }
                        /*[EngineNumber] [varchar] (255) NULL,*/
                        XmlNodeList = nodo.GetElementsByTagName("EngineNumber");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriVehicle.EngineNumber = aux; } else { scriVehicle.EngineNumber = ""; }
                        }
                        /*[HasClaimComputableForBonusMalus] [varchar] (255) NULL,*/
                        XmlNodeList = nodo.GetElementsByTagName("HasClaimComputableForBonusMalus");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriVehicle.HasClaimComputableForBonusMalus = aux; } else { scriVehicle.HasClaimComputableForBonusMalus = ""; }
                        }
                        /*[HasGPS] [varchar] (255) NULL,*/
                        XmlNodeList = nodo.GetElementsByTagName("HasGPS");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriVehicle.HasGPS = aux; } else { scriVehicle.HasGPS = ""; }
                        }
                        /*[HasInspections] [varchar] (255) NULL,*/
                        XmlNodeList = nodo.GetElementsByTagName("HasInspections");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriVehicle.HasInspections = aux; } else { scriVehicle.HasInspections = ""; }
                        }
                        /*[InfoAutoCode] [varchar] (255) NULL,*/
                        XmlNodeList = nodo.GetElementsByTagName("InfoAutoCode");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriVehicle.InfoAutoCode = aux; } else { scriVehicle.InfoAutoCode = ""; }
                        }
                        /*[Is0Km] [varchar] (255) NULL,*/
                        XmlNodeList = nodo.GetElementsByTagName("Is0Km");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriVehicle.Is0Km = aux; } else { scriVehicle.Is0Km = ""; }
                        }
                        /*[IsPatentedAtArg] [varchar] (255) NULL,*/
                        XmlNodeList = nodo.GetElementsByTagName("IsPatentedAtArg");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriVehicle.IsPatentedAtArg = aux; } else { scriVehicle.IsPatentedAtArg = ""; }
                        }
                        /*[IsTruck10TT100KM] [varchar] (255) NULL,*/
                        XmlNodeList = nodo.GetElementsByTagName("IsTruck10TT100KM");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriVehicle.IsTruck10TT100KM = aux; } else { scriVehicle.IsTruck10TT100KM = ""; }
                        }
                        /*[LicensePlate] [varchar] (255) NULL,*/
                        XmlNodeList = nodo.GetElementsByTagName("LicensePlate");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriVehicle.LicensePlate = aux; } else { scriVehicle.LicensePlate = ""; }
                        }
                        /*[ModelCode] [varchar] (255) NULL,*/
                        XmlNodeList = nodo.GetElementsByTagName("ModelCode");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriVehicle.ModelCode = aux; } else { scriVehicle.ModelCode = ""; }
                        }
                        /*[ModelName] [varchar] (255) NULL,*/
                        XmlNodeList = nodo.GetElementsByTagName("ModelName");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriVehicle.ModelName = aux; } else { scriVehicle.ModelName = ""; }
                        }
                        /*[OriginalCostNew] [decimal](15, 2) NULL,*/
                        XmlNodeList = nodo.GetElementsByTagName("OriginalCostNew");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriVehicle.OriginalCostNew = Decimal.Parse(aux.Replace(".", ",")); } else { scriVehicle.OriginalCostNew = 0; }
                        }
                        /*[OtherBrandName] [varchar] (255) NULL,*/
                        XmlNodeList = nodo.GetElementsByTagName("OtherBrandName");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriVehicle.OtherBrandName = aux; } else { scriVehicle.OtherBrandName = ""; }
                        }
                        /*[OtherModelName] [varchar] (255) NULL,*/
                        XmlNodeList = nodo.GetElementsByTagName("OtherModelName");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriVehicle.OtherModelName = aux; } else { scriVehicle.OtherModelName = ""; }
                        }
                        /*[OtherVersionName] [varchar] (255) NULL,*/
                        XmlNodeList = nodo.GetElementsByTagName("OtherVersionName");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriVehicle.OtherVersionName = aux; } else { scriVehicle.OtherVersionName = ""; }
                        }
                        /*[PolicyOwnerIsInsured][varchar](255) NULL,*/
                        XmlNodeList = nodo.GetElementsByTagName("PolicyOwnerIsInsured");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriVehicle.PolicyOwnerIsInsured = aux; } else { scriVehicle.PolicyOwnerIsInsured = ""; }
                        }
                        /*[PrimaryNamedInsured] [varchar] (255) NULL,*/
                        XmlNodeList = nodo.GetElementsByTagName("PrimaryNamedInsured");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriVehicle.PrimaryNamedInsured = aux; } else { scriVehicle.PrimaryNamedInsured = ""; }
                        }
                        /*[PublicId] [varchar] (255) NULL,*/
                        XmlNodeList = nodo.GetElementsByTagName("PublicId");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriVehicle.PublicId = aux; } else { scriVehicle.PublicId = ""; }
                        }
                        /*[StatedAmount][decimal](15, 2) NULL,**/
                        XmlNodeList = nodo.GetElementsByTagName("StatedAmount");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriVehicle.StatedAmount = Decimal.Parse(aux.Replace(".", ",")); } else { scriVehicle.StatedAmount = 0; }
                        }
                        /*[TargetPremium] [decimal](15, 2) NULL,*/
                        XmlNodeList = nodo.GetElementsByTagName("TargetPremium");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriVehicle.TargetPremium = Decimal.Parse(aux.Replace(".", ",")); } else { scriVehicle.TargetPremium = 0; }
                        }
                        /*[TargetPremiumAfterTax] [decimal](15, 2) NULL,*/
                        XmlNodeList = nodo.GetElementsByTagName("TargetPremiumAfterTax");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriVehicle.TargetPremiumAfterTax = Decimal.Parse(aux.Replace(".", ",")); } else { scriVehicle.TargetPremiumAfterTax = 0; }
                        }
                        /*[VIN] [varchar] (255) NULL,*/
                        XmlNodeList = nodo.GetElementsByTagName("VIN");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriVehicle.VIN = aux; } else { scriVehicle.VIN = ""; }
                        }
                        /*[VTVExpirationDate] [datetime] NULL,*/
                        XmlNodeList = nodo.GetElementsByTagName("VTVExpirationDate");
                        if (XmlNodeList.Count != 0)
                        {
                            try
                            {
                                auxFecha = DateTime.Parse(XmlNodeList[0].InnerText);
                                if (auxFecha == DateTime.Parse("01/01/0001"))
                                {
                                    auxFecha = DateTime.Parse("01/01/1900");
                                }

                                scriVehicle.VTVExpirationDate = auxFecha;
                            }
                            catch
                            {
                                scriVehicle.VTVExpirationDate = DateTime.Parse("01/01/1900");
                            }
                        }
                        else { scriVehicle.VTVExpirationDate = DateTime.Parse("01/01/1900"); }
                        /*[VehicleNumber] [int] NULL,*/
                        XmlNodeList = nodo.GetElementsByTagName("VehicleNumber");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriVehicle.VehicleNumber = Int32.Parse(aux); } else { scriVehicle.VehicleNumber = 0; }
                        }
                        /*[VersionCode] [int] NULL,*/
                        XmlNodeList = nodo.GetElementsByTagName("VersionCode");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriVehicle.VersionCode = Int32.Parse(aux); } else { scriVehicle.VersionCode = 0; }
                        }
                        /*[VersionName] [varchar] (255) NULL,*/
                        XmlNodeList = nodo.GetElementsByTagName("VersionName");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriVehicle.VersionName = aux; } else { scriVehicle.VersionName = ""; }
                        }
                        /*[Year] [int] NULL,*/
                        XmlNodeList = nodo.GetElementsByTagName("Year");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriVehicle.Year = Int32.Parse(aux); } else { scriVehicle.Year = 0; }
                        }

                        /*  [CodigoInfoAuto] [int] NULL,*/
                        XmlNodeList = nodo.GetElementsByTagName("CodigoInfoAuto");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriVehicle.CodigoInfoAuto = Int32.Parse(aux); } else { scriVehicle.CodigoInfoAuto = 0; }
                        }

                        /***************/
                        /*AutomaticAdjust*/
                        /***************/
                        XmlAutomaticAdjust = nodo.GetElementsByTagName("AutomaticAdjust");
                        foreach (XmlElement nodoAutomaticAdjust in XmlAutomaticAdjust)
                        {
                            /* [Code] [varchar] (255) NULL,*/
                            XmlNodeList = nodoAutomaticAdjust.GetElementsByTagName("Code");
                            if (XmlNodeList.Count != 0)
                            {
                                aux = XmlNodeList[0].InnerText;
                                if (aux != "") { scriAutomaticAdjust.Code = aux; } else { scriAutomaticAdjust.Code = ""; }
                            }
                            /*,Description VARCHAR(255)*/
                            XmlNodeList = nodoAutomaticAdjust.GetElementsByTagName("Description");
                            if (XmlNodeList.Count != 0)
                            {
                                aux = XmlNodeList[0].InnerText;
                                if (aux != "") { scriAutomaticAdjust.Description = aux; } else { scriAutomaticAdjust.Description = ""; }
                            }
                        } //AutomaticAdjust

                        /***************/
                        /*Category*/
                        /***************/
                        XmlCategory = nodo.GetElementsByTagName("Category");
                        foreach (XmlElement nodoCategory in XmlCategory)
                        {
                            /* [Code] [varchar] (255) NULL,*/
                            XmlNodeList = nodoCategory.GetElementsByTagName("Code");
                            if (XmlNodeList.Count != 0)
                            {
                                aux = XmlNodeList[0].InnerText;
                                if (aux != "") { scriCategory.Code = aux; } else { scriCategory.Code = ""; }
                            }
                            /*,Description VARCHAR(255)*/
                            XmlNodeList = nodoCategory.GetElementsByTagName("Description");
                            if (XmlNodeList.Count != 0)
                            {
                                aux = XmlNodeList[0].InnerText;
                                if (aux != "") { scriCategory.Description = aux; } else { scriCategory.Description = ""; }
                            }
                        } //Category

                        /***************/
                        /*Color*/
                        /***************/
                        XmlColor = nodo.GetElementsByTagName("Color");
                        foreach (XmlElement nodoColor in XmlColor)
                        {
                            /* [Code] [varchar] (255) NULL,*/
                            XmlNodeList = nodoColor.GetElementsByTagName("Code");
                            if (XmlNodeList.Count != 0)
                            {
                                aux = XmlNodeList[0].InnerText;
                                if (aux != "") { scriColor.Code = aux; } else { scriColor.Code = ""; }
                            }
                            /*,Description VARCHAR(255)*/
                            XmlNodeList = nodoColor.GetElementsByTagName("Description");
                            if (XmlNodeList.Count != 0)
                            {
                                aux = XmlNodeList[0].InnerText;
                                if (aux != "") { scriColor.Description = aux; } else { scriColor.Description = ""; }
                            }
                        } //Color

                        /***************/
                        /*FuelType*/
                        /***************/
                        XmlFuelType = nodo.GetElementsByTagName("FuelType");
                        foreach (XmlElement nodoFuelType in XmlFuelType)
                        {
                            /* [Code] [varchar] (255) NULL,*/
                            XmlNodeList = nodoFuelType.GetElementsByTagName("Code");
                            if (XmlNodeList.Count != 0)
                            {
                                aux = XmlNodeList[0].InnerText;
                                if (aux != "") { scriFuelType.Code = aux; } else { scriFuelType.Code = ""; }
                            }
                            /*,Description VARCHAR(255)*/
                            XmlNodeList = nodoFuelType.GetElementsByTagName("Description");
                            if (XmlNodeList.Count != 0)
                            {
                                aux = XmlNodeList[0].InnerText;
                                if (aux != "") { scriFuelType.Description = aux; } else { scriFuelType.Description = ""; }
                            }
                        } //FuelType

                        /***************/
                        /*Jurisdiction*/
                        /***************/
                        XmlJurisdiction = nodo.GetElementsByTagName("Jurisdiction");
                        foreach (XmlElement nodoJurisdiction in XmlJurisdiction)
                        {
                            /* [Code] [varchar] (255) NULL,*/
                            XmlNodeList = nodoJurisdiction.GetElementsByTagName("Code");
                            if (XmlNodeList.Count != 0)
                            {
                                aux = XmlNodeList[0].InnerText;
                                if (aux != "") { scriJurisdiction.Code = aux; } else { scriJurisdiction.Code = ""; }
                            }
                            /*,Description VARCHAR(255)*/
                            XmlNodeList = nodoJurisdiction.GetElementsByTagName("Description");
                            if (XmlNodeList.Count != 0)
                            {
                                aux = XmlNodeList[0].InnerText;
                                if (aux != "") { scriJurisdiction.Description = aux; } else { scriJurisdiction.Description = ""; }
                            }
                        } //Jurisdiction

                        /***************/
                        /*OriginCountry*/
                        /***************/
                        XmlOriginCountry = nodo.GetElementsByTagName("OriginCountry");
                        foreach (XmlElement nodoOriginCountry in XmlOriginCountry)
                        {
                            /* [Code] [varchar] (255) NULL,*/
                            XmlNodeList = nodoOriginCountry.GetElementsByTagName("Code");
                            if (XmlNodeList.Count != 0)
                            {
                                aux = XmlNodeList[0].InnerText;
                                if (aux != "") { scriOriginCountry.Code = aux; } else { scriOriginCountry.Code = ""; }
                            }
                            /*,Description VARCHAR(255)*/
                            XmlNodeList = nodoOriginCountry.GetElementsByTagName("Description");
                            if (XmlNodeList.Count != 0)
                            {
                                aux = XmlNodeList[0].InnerText;
                                if (aux != "") { scriOriginCountry.Description = aux; } else { scriOriginCountry.Description = ""; }
                            }
                        } //OriginCountry

                        /***************/
                        /*ProductOffering*/
                        /***************/
                        XmlProductOffering = nodo.GetElementsByTagName("ProductOffering");
                        foreach (XmlElement nodoProductOffering in XmlProductOffering)
                        {
                            /* [Code] [varchar] (255) NULL,*/
                            XmlNodeList = nodoProductOffering.GetElementsByTagName("Code");
                            if (XmlNodeList.Count != 0)
                            {
                                aux = XmlNodeList[0].InnerText;
                                if (aux != "") { scriProductOffering.Code = aux; } else { scriProductOffering.Code = ""; }
                            }
                            /*,Description VARCHAR(255)*/
                            XmlNodeList = nodoProductOffering.GetElementsByTagName("Description");
                            if (XmlNodeList.Count != 0)
                            {
                                aux = XmlNodeList[0].InnerText;
                                if (aux != "") { scriProductOffering.Description = aux; } else { scriProductOffering.Description = ""; }
                            }
                        } //ProductOffering

                        /***************/
                        /*RiskLocation*/
                        /***************/
                        XmlRiskLocation = nodo.GetElementsByTagName("RiskLocation");
                        foreach (XmlElement nodoRiskLocation in XmlRiskLocation)
                        {
                            /* [Code] [varchar] (255) NULL,*/
                            XmlNodeList = nodoRiskLocation.GetElementsByTagName("City");
                            if (XmlNodeList.Count != 0)
                            {
                                aux = XmlNodeList[0].InnerText;
                                if (aux != "") { scriRiskLocation.City = aux; } else { scriRiskLocation.City = ""; }
                            }
                            /*[DisplayName] [varchar] (255) NULL,*/
                            XmlNodeList = nodoRiskLocation.GetElementsByTagName("DisplayName");
                            if (XmlNodeList.Count != 0)
                            {
                                aux = XmlNodeList[0].InnerText;
                                if (aux != "") { scriRiskLocation.DisplayName = aux; } else { scriRiskLocation.DisplayName = ""; }
                            }
                            /*[PostalCode] [varchar] (255) NULL,*/
                            XmlNodeList = nodoRiskLocation.GetElementsByTagName("PostalCode");
                            if (XmlNodeList.Count != 0)
                            {
                                aux = XmlNodeList[0].InnerText;
                                if (aux != "") { scriRiskLocation.PostalCode = aux; } else { scriRiskLocation.PostalCode = ""; }
                            }
                            /*[PublicID] [varchar] (255) NULL,*/
                            XmlNodeList = nodoRiskLocation.GetElementsByTagName("PublicID");
                            if (XmlNodeList.Count != 0)
                            {
                                aux = XmlNodeList[0].InnerText;
                                if (aux != "") { scriRiskLocation.PublicID = aux; } else { scriRiskLocation.PublicID = ""; }
                            }
                            /*[Street] [varchar] (255) NULL,*/
                            XmlNodeList = nodoRiskLocation.GetElementsByTagName("Street");
                            if (XmlNodeList.Count != 0)
                            {
                                aux = XmlNodeList[0].InnerText;
                                if (aux != "") { scriRiskLocation.Street = aux; } else { scriRiskLocation.Street = ""; }
                            }

                        } //RiskLocation

                        /***************/
                        /*Usage*/
                        /***************/
                        XmlUsage = nodo.GetElementsByTagName("Usage");
                        foreach (XmlElement nodoUsage in XmlUsage)
                        {
                            /* [Code] [varchar] (255) NULL,*/
                            XmlNodeList = nodoUsage.GetElementsByTagName("Code");
                            if (XmlNodeList.Count != 0)
                            {
                                aux = XmlNodeList[0].InnerText;
                                if (aux != "") { scriUsage.Code = aux; } else { scriUsage.Code = ""; }
                            }
                            /*,Description VARCHAR(255)*/
                            XmlNodeList = nodoUsage.GetElementsByTagName("Description");
                            if (XmlNodeList.Count != 0)
                            {
                                aux = XmlNodeList[0].InnerText;
                                if (aux != "") { scriUsage.Description = aux; } else { scriUsage.Description = ""; }
                            }
                        } //Usage

                    }//Vehicle
                    try
                    {
                        _context.ScriVehicle.Add(scriVehicle);
                        await _context.SaveChangesAsync();
                        scriAutomaticAdjust.idVehicle = scriVehicle.Id;
                        _context.ScriAutomaticAdjust.Add(scriAutomaticAdjust);
                        scriCategory.idVehicle = scriVehicle.Id;
                        _context.ScriCategory.Add(scriCategory);
                        scriColor.idVehicle = scriVehicle.Id;
                        _context.ScriColor.Add(scriColor);
                        scriFuelType.idVehicle = scriVehicle.Id;
                        _context.ScriFuelType.Add(scriFuelType);
                        scriJurisdiction.idVehicle = scriVehicle.Id;
                        _context.ScriJurisdiction.Add(scriJurisdiction);
                        scriOriginCountry.idVehicle = scriVehicle.Id;
                        _context.ScriOriginCountry.Add(scriOriginCountry);
                        scriProductOffering.idVehicle = scriVehicle.Id;
                        _context.ScriProductOffering.Add(scriProductOffering);
                        scriRiskLocation.idVehicle = scriVehicle.Id;
                        _context.ScriRiskLocation.Add(scriRiskLocation);
                        scriUsage.idVehicle = scriVehicle.Id;
                        _context.ScriUsage.Add(scriUsage);
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception e)
                    {
                        respuesta = "218";
                        return respuesta;
                    }

                    /***************/
                    /*ReasonCancelDTO*/
                    /***************/
                    XmlReasonCancelDTO = doc.GetElementsByTagName("ReasonCancelDTO");

                    foreach (XmlElement nodo in XmlReasonCancelDTO)
                    {
                        scriReasonCancelDTO.idPolicy = scriPolicy.Id;

                        /*,ReasonCode VARCHAR(255)*/
                        XmlNodeList = nodo.GetElementsByTagName("ReasonCode");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriReasonCancelDTO.ReasonCode = aux; } else { scriReasonCancelDTO.ReasonCode = ""; }
                        }

                        /*,ReasonDescrip VARCHAR(255)*/
                        XmlNodeList = nodo.GetElementsByTagName("ReasonDescrip");
                        if (XmlNodeList.Count != 0)
                        {
                            aux = XmlNodeList[0].InnerText;
                            if (aux != "") { scriReasonCancelDTO.ReasonDescrip = aux; } else { scriReasonCancelDTO.ReasonDescrip = ""; }
                        }

                        /*,EndDate DATETIME*/
                        XmlNodeList = nodo.GetElementsByTagName("CancellationDate");
                        if (XmlNodeList.Count != 0)
                        {
                            try
                            {
                                auxFecha = DateTime.Parse(XmlNodeList[0].InnerText);
                                if (auxFecha == DateTime.Parse("01/01/0001"))
                                {
                                    auxFecha = DateTime.Parse("01/01/1900");
                                }

                                scriReasonCancelDTO.CancellationDate = auxFecha;
                            }
                            catch
                            {
                                scriReasonCancelDTO.CancellationDate = DateTime.Parse("01/01/1900");
                            }
                        }
                        else { scriReasonCancelDTO.CancellationDate = DateTime.Parse("01/01/1900"); }
                    }//ReasonCancelDTO


                    try
                    {
                        if (scriReasonCancelDTO.idPolicy != 0) { 
                            _context.ScriReasonCancelDTO.Add(scriReasonCancelDTO);
                            await _context.SaveChangesAsync();
                        }
                    }
                    catch (Exception e)
                    {
                        respuesta = "219";
                        return respuesta;//StatusCode(208, e.Message);
                    }


                }; //using (var httpClient = new HttpClient())
            }
            catch (Exception e)
            {
                respuesta = "207";
                return respuesta;//StatusCode(208, e.Message);
            }
            respuesta = "200";
            return respuesta;//StatusCode(208, e.Message);
        }

        }
}
