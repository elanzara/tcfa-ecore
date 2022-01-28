using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eCore.Context;
using eCore.Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using eCore.Services.WebApi.Services;
using AutoMapper;
using eCore.Models;
using System.Data.SqlClient;
using System.Linq.Expressions;

namespace eCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [EnableCors("AllowOrigin")]
    public class ProgramasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public ProgramasController(ApplicationDbContext context, IMapper mapper, IUserService userService)
        {
            _context = context;
            _mapper = mapper;
            _userService = userService;
        }

        //GET: api/Programas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProgramasDTO>>> GetAccProgramas(string codigo, string descripcion)
        {
            List<AccProgramas> lista = await FindAccProgramas(codigo, descripcion);
            return _mapper.Map<List<ProgramasDTO>>(lista);
        }

        // GET: api/Programas/origen/{T}/programa/{mdl001}
        [HttpGet("origen/{origen}/programa/{programa}", Name = "Programas")]
        public async Task<ActionResult<DetalleDTO<AccProgramas>>> GetAccProgramas(string origen, string programa, string codigo, string descripcion)
        {
            //Recupero y valido el usuario del token
            AccUsuarios userConnected = _userService.GetUserAllByToken(Request.Headers["Authorization"]);
            if (userConnected == null)
            {
                return StatusCode(419, "Error en autenticación de usuario.");
            }

            var accPrograma = await _context.AccProgramas.FirstOrDefaultAsync(x => x.Codigo == programa);

            if (accPrograma == null)
            {
                return NotFound();
            }

            var retorno = new DetalleDTO<AccProgramas>();

            if (origen == "T")
            {
                retorno.datos = await FindAccProgramas(codigo, descripcion);

                var perfilPar = new SqlParameter("@perfil", userConnected.IdAccPerfil);
                var programaPar = new SqlParameter("@programa", accPrograma.Id);
                var origenPar = new SqlParameter("@origen", origen);
                var accAcciones = await _context.AccAcciones.FromSql($@"select a.[id]
                                                                              ,a.[codigo]
                                                                              ,a.[descripcion]
                                                                              ,a.[icono]
                                                                              ,a.[creado_por]
                                                                              ,a.[creado_en]
                                                                              ,a.[autorizado_por]
                                                                              ,a.[autorizado_en]
                                                                        from acc_acciones a
                                                                        join acc_programas_acciones pa ON pa.id_acc_accion = a.id
                                                                        join acc_programas_acciones_x_grupo pg ON pg.id_programa_accion = pa.id
                                                                        join acc_grupos g ON g.id = pg.id_acc_grupo
                                                                        join acc_grupos_x_perfil gp ON gp.id_acc_grupo = g.id
                                                                        where gp.id_acc_perfil = @perfil
                                                                        and pa.id_acc_programa = @programa
                                                                        and (pa.origen = @origen OR pa.origen = 'X')
                                                                        group by   a.[id]
		                                                                          ,a.[codigo]
		                                                                          ,a.[descripcion]
		                                                                          ,a.[icono]
		                                                                          ,a.[creado_por]
		                                                                          ,a.[creado_en]
		                                                                          ,a.[autorizado_por]
		                                                                          ,a.[autorizado_en]", perfilPar, programaPar, origenPar).ToListAsync();

                retorno.acciones = _mapper.Map<List<AccionesDTO>>(accAcciones);
            }

            return retorno;
        }

        // GET: api/Programas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AccProgramas>> GetAccProgramas(int id)
        {
            //Recupero y valido el usuario del token
            AccUsuarios userConnected = _userService.GetUserAllByToken(Request.Headers["Authorization"]);
            if (userConnected == null)
            {
                return StatusCode(419, "Error en autenticación de usuario.");
            }

            var accProgramas = await _context.AccProgramas.FindAsync(id);

            if (accProgramas == null)
            {
                return NotFound();
            }

            return accProgramas;
        }

        // PUT: api/Programas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccProgramas(int id, AccProgramas accProgramas)
        {
            if (id != accProgramas.Id)
            {
                return BadRequest();
            }

            //Recupero y valido el usuario del token
            string userConnected = _userService.GetUserByToken(Request.Headers["Authorization"]);
            if (userConnected == null)
            {
                return StatusCode(419, "Error en autenticación de usuario.");
            }

            if (!MAccProgramasExists(id))
            {
                var mAccProgramas = _mapper.Map<MAccProgramas>(accProgramas);
                mAccProgramas.Id = 0;
                mAccProgramas.Modifica = "m";
                mAccProgramas.CreadoPor = userConnected;
                mAccProgramas.CreadoEn = DateTime.Now;
                mAccProgramas.AutorizadoPor = null;
                mAccProgramas.AutorizadoEn = null;

                _context.MAccProgramas.Add(mAccProgramas);
            }
            else
            {
                return StatusCode(420, "El registro ya está pendiente de autorización.");
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccProgramasExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e.InnerException.Message);
            }

            return NoContent();
        }

        // DELETE: api/Programas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AccProgramas>> DeleteAccProgramas(int id)
        {
            var accProgramas = await _context.AccProgramas.FindAsync(id);

            if (accProgramas == null)
            {
                return NotFound();
            }

            //Recupero y valido el usuario del token
            string userConnected = _userService.GetUserByToken(Request.Headers["Authorization"]);
            if (userConnected == null)
            {
                return StatusCode(419, "Error en autenticación de usuario.");
            }

            if (MAccProgramasExists(id))
            {
                return StatusCode(420, "Ya existe una modificación pendiente del mismo registro.");
            }

            var mAccProgramas = _mapper.Map<MAccProgramas>(accProgramas);
            mAccProgramas.Id = 0;
            mAccProgramas.Modifica = "b";
            mAccProgramas.CreadoPor = userConnected;
            mAccProgramas.CreadoEn = DateTime.Now;
            mAccProgramas.AutorizadoPor = null;
            mAccProgramas.AutorizadoEn = null;

            _context.MAccProgramas.Add(mAccProgramas);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.InnerException.Message);
            }

            return accProgramas;
        }

        private bool AccProgramasExists(int id)
        {
            return _context.AccProgramas.Any(e => e.Id == id);
        }

        private bool MAccProgramasExists(int id)
        {
            return _context.MAccProgramas.Any(e => e.IdOrigen == id);
        }

        private async Task<List<AccProgramas>> FindAccProgramas(string codigo, string descripcion)
        {
            Expression<Func<AccProgramas, bool>> where = x => (!string.IsNullOrWhiteSpace(codigo) ? x.Codigo.Contains(codigo) : 1 == 1)
                                && (!string.IsNullOrWhiteSpace(descripcion) ? x.Descripcion.Contains(descripcion) : 1 == 1);

            return await _context.AccProgramas.Where(where).ToListAsync();
        }
    }
}
