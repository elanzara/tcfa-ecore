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
    public class AbmCoberturaController : ControllerBase
    {
        private readonly SecureOMDb _context;
        private readonly SecureDbContext _contexts;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IConfiguration Configuration;

        public AbmCoberturaController(SecureOMDb context, SecureDbContext contexts, IMapper mapper, IUserService userService, IConfiguration configuration)
        {
            _context = context;
            _contexts = contexts;
            _mapper = mapper;
            _userService = userService;
            Configuration = configuration;
        }

        // GET: api/AbmCobertura/
        [HttpGet("compania/", Name = "getcompania")]
        public async Task<List<WsBrokerCompania>> GetCompania()
        {
            var companias = _context.WsBrokerCompania.FromSql("SELECT ID, Codigo, Nombre from ws_broker_compania order by ID").ToListAsync();
            return await companias;
        }

        // GET: api/AbmCobertura/
        [HttpGet("familia/", Name = "getfamilia")]
        public async Task<List<WsBrokerFamilia>> GetFamilia()
        {
            var familias = _context.WsBrokerFamilia.FromSql("SELECT FamiliaID, Codigo, Descripcion from ws_broker_familia order by FamiliaID").ToListAsync();
            return await familias;
        }

        // GET: api/AbmCobertura/
        [HttpGet("ciacobertura/", Name = "getciacobertura")]
        public async Task<List<WsBrokerCiaCoberturaDetalle>> GetCiaCobertura(int CompaniaID, string CoberturaID)
        {
            var query = "SELECT CompaniaID, CoberturaID, Detalle FROM ws_broker_cia_cobertura_detalle WHERE CompaniaID = " + CompaniaID + " AND CoberturaID = '" + CoberturaID + "'";
            var cobertura = _context.WsBrokerCiaCoberturaDetalle.FromSql(query).ToListAsync();
            return await cobertura;
        }
        // GET: api/AbmCobertura/
        [HttpGet("ciafamilia/", Name = "getciafamilia")]
        public async Task<ActionResult> GetCiaFamilia(int CompaniaID, string CoberturaID)
        {
            var query = "select CodigoTCFA, CompaniaID, FamiliaID, Cobertura, Activo, AceptaPagoEfectivo from ws_broker_cia_familia WHERE CompaniaID = " + CompaniaID + " AND Cobertura = '" + CoberturaID + "'";
            var ciafamilia = _context.WsBrokerCiaFamilia.FromSql(query).ToListAsync();
            var count = ciafamilia.Result.Count();

            if (count > 0)
            {
                return StatusCode(201, "Registro existente");
            }
            else
            {
                return StatusCode(200, "OK");
            }
        }
        // GET: api/AbmCobertura/
        [HttpGet("somdetallecobertura/", Name = "getsomdetallecobertura")]
        public async Task<ActionResult> GetSomDetalleCobertura(int CompaniaID, string CoberturaID)
        {
            var query = "select Id, CodigoTCFA, CompaniaID, Cobertura, Nombre from som_detallecoberturas WHERE CompaniaID = " + CompaniaID + " AND Cobertura = '" + CoberturaID + "'";
            var detallecobertura = _contexts.SomDetalleCoberturas.FromSql(query).ToListAsync();
            var count = detallecobertura.Result.Count();

            if (count > 0)
            {
                return StatusCode(201, "Registro existente");
            }
            else
            {
                return StatusCode(200, "OK");
            }
        }
        // GET: api/AbmCobertura/
        [HttpGet("altacobertura/", Name = "getaltacobertura")]
        public async Task<ActionResult> GetAltaCobertura(int CompaniaID, string CoberturaID, int familiaId, int activo, int acepta, string telesales, string detalle)
        {
            var query = "";
            /*ws_broker_cia_cobertura_detalle*/
            query = "SELECT CompaniaID, CoberturaID, Detalle FROM ws_broker_cia_cobertura_detalle WHERE CompaniaID = " + CompaniaID + " AND CoberturaID = '" + CoberturaID + "'";
            var cobertura = _context.WsBrokerCiaCoberturaDetalle.FromSql(query).ToListAsync();
            var count = cobertura.Result.Count();

            if (count > 0)
            {
                return StatusCode(260, "Registro duplicado en ws_broker_cia_cobertura_detalle");
            }
            /*FIN ws_broker_cia_cobertura_detalle*/

            /*ws_broker_cia_familia*/
            query = "select CodigoTCFA, CompaniaID, FamiliaID, Cobertura, Activo, AceptaPagoEfectivo from ws_broker_cia_familia WHERE CompaniaID = " + CompaniaID + " AND Cobertura = '" + CoberturaID + "'";
            var ciafamilia = _context.WsBrokerCiaFamilia.FromSql(query).ToListAsync();
            count = ciafamilia.Result.Count();

            if (count > 0)
            {
                return StatusCode(261, "Registro duplicado en ws_broker_cia_familia");
            }
            /*FIN ws_broker_cia_familia*/

            /*som_detallecoberturas*/
            query = "select Id, CodigoTCFA, CompaniaID, Cobertura, Nombre from som_detallecoberturas WHERE CompaniaID = " + CompaniaID + " AND Cobertura = '" + CoberturaID + "'";
            var detallecobertura = _contexts.SomDetalleCoberturas.FromSql(query).ToListAsync();
            count = detallecobertura.Result.Count();

            if (count > 0)
            {
                return StatusCode(262, "Registro duplicado en som_detallecoberturas");
            }
            /*FIN som_detallecoberturas*/

            var error = "";
            /*ws_broker_cia_cobertura_detalle.*/
            //var query = "INSERT INTO ws_broker_cia_cobertura_detalle (CompaniaID, CoberturaID, Detalle) VALUES ("+ CompaniaID + ", "+ CoberturaID+ ",'" + detalle+"')";
            Entities.WsBrokerCiaCoberturaDetalle wsBrokerCiaCoberturaDetalle = new WsBrokerCiaCoberturaDetalle();
            wsBrokerCiaCoberturaDetalle.CompaniaID = CompaniaID;
            wsBrokerCiaCoberturaDetalle.CoberturaID = CoberturaID;
            wsBrokerCiaCoberturaDetalle.Detalle = detalle;
            _context.WsBrokerCiaCoberturaDetalle.Add(wsBrokerCiaCoberturaDetalle);

            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                // return StatusCode(500, e.InnerException.Message);
                error = error + " - " + e.InnerException.Message;
            }

            /*ws_broker_cia_familia*/
            Entities.WsBrokerCiaFamilia wsBrokerCiaFamilia = new WsBrokerCiaFamilia();
            wsBrokerCiaFamilia.CompaniaID = CompaniaID;
            wsBrokerCiaFamilia.FamiliaID = familiaId;
            wsBrokerCiaFamilia.Cobertura = CoberturaID;
            Boolean bactivo;
            if (activo ==1) { bactivo = true; } else { bactivo = false; }
            wsBrokerCiaFamilia.Activo = bactivo;
            Boolean bacepta;
            if (acepta == 1) { bacepta = true; } else { bacepta = false; }
            wsBrokerCiaFamilia.AceptaPagoEfectivo = bacepta;
            _context.WsBrokerCiaFamilia.Add(wsBrokerCiaFamilia);

            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                // return StatusCode(500, e.InnerException.Message);
                error = error + " - " + e.InnerException.Message;
            }

            /*SOM_DetalleCoberturas*/
            Entities.SomDetalleCoberturas somDetalleCoberturas = new SomDetalleCoberturas();
            somDetalleCoberturas.CompaniaID = CompaniaID;
            somDetalleCoberturas.Cobertura = CoberturaID;
            somDetalleCoberturas.Nombre = telesales;
            _contexts.SomDetalleCoberturas.Add(somDetalleCoberturas);

            try
            {
                _contexts.SaveChanges();
            }
            catch (Exception e)
            {
                // return StatusCode(500, e.InnerException.Message);
                error = error + " - " + e.InnerException.Message;
            }

            if (error == "")
            {
                return StatusCode(200, "OK");
            }
            else
            {
                return StatusCode(201, error);
            }
        }
        // GET: api/AbmCobertura/
        [HttpGet("updatecobertura/", Name = "getUpdateCobertura")]
        public async Task<List<WsBrokerCiaCoberturaDetalle>> getUpdateCobertura(int CompaniaID, string CoberturaID, string Detalle)
        {
            var query = "UPDATE ws_broker_cia_cobertura_detalle SET Detalle = '" + Detalle + "' WHERE  CompaniaID = " + CompaniaID + " AND CoberturaID = '" + CoberturaID + "'; SELECT CompaniaID, CoberturaID, Detalle FROM ws_broker_cia_cobertura_detalle WHERE CompaniaID = " + CompaniaID + " AND CoberturaID = '" + CoberturaID + "'";
            //"SELECT CompaniaID, CoberturaID, Detalle FROM ws_broker_cia_cobertura_detalle WHERE CompaniaID = " + CompaniaID + " AND CoberturaID = '" + CoberturaID + "'";
            var cobertura = _context.WsBrokerCiaCoberturaDetalle.FromSql(query).ToListAsync();
            return await cobertura;
        }
        // GET: api/AbmCobertura/
        [HttpGet("reportecobertura/", Name = "getReporteCobertura")]
        public async Task<List<ReporteCobertura>> getReporteCobertura(int CompaniaID, string Estado)
        {
            var query = "Select cd.CompaniaID, c.Nombre as Compania, cd.CoberturaID, cd.Detalle, cf.FamiliaID, f.Descripcion, cf.Activo, cf.AceptaPagoEfectivo, dc.Nombre as Telesale" +
                " from ws_broker_cia_cobertura_detalle cd " +
                " , ws_broker_compania c " +
                " , ws_broker_cia_familia cf " +
                " , ws_broker_familia f " +
                " , Seguros..SOM_DetalleCoberturas dc " +
                " WHERE cd.CompaniaID = c.ID " +
                " AND cf.CompaniaID = cd.CompaniaID " +
                " AND cf.cobertura = cd.CoberturaID" + 
                " AND cf.FamiliaID = f.FamiliaID " +
                " AND dc.CompaniaID = cd.CompaniaID " +
                " AND dc.Cobertura = cd.CoberturaID ";

            if (Estado == "1")
            {
                query = query + " AND cf.Activo = " + Estado;
            }
            if (CompaniaID != 0)
            {
                query = query + " AND cd.CompaniaID = " + CompaniaID;
            }


            var cobertura = _context.ReporteCobertura.FromSql(query).ToList();

            return cobertura;
        }
    }
}
