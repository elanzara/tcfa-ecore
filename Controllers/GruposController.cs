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
using AutoMapper;
using eCore.Services.WebApi.Services;
using eCore.Models;
using System.Data.SqlClient;
using System.Linq.Expressions;

namespace eCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [EnableCors("AllowOrigin")]
    public class GruposController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public GruposController(ApplicationDbContext context, IMapper mapper, IUserService userService)
        {
            _context = context;
            _mapper = mapper;
            _userService = userService;
        }

        // GET: api/Grupos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccGrupos>>> GetAccGrupos(string codigo, string descripcion)
        {
            return await FindAccGrupos(codigo, descripcion);
        }

        // GET: api/Grupos/origen/{T}/programa/{mdl001}
        [HttpGet("origen/{origen}/programa/{programa}", Name = "Grupos")]
        public async Task<ActionResult<DetalleDTO<AccGrupos>>> GetAccGrupos(string origen, string programa, string codigo, string descripcion)
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

            var retorno = new DetalleDTO<AccGrupos>();

            if (origen == "T")
            {
                retorno.datos = await FindAccGrupos(codigo, descripcion);

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

        // GET: api/Grupos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AccGrupos>> GetAccGrupos(int id)
        {
            //Recupero y valido el usuario del token
            AccUsuarios userConnected = _userService.GetUserAllByToken(Request.Headers["Authorization"]);
            if (userConnected == null)
            {
                return StatusCode(419, "Error en autenticación de usuario.");
            }

            var accGrupos = await _context.AccGrupos.Include(b => b.AccGruposXPerfil).ThenInclude(c => c.IdAccPerfilNavigation).FirstOrDefaultAsync(i => i.Id == id);

            if (accGrupos == null)
            {
                return NotFound();
            }

            return accGrupos;
        }

        // PUT: api/Grupos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccGrupos(int id, AccGrupos accGrupos)
        {
            if (id != accGrupos.Id)
            {
                return BadRequest();
            }

            //Recupero y valido el usuario del token
            string userConnected = _userService.GetUserByToken(Request.Headers["Authorization"]);
            if (userConnected == null)
            {
                return StatusCode(419, "Error en autenticación de usuario.");
            }

            if (!MAccGruposExists(id))
            {
                var mAccGrupos = _mapper.Map<MAccGrupos>(accGrupos);
                mAccGrupos.Id = 0;
                mAccGrupos.Modifica = "m";
                mAccGrupos.CreadoPor = userConnected;
                mAccGrupos.CreadoEn = DateTime.Now;
                mAccGrupos.AutorizadoPor = null;
                mAccGrupos.AutorizadoEn = null;

                _context.MAccGrupos.Add(mAccGrupos);
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
                if (!AccGruposExists(id))
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

        // DELETE: api/Grupos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AccGrupos>> DeleteAccGrupos(int id)
        {
            var accGrupos = await _context.AccGrupos.FindAsync(id);

            if (accGrupos == null)
            {
                return NotFound();
            }

            //Recupero y valido el usuario del token
            string userConnected = _userService.GetUserByToken(Request.Headers["Authorization"]);
            if (userConnected == null)
            {
                return StatusCode(419, "Error en autenticación de usuario.");
            }

            if (MAccGruposExists(id))
            {
                return StatusCode(420, "Ya existe una modificación pendiente del mismo registro.");
            }

            var mAccGrupos = _mapper.Map<MAccGrupos>(accGrupos);
            mAccGrupos.Id = 0;
            mAccGrupos.Modifica = "b";
            mAccGrupos.CreadoPor = userConnected;
            mAccGrupos.CreadoEn = DateTime.Now;
            mAccGrupos.AutorizadoPor = null;
            mAccGrupos.AutorizadoEn = null;

            _context.MAccGrupos.Add(mAccGrupos);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.InnerException.Message);
            }

            return accGrupos;
        }

        private bool AccGruposExists(int id)
        {
            return _context.AccGrupos.Any(e => e.Id == id);
        }

        private bool MAccGruposExists(int id)
        {
            return _context.MAccGrupos.Any(e => e.IdOrigen == id);
        }

        private async Task<List<AccGrupos>> FindAccGrupos(string codigo, string descripcion)
        {
            Expression<Func<AccGrupos, bool>> where = x => (!string.IsNullOrWhiteSpace(codigo) ? x.Codigo.Contains(codigo) : 1 == 1)
                                && (!string.IsNullOrWhiteSpace(descripcion) ? x.Descripcion.Contains(descripcion) : 1 == 1);

            return await _context.AccGrupos.Where(where).ToListAsync();
        }
    }
}
