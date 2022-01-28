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
    public class PerfilesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public PerfilesController(ApplicationDbContext context, IMapper mapper, IUserService userService)
        {
            _context = context;
            _mapper = mapper;
            _userService = userService;
        }

        //GET: api/Perfiles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccPerfiles>>> GetAccPerfiles(string codigo, string descripcion)
        {
            return await FindAccPerfiles(codigo, descripcion);
        }

        // GET: api/Perfiles/origen/{T}/programa/{mdl001}
        [HttpGet("origen/{origen}/programa/{programa}", Name = "Perfiles")]
        public async Task<ActionResult<DetalleDTO<AccPerfiles>>> GetAccPerfiles(string origen, string programa, string codigo, string descripcion)
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

            var retorno = new DetalleDTO<AccPerfiles>();

            if (origen == "T")
            {
                retorno.datos = await FindAccPerfiles(codigo, descripcion);

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

        // GET: api/Perfiles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AccPerfiles>> GetAccPerfiles(int id)
        {
            //Recupero y valido el usuario del token
            AccUsuarios userConnected = _userService.GetUserAllByToken(Request.Headers["Authorization"]);
            if (userConnected == null)
            {
                return StatusCode(419, "Error en autenticación de usuario.");
            }

            var accPerfiles = await _context.AccPerfiles.Include(b => b.AccGruposXPerfil).ThenInclude(c => c.IdAccGrupoNavigation).FirstOrDefaultAsync(i => i.Id == id);

            if (accPerfiles == null)
            {
                return NotFound();
            }

            return accPerfiles;
        }

        // PUT: api/Perfiles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccPerfiles(int id, AccPerfiles accPerfiles)
        {
            if (id != accPerfiles.Id)
            {
                return BadRequest();
            }

            //Recupero y valido el usuario del token
            string userConnected = _userService.GetUserByToken(Request.Headers["Authorization"]);
            if (userConnected == null)
            {
                return StatusCode(419, "Error en autenticación de usuario.");
            }

            if (!MAccPerfilesExists(id))
            {
                var mAccPerfiles = _mapper.Map<MAccPerfiles>(accPerfiles);
                mAccPerfiles.Id = 0;
                mAccPerfiles.Modifica = "m";
                mAccPerfiles.CreadoPor = userConnected;
                mAccPerfiles.CreadoEn = DateTime.Now;
                mAccPerfiles.AutorizadoPor = null;
                mAccPerfiles.AutorizadoEn = null;

                _context.MAccPerfiles.Add(mAccPerfiles);
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
                if (!AccPerfilesExists(id))
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

        // DELETE: api/Perfiles/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AccPerfiles>> DeleteAccPerfiles(int id)
        {
            var accPerfiles = await _context.AccPerfiles.FindAsync(id);

            if (accPerfiles == null)
            {
                return NotFound();
            }

            //Recupero y valido el usuario del token
            string userConnected = _userService.GetUserByToken(Request.Headers["Authorization"]);
            if (userConnected == null)
            {
                return StatusCode(419, "Error en autenticación de usuario.");
            }

            if (MAccPerfilesExists(id))
            {
                return StatusCode(420, "Ya existe una modificación pendiente del mismo registro.");
            }

            var mAccPerfiles = _mapper.Map<MAccPerfiles>(accPerfiles);
            mAccPerfiles.Id = 0;
            mAccPerfiles.Modifica = "b";
            mAccPerfiles.CreadoPor = userConnected;
            mAccPerfiles.CreadoEn = DateTime.Now;
            mAccPerfiles.AutorizadoPor = null;
            mAccPerfiles.AutorizadoEn = null;

            _context.MAccPerfiles.Add(mAccPerfiles);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.InnerException.Message);
            }

            return accPerfiles;
        }

        private bool AccPerfilesExists(int id)
        {
            return _context.AccPerfiles.Any(e => e.Id == id);
        }

        private bool MAccPerfilesExists(int id)
        {
            return _context.MAccPerfiles.Any(e => e.IdOrigen == id);
        }

        private async Task<List<AccPerfiles>> FindAccPerfiles(string codigo, string descripcion)
        {
            Expression<Func<AccPerfiles, bool>> where = x => (!string.IsNullOrWhiteSpace(codigo) ? x.Codigo.Contains(codigo) : 1 == 1)
                                && (!string.IsNullOrWhiteSpace(descripcion) ? x.Descripcion.Contains(descripcion) : 1 == 1);

            return await _context.AccPerfiles.Where(where).ToListAsync();
        }
    }
}
