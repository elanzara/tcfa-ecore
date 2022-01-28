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
    public class PerfilesSLCController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public PerfilesSLCController(ApplicationDbContext context, IMapper mapper, IUserService userService)
        {
            _context = context;
            _mapper = mapper;
            _userService = userService;
        }

        // GET: api/PerfilesSLC
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MAccPerfiles>>> GetMAccPerfiles(string codigo, string descripcion)
        {
            return await FindMAccPerfiles(codigo, descripcion);
        }

        // GET: api/PerfilesSLC/origen/{T}/programa/{mdl001}
        [HttpGet("origen/{origen}/programa/{programa}", Name = "PerfilesSLC")]
        public async Task<ActionResult<DetalleDTO<MAccPerfiles>>> GetMAccPerfiles(string origen, string programa, string codigo, string descripcion)
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

            var retorno = new DetalleDTO<MAccPerfiles>();

            if (origen == "P")
            {
                retorno.datos = await FindMAccPerfiles(codigo, descripcion, userConnected.AdCuenta, origen);
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
                retorno.datos = await FindMAccPerfiles(codigo, descripcion, userConnected.AdCuenta, origen);
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


        // GET: api/PerfilesSLC/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MAccPerfiles>> GetMAccPerfiles(int id)
        {
            //Recupero y valido el usuario del token
            AccUsuarios userConnected = _userService.GetUserAllByToken(Request.Headers["Authorization"]);
            if (userConnected == null)
            {
                return StatusCode(419, "Error en autenticación de usuario.");
            }

            var mAccPerfiles = await _context.MAccPerfiles.FindAsync(id);

            if (mAccPerfiles == null)
            {
                return NotFound();
            }

            return mAccPerfiles;
        }

        // PUT: api/PerfilesSLC/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMAccPerfiles(int id, MAccPerfiles mAccPerfiles)
        {
            if (id != mAccPerfiles.Id)
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

            var mAccPerfilesOriginal = await _context.MAccPerfiles.FindAsync(id);

            if (mAccPerfilesOriginal.CreadoPor != userConnected)
            {
                return StatusCode(420, "No se puede modificar un registro creado por otro usuario.");
            }

            mAccPerfiles.CreadoPor = userConnected;
            mAccPerfiles.CreadoEn = DateTime.Now;
            mAccPerfiles.AutorizadoPor = null;
            mAccPerfiles.AutorizadoEn = null;
            mAccPerfiles.Modifica = mAccPerfilesOriginal.Modifica;

            _context.Entry(mAccPerfiles).State = EntityState.Modified;

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

        // POST: api/PerfilesSLC
        [HttpPost]
        public async Task<ActionResult<MAccPerfiles>> PostMAccPerfiles(MAccPerfiles mAccPerfiles)
        {
            //Recupero y valido el usuario del token
            string userConnected = _userService.GetUserByToken(Request.Headers["Authorization"]);
            if (userConnected == null)
            {
                return StatusCode(419, "Error en autenticación de usuario.");
            }

            mAccPerfiles.Id = 0;
            mAccPerfiles.CreadoPor = userConnected;
            mAccPerfiles.CreadoEn = DateTime.Now;
            mAccPerfiles.AutorizadoPor = null;
            mAccPerfiles.AutorizadoEn = null;
            mAccPerfiles.Modifica = "a";

            _context.MAccPerfiles.Add(mAccPerfiles);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMAccPerfiles", new { id = mAccPerfiles.Id }, mAccPerfiles);
        }

        // DELETE: api/PerfilesSLC/autorizar/5
        [HttpDelete("autorizar/{id}")]
        public async Task<ActionResult<MAccPerfiles>> DeleteMAccPerfilesAutorizar(int id)
        {
            var mAccPerfiles = await _context.MAccPerfiles.FindAsync(id);

            if (mAccPerfiles == null)
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

            if (mAccPerfiles.CreadoPor == userConnected)
            {
                return StatusCode(420, "No se puede autorizar el registro. Debe ser autorizado con un usuario distinto.");
            }

            if (mAccPerfiles.Modifica == "a")
            {
                var accPerfiles = _mapper.Map<AccPerfiles>(mAccPerfiles);

                accPerfiles.Id = 0;
                accPerfiles.AutorizadoPor = userConnected;
                accPerfiles.AutorizadoEn = DateTime.Now;

                _context.MAccPerfiles.Remove(mAccPerfiles);
                _context.AccPerfiles.Add(accPerfiles);

                //Guardo el Log
                var lAccPerfiles = _mapper.Map<LAccPerfiles>(mAccPerfiles);
                lAccPerfiles.Id = 0;

                _context.LAccPerfiles.Add(lAccPerfiles);

            }

            if (mAccPerfiles.Modifica == "b")
            {
                var accPerfiles = await _context.AccPerfiles.FindAsync(mAccPerfiles.IdOrigen);
                _context.MAccPerfiles.Remove(mAccPerfiles);
                _context.AccPerfiles.Remove(accPerfiles);

                //Guardo el Log
                var lAccPerfiles = _mapper.Map<LAccPerfiles>(mAccPerfiles);
                lAccPerfiles.Id = 0;

                _context.LAccPerfiles.Add(lAccPerfiles);
            }

            if (mAccPerfiles.Modifica == "m")
            {
                var accPerfiles = await _context.AccPerfiles.FindAsync(id);
                accPerfiles = _mapper.Map<AccPerfiles>(mAccPerfiles);

                accPerfiles.AutorizadoPor = userConnected;
                accPerfiles.AutorizadoEn = DateTime.Now;

                _context.MAccPerfiles.Remove(mAccPerfiles);
                _context.AccPerfiles.Update(accPerfiles);

                //Guardo el Log
                var lAccPerfiles = _mapper.Map<LAccPerfiles>(mAccPerfiles);
                lAccPerfiles.Id = 0;

                _context.LAccPerfiles.Add(lAccPerfiles);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.InnerException.Message);
            }

            return mAccPerfiles;
        }

        // DELETE: api/PerfilesSLC/desautorizar/5
        [HttpDelete("desautorizar/{id}")]
        public async Task<ActionResult<MAccPerfiles>> DeleteMAccPerfilesDesautorizar(int id)
        {
            var mAccPerfiles = await _context.MAccPerfiles.FindAsync(id);

            if (mAccPerfiles == null)
            {
                return NotFound();
            }

            //Recupero y valido el usuario del token
            string userConnected = _userService.GetUserByToken(Request.Headers["Authorization"]);
            if (userConnected == null)
            {
                return StatusCode(419, "Error en autenticación de usuario.");
            }

            if (mAccPerfiles.CreadoPor == userConnected)
            {
                return StatusCode(420, "No se puede desautorizar el registro. Debe ser desautorizado con un usuario distinto.");
            }

            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            var nAccPerfiles = _mapper.Map<NAccPerfiles>(mAccPerfiles);

            nAccPerfiles.Id = 0;
            nAccPerfiles.AutorizadoPor = userConnected;
            nAccPerfiles.AutorizadoEn = DateTime.Now;
            nAccPerfiles.Accionsql = nAccPerfiles.Accionsql.ToUpper();

            _context.MAccPerfiles.Remove(mAccPerfiles);
            _context.NAccPerfiles.Add(nAccPerfiles);

            //Guardo el Log
            var lAccPerfiles = _mapper.Map<LAccPerfiles>(mAccPerfiles);
            lAccPerfiles.Id = 0;
            lAccPerfiles.Accionsql = lAccPerfiles.Accionsql.ToUpper();

            _context.LAccPerfiles.Add(lAccPerfiles);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.InnerException.Message);
            }

            return mAccPerfiles;
        }

        // DELETE: api/PerfilesSLC/eliminar/5
        [HttpDelete("eliminar/{id}")]
        public async Task<ActionResult<MAccPerfiles>> DeleteMAccPerfilesEliminar(int id)
        {
            var mAccPerfiles = await _context.MAccPerfiles.FindAsync(id);

            if (mAccPerfiles == null)
            {
                return NotFound();
            }

            //Recupero y valido el usuario del token
            string userConnected = _userService.GetUserByToken(Request.Headers["Authorization"]);
            if (userConnected == null)
            {
                return StatusCode(419, "Error en autenticación de usuario.");
            }

            if (mAccPerfiles.CreadoPor != userConnected)
            {
                return StatusCode(420, "No se puede eliminar el registro, ya que fue creado por otro usuario.");
            }

            _context.MAccPerfiles.Remove(mAccPerfiles);
            await _context.SaveChangesAsync();

            return mAccPerfiles;
        }

        private bool MAccPerfilesExists(int id)
        {
            return _context.MAccPerfiles.Any(e => e.IdOrigen == id);
        }

        private async Task<List<MAccPerfiles>> FindMAccPerfiles(string codigo, string descripcion, string adCuenta, string origen)
        {
            Expression<Func<MAccPerfiles, bool>> where = x => (!string.IsNullOrWhiteSpace(codigo) ? x.Codigo.Contains(codigo) : 1 == 1)
                                && (!string.IsNullOrWhiteSpace(descripcion) ? x.Descripcion.Contains(descripcion) : 1 == 1)
                                && (origen == "P" ? x.CreadoPor == adCuenta : 1 == 1)
                                && (origen == "A" ? x.CreadoPor != adCuenta : 1 == 1);

            return await _context.MAccPerfiles.Where(where).ToListAsync();
        }

        private async Task<List<MAccPerfiles>> FindMAccPerfiles(string codigo, string descripcion)
        {
            Expression<Func<MAccPerfiles, bool>> where = x => (!string.IsNullOrWhiteSpace(codigo) ? x.Codigo.Contains(codigo) : 1 == 1)
                                && (!string.IsNullOrWhiteSpace(descripcion) ? x.Descripcion.Contains(descripcion) : 1 == 1);

            return await _context.MAccPerfiles.Where(where).ToListAsync();
        }
    }
}
