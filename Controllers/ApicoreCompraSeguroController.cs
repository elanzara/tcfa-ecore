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
    public class ApicoreCompraSeguroController : ControllerBase
    {
        private readonly SecureOMDb _context;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IConfiguration Configuration;

        public ApicoreCompraSeguroController(SecureOMDb context, IMapper mapper, IUserService userService, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _userService = userService;
            Configuration = configuration;
        }

        //Task<ActionResult<IEnumerable<TrfNovedades>>>
        // GET: api/ApicoreCompraSeguro/
        [HttpGet("", Name = "getComprado")]
        public async Task<IEnumerable<ApicoreCompraSeguro>> GetComprado()
        {
            //return _context.ApicoreCompraSeguro.FromSql("select id,cobertura,compania,createdAt,estado,externalCoberturaId, externalCotizacionId, nroPoliza, primaPoliza, trackId, updatedAt, auto_id, persona_id, user_id, medioPago_id, fechaFuturaCotizacion from apicore_compra_seguro where estado = 'COMPRADO'");

            var comprado = _context.ApicoreCompraSeguro.FromSql("select id,cobertura,compania,createdAt,estado,externalCoberturaId, externalCotizacionId, nroPoliza, primaPoliza, trackId, updatedAt, auto_id, persona_id, user_id, medioPago_id, fechaFuturaCotizacion from apicore_compra_seguro where estado = 'COMPRADO'").ToListAsync();
            //var scriTaxId = await _context.ScriTaxId.AllAsync(); //.FirstOrDefault();
            return await comprado;

        }

        [HttpGet("{id}/Estado/{estado}", Name = "change")]
        public async Task<string> GetChange(int id, string estado)
        {
            //var change = _context.ApicoreCompraSeguro.FromSql("UPDATE apicore_compra_seguro SET estado = " + estado + "' WHERE id = " + id).ToListAsync();

            var change = _context.Database.ExecuteSqlCommand("UPDATE apicore_compra_seguro SET estado = '" + estado + "' WHERE id = " + id);

            if (change == 1) {
                return "200";
            } else
            {
                return "205";
            }
            
        }

    }
}
