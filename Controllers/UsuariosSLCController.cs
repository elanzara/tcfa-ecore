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

namespace eCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [EnableCors("AllowOrigin")]
    public class UsuariosSLCController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UsuariosSLCController(ApplicationDbContext context, IMapper mapper, IUserService userService)
        {
            _context = context;
            _mapper = mapper;
            _userService = userService;
        }

        // GET: api/UsuariosSLC
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MAccUsuarios>>> GetMAccUsuarios(string nombres, string apellido, string adCuenta, int? idAccPerfil)
        {
            return await FindMAccUsuarios(nombres, apellido, adCuenta, idAccPerfil);
        }

        // GET: api/UsuariosSLC/origen/{T}/programa/{mdl001}
        [HttpGet("origen/{origen}/programa/{programa}", Name = "UsuariosSLC")]
        public async Task<ActionResult<DetalleDTO<MAccUsuarios>>> GetMAccUsuarios(string origen, string programa, string nombres, string apellido, string adCuenta, int? idAccPerfil)
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

            var retorno = new DetalleDTO<MAccUsuarios>();

            if (origen == "P")
            {
                retorno.datos = await FindMAccUsuarios(nombres, apellido, adCuenta, idAccPerfil, userConnected.AdCuenta, origen);
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
                retorno.datos = await FindMAccUsuarios(nombres, apellido, adCuenta, idAccPerfil, userConnected.AdCuenta, origen);
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

        // GET: api/UsuariosSLC/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MAccUsuarios>> GetMAccUsuarios(int id)
        {
            //Recupero y valido el usuario del token
            AccUsuarios userConnected = _userService.GetUserAllByToken(Request.Headers["Authorization"]);
            if (userConnected == null)
            {
                return StatusCode(419, "Error en autenticación de usuario.");
            }

            var mAccUsuarios = await _context.MAccUsuarios.FindAsync(id);

            if (mAccUsuarios == null)
            {
                return NotFound();
            }

            return mAccUsuarios;
        }

        // PUT: api/UsuariosSLC/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMAccUsuarios(int id, MAccUsuarios mAccUsuarios)
        {
            if (id != mAccUsuarios.Id)
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

            if (!AccPerfilesExists(mAccUsuarios.IdAccPerfil))
            {
                return StatusCode(421, "El perfil con id " + mAccUsuarios.IdAccPerfil + " no existe.");
            }

            var mAccUsuariosOriginal = await _context.MAccUsuarios.FindAsync(id);

            if (mAccUsuariosOriginal.CreadoPor != userConnected)
            {
                return StatusCode(420, "No se puede modificar un registro creado por otro usuario.");
            }

            mAccUsuarios.CreadoPor = userConnected;
            mAccUsuarios.CreadoEn = DateTime.Now;
            mAccUsuarios.AutorizadoPor = null;
            mAccUsuarios.AutorizadoEn = null;
            mAccUsuarios.Modifica = mAccUsuariosOriginal.Modifica;

            _context.Entry(mAccUsuarios).State = EntityState.Modified;

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

        // POST: api/UsuariosSLC
        [HttpPost]
        public async Task<ActionResult<MAccUsuarios>> PostMAccUsuarios(MAccUsuarios mAccUsuarios)
        {
            //Recupero y valido el usuario del token
            string userConnected = _userService.GetUserByToken(Request.Headers["Authorization"]);
            if (userConnected == null)
            {
                return StatusCode(419, "Error en autenticación de usuario.");
            }

            if (!AccPerfilesExists(mAccUsuarios.IdAccPerfil))
            {
                return StatusCode(421, "El perfil con id " + mAccUsuarios.IdAccPerfil + " no existe.");
            }

            mAccUsuarios.Id = 0;
            mAccUsuarios.CreadoPor = userConnected;
            mAccUsuarios.CreadoEn = DateTime.Now;
            mAccUsuarios.AutorizadoPor = null;
            mAccUsuarios.AutorizadoEn = null;
            mAccUsuarios.Modifica = "a";

            _context.MAccUsuarios.Add(mAccUsuarios);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.InnerException.Message);
            }

            return CreatedAtAction("GetMAccusuarios", new { id = mAccUsuarios.Id }, mAccUsuarios);
        }

        // DELETE: api/UsuariosSLC/autorizar/5
        [HttpDelete("autorizar/{id}")]
        public async Task<ActionResult<MAccUsuarios>> DeleteMAccUsuariosAutorizar(int id)
        {
            var mAccUsuarios = await _context.MAccUsuarios.FindAsync(id);

            if (mAccUsuarios == null)
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

            if (mAccUsuarios.CreadoPor == userConnected)
            {
                return StatusCode(420, "No se puede autorizar el registro. Debe ser autorizado con un usuario distinto.");
            }

            if (mAccUsuarios.Modifica == "a")
            {
                var accUsuarios = _mapper.Map<AccUsuarios>(mAccUsuarios);

                accUsuarios.Id = 0;
                accUsuarios.AutorizadoPor = userConnected;
                accUsuarios.AutorizadoEn = DateTime.Now;

                _context.MAccUsuarios.Remove(mAccUsuarios);
                _context.AccUsuarios.Add(accUsuarios);

                //Guardo el Log
                var lAccUsuarios = _mapper.Map<LAccUsuarios>(mAccUsuarios);
                lAccUsuarios.Id = 0;

                _context.LAccUsuarios.Add(lAccUsuarios);

            }

            if (mAccUsuarios.Modifica == "b")
            {
                var accUsuarios = await _context.AccUsuarios.FindAsync(mAccUsuarios.IdOrigen);
                _context.MAccUsuarios.Remove(mAccUsuarios);
                _context.AccUsuarios.Remove(accUsuarios);

                //Guardo el Log
                var lAccUsuarios = _mapper.Map<LAccUsuarios>(mAccUsuarios);
                lAccUsuarios.Id = 0;

                _context.LAccUsuarios.Add(lAccUsuarios);
            }

            if (mAccUsuarios.Modifica == "m")
            {
                var accUsuarios = await _context.AccUsuarios.FindAsync(id);
                accUsuarios = _mapper.Map<AccUsuarios>(mAccUsuarios);

                accUsuarios.AutorizadoPor = userConnected;
                accUsuarios.AutorizadoEn = DateTime.Now;

                _context.MAccUsuarios.Remove(mAccUsuarios);
                _context.AccUsuarios.Update(accUsuarios);

                //Guardo el Log
                var lAccUsuarios = _mapper.Map<LAccUsuarios>(mAccUsuarios);
                lAccUsuarios.Id = 0;

                _context.LAccUsuarios.Add(lAccUsuarios);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.InnerException.Message);
            }

            return mAccUsuarios;
        }

        // DELETE: api/UsuariosSLC/desautorizar/5
        [HttpDelete("desautorizar/{id}")]
        public async Task<ActionResult<MAccUsuarios>> DeleteMAccUsuariosDesautorizar(int id)
        {
            var mAccUsuarios = await _context.MAccUsuarios.FindAsync(id);

            if (mAccUsuarios == null)
            {
                return NotFound();
            }

            //Recupero y valido el usuario del token
            string userConnected = _userService.GetUserByToken(Request.Headers["Authorization"]);
            if (userConnected == null)
            {
                return StatusCode(419, "Error en autenticación de usuario.");
            }

            if (mAccUsuarios.CreadoPor == userConnected)
            {
                return StatusCode(420, "No se puede desautorizar el registro. Debe ser desautorizado con un usuario distinto.");
            }

            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            var nAccUsuarios = _mapper.Map<NAccUsuarios>(mAccUsuarios);

            nAccUsuarios.Id = 0;
            nAccUsuarios.AutorizadoPor = userConnected;
            nAccUsuarios.AutorizadoEn = DateTime.Now;
            nAccUsuarios.Accionsql = nAccUsuarios.Accionsql.ToUpper();

            _context.MAccUsuarios.Remove(mAccUsuarios);
            _context.NAccUsuarios.Add(nAccUsuarios);

            //Guardo el Log
            var lAccUsuarios = _mapper.Map<LAccUsuarios>(mAccUsuarios);
            lAccUsuarios.Id = 0;
            lAccUsuarios.Accionsql = lAccUsuarios.Accionsql.ToUpper();

            _context.LAccUsuarios.Add(lAccUsuarios);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.InnerException.Message);
            }

            return mAccUsuarios;
        }

        // DELETE: api/UsuariosSLC/eliminar/5
        [HttpDelete("eliminar/{id}")]
        public async Task<ActionResult<MAccUsuarios>> DeleteMAccUsuariosEliminar(int id)
        {
            var mAccUsuarios = await _context.MAccUsuarios.FindAsync(id);

            if (mAccUsuarios == null)
            {
                return NotFound();
            }

            //Recupero y valido el usuario del token
            string userConnected = _userService.GetUserByToken(Request.Headers["Authorization"]);
            if (userConnected == null)
            {
                return StatusCode(419, "Error en autenticación de usuario.");
            }

            if (mAccUsuarios.CreadoPor != userConnected)
            {
                return StatusCode(420, "No se puede eliminar el registro, ya que fue creado por otro usuario.");
            }

            _context.MAccUsuarios.Remove(mAccUsuarios);
            await _context.SaveChangesAsync();

            return mAccUsuarios;
        }

        private bool MAccUsuariosExists(int id)
        {
            return _context.MAccUsuarios.Any(e => e.Id == id);
        }

        private bool AccPerfilesExists(int? id)
        {
            return _context.AccPerfiles.Any(m => m.Id == id);
        }

        private async Task<List<MAccUsuarios>> FindMAccUsuarios(string nombres, string apellido, string adCuenta, int? idAccPerfil)
        {
            Expression<Func<MAccUsuarios, bool>> where = x => (!string.IsNullOrWhiteSpace(nombres) ? x.Nombres.Contains(nombres) : 1 == 1)
                                && (!string.IsNullOrWhiteSpace(apellido) ? x.Apellido.Contains(apellido) : 1 == 1)
                                && (!string.IsNullOrWhiteSpace(adCuenta) ? x.AdCuenta.Contains(adCuenta) : 1 == 1)
                                && (idAccPerfil != null ? x.IdAccPerfil.Equals(idAccPerfil) : 1 == 1);

            return await _context.MAccUsuarios.Where(where).ToListAsync();
        }

        private async Task<List<MAccUsuarios>> FindMAccUsuarios(string nombres, string apellido, string adCuenta, int? idAccPerfil, string userConnectedAdCuenta, string origen)
        {
            Expression<Func<MAccUsuarios, bool>> where = x => (!string.IsNullOrWhiteSpace(nombres) ? x.Nombres.Contains(nombres) : 1 == 1)
                                && (!string.IsNullOrWhiteSpace(apellido) ? x.Apellido.Contains(apellido) : 1 == 1)
                                && (!string.IsNullOrWhiteSpace(adCuenta) ? x.AdCuenta.Contains(adCuenta) : 1 == 1)
                                && (idAccPerfil != null ? x.IdAccPerfil.Equals(idAccPerfil) : 1 == 1)
                                && (origen == "P" ? x.CreadoPor == userConnectedAdCuenta : 1 == 1)
                                && (origen == "A" ? x.CreadoPor != userConnectedAdCuenta : 1 == 1);

            return await _context.MAccUsuarios.Where(where).ToListAsync();
        }
    }
}
