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
    public class GruposSLCController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public GruposSLCController(ApplicationDbContext context, IMapper mapper, IUserService userService)
        {
            _context = context;
            _mapper = mapper;
            _userService = userService;
        }

        // GET: api/GruposSLC
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MAccGrupos>>> GetMAccGrupos(string codigo, string descripcion)
        {
            return await FindMAccGrupos(codigo, descripcion);
        }

        // GET: api/GruposSLC/origen/{T}/programa/{mdl001}
        [HttpGet("origen/{origen}/programa/{programa}", Name = "GruposSLC")]
        public async Task<ActionResult<DetalleDTO<MAccGrupos>>> GetMAccGrupos(string origen, string programa, string codigo, string descripcion)
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

            var retorno = new DetalleDTO<MAccGrupos>();

            if (origen == "P")
            {
                retorno.datos = await FindMAccGrupos(codigo, descripcion, userConnected.AdCuenta, origen);
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
            else if (origen == "A")
            {
                retorno.datos = await FindMAccGrupos(codigo, descripcion, userConnected.AdCuenta, origen);

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

        // GET: api/GruposSLC/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MAccGrupos>> GetMAccGrupos(int id)
        {
            //Recupero y valido el usuario del token
            AccUsuarios userConnected = _userService.GetUserAllByToken(Request.Headers["Authorization"]);
            if (userConnected == null)
            {
                return StatusCode(419, "Error en autenticación de usuario.");
            }

            var mAccGrupos = await _context.MAccGrupos.FindAsync(id);

            if (mAccGrupos == null)
            {
                return NotFound();
            }

            return mAccGrupos;
        }

        // PUT: api/GruposSLC/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMAccGrupos(int id, MAccGrupos mAccGrupos)
        {
            if (id != mAccGrupos.Id)
            {
                return BadRequest();
            }

            //Recupero y valido el usuario del token
            string userConnected = _userService.GetUserByToken(Request.Headers["Authorization"]);
            if (userConnected == null)
            {
                return StatusCode(419, "Error en autenticación de usuario.");
            }

            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            var mAccGruposOriginal = await _context.MAccGrupos.FindAsync(id);

            if (mAccGruposOriginal.CreadoPor != userConnected)
            {
                return StatusCode(420, "No se puede modificar un registro creado por otro usuario.");
            }

            mAccGrupos.CreadoPor = userConnected;
            mAccGrupos.CreadoEn = DateTime.Now;
            mAccGrupos.AutorizadoPor = null;
            mAccGrupos.AutorizadoEn = null;
            mAccGrupos.Modifica = mAccGruposOriginal.Modifica;

            _context.Entry(mAccGrupos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.InnerException.Message);
            }

            return NoContent();
        }

        // POST: api/GruposSLC
        [HttpPost]
        public async Task<ActionResult<MAccGrupos>> PostMAccGrupos(MAccGrupos mAccGrupos)
        {
            //Recupero y valido el usuario del token
            string userConnected = _userService.GetUserByToken(Request.Headers["Authorization"]);
            if (userConnected == null)
            {
                return StatusCode(419, "Error en autenticación de usuario.");
            }

            mAccGrupos.Id = 0;
            mAccGrupos.CreadoPor = userConnected;
            mAccGrupos.CreadoEn = DateTime.Now;
            mAccGrupos.AutorizadoPor = null;
            mAccGrupos.AutorizadoEn = null;
            mAccGrupos.Modifica = "a";

            _context.MAccGrupos.Add(mAccGrupos);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMAccGrupos", new { id = mAccGrupos.Id }, mAccGrupos);
        }

        // DELETE: api/GruposSLC/autorizar/5
        [HttpDelete("autorizar/{id}")]
        public async Task<ActionResult<MAccGrupos>> DeleteMAccGruposAutorizar(int id)
        {
            var mAccGrupos = await _context.MAccGrupos.FindAsync(id);

            if (mAccGrupos == null)
            {
                return NotFound();
            }

            //Recupero y valido el usuario del token
            string userConnected = _userService.GetUserByToken(Request.Headers["Authorization"]);
            if (userConnected == null)
            {
                return StatusCode(419, "Error en autenticación de usuario.");
            }

            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            if (mAccGrupos.CreadoPor == userConnected)
            {
                return StatusCode(420, "No se puede autorizar el registro. Debe ser autorizado con un usuario distinto.");
            }

            if (mAccGrupos.Modifica == "a")
            {
                var accGrupos = _mapper.Map<AccGrupos>(mAccGrupos);

                accGrupos.Id = 0;
                accGrupos.AutorizadoPor = userConnected;
                accGrupos.AutorizadoEn = DateTime.Now;

                _context.MAccGrupos.Remove(mAccGrupos);
                _context.AccGrupos.Add(accGrupos);

                //Guardo el Log
                var lAccGrupos = _mapper.Map<LAccGrupos>(mAccGrupos);
                lAccGrupos.Id = 0;

                _context.LAccGrupos.Add(lAccGrupos);

            }

            if (mAccGrupos.Modifica == "b")
            {
                var accGrupos = await _context.AccGrupos.FindAsync(mAccGrupos.IdOrigen);
                _context.MAccGrupos.Remove(mAccGrupos);
                _context.AccGrupos.Remove(accGrupos);

                //Guardo el Log
                var lAccGrupos = _mapper.Map<LAccGrupos>(mAccGrupos);
                lAccGrupos.Id = 0;

                _context.LAccGrupos.Add(lAccGrupos);
            }

            if (mAccGrupos.Modifica == "m")
            {
                var accGrupos = await _context.AccGrupos.FindAsync(id);
                accGrupos = _mapper.Map<AccGrupos>(mAccGrupos);

                accGrupos.AutorizadoPor = userConnected;
                accGrupos.AutorizadoEn = DateTime.Now;

                _context.MAccGrupos.Remove(mAccGrupos);
                _context.AccGrupos.Update(accGrupos);

                //Guardo el Log
                var lAccGrupos = _mapper.Map<LAccGrupos>(mAccGrupos);
                lAccGrupos.Id = 0;

                _context.LAccGrupos.Add(lAccGrupos);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.InnerException.Message);
            }

            return mAccGrupos;
        }

        // DELETE: api/GruposSLC/desautorizar/5
        [HttpDelete("desautorizar/{id}")]
        public async Task<ActionResult<MAccGrupos>> DeleteMAccGruposDesautorizar(int id)
        {
            var mAccGrupos = await _context.MAccGrupos.FindAsync(id);

            if (mAccGrupos == null)
            {
                return NotFound();
            }

            //Recupero y valido el usuario del token
            string userConnected = _userService.GetUserByToken(Request.Headers["Authorization"]);
            if (userConnected == null)
            {
                return StatusCode(419, "Error en autenticación de usuario.");
            }

            if (mAccGrupos.CreadoPor == userConnected)
            {
                return StatusCode(420, "No se puede desautorizar el registro. Debe ser desautorizado con un usuario distinto.");
            }

            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            var nAccGrupos = _mapper.Map<NAccGrupos>(mAccGrupos);

            nAccGrupos.Id = 0;
            nAccGrupos.AutorizadoPor = userConnected;
            nAccGrupos.AutorizadoEn = DateTime.Now;
            nAccGrupos.Accionsql = nAccGrupos.Accionsql.ToUpper();

            _context.MAccGrupos.Remove(mAccGrupos);
            _context.NAccGrupos.Add(nAccGrupos);

            //Guardo el Log
            var lAccGrupos = _mapper.Map<LAccGrupos>(mAccGrupos);
            lAccGrupos.Id = 0;
            lAccGrupos.Accionsql = lAccGrupos.Accionsql.ToUpper();

            _context.LAccGrupos.Add(lAccGrupos);
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.InnerException.Message);
            }

            return mAccGrupos;

        }

        // DELETE: api/GruposSLC/eliminar/5
        [HttpDelete("eliminar/{id}")]
        public async Task<ActionResult<MAccGrupos>> DeleteMAccGruposEliminar(int id)
        {
            var mAccGrupos = await _context.MAccGrupos.FindAsync(id);

            if (mAccGrupos == null)
            {
                return NotFound();
            }

            //Recupero y valido el usuario del token
            string userConnected = _userService.GetUserByToken(Request.Headers["Authorization"]);
            if (userConnected == null)
            {
                return StatusCode(419, "Error en autenticación de usuario.");
            }

            if (mAccGrupos.CreadoPor != userConnected)
            {
                return StatusCode(420, "No se puede eliminar el registro, ya que fue creado por otro usuario.");
            }

            _context.MAccGrupos.Remove(mAccGrupos);
            await _context.SaveChangesAsync();

            return mAccGrupos;
        }

        private bool MAccGruposExists(int id)
        {
            return _context.MAccGrupos.Any(e => e.IdOrigen == id);
        }

        private async Task<List<MAccGrupos>> FindMAccGrupos(string codigo, string descripcion, string adCuenta, string origen)
        {
            Expression<Func<MAccGrupos, bool>> where = x => (!string.IsNullOrWhiteSpace(codigo) ? x.Codigo.Contains(codigo) : 1 == 1)
                                && (!string.IsNullOrWhiteSpace(descripcion) ? x.Descripcion.Contains(descripcion) : 1 == 1)
                                && (origen == "P" ? x.CreadoPor == adCuenta : 1 == 1)
                                && (origen == "A" ? x.CreadoPor != adCuenta : 1 == 1);

            return await _context.MAccGrupos.Where(where).ToListAsync();
        }

        private async Task<List<MAccGrupos>> FindMAccGrupos(string codigo, string descripcion)
        {
            Expression<Func<MAccGrupos, bool>> where = x => (!string.IsNullOrWhiteSpace(codigo) ? x.Codigo.Contains(codigo) : 1 == 1)
                                && (!string.IsNullOrWhiteSpace(descripcion) ? x.Descripcion.Contains(descripcion) : 1 == 1);

            return await _context.MAccGrupos.Where(where).ToListAsync();
        }
    }
}
