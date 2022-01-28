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
    public class ModulosSLCController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public ModulosSLCController(ApplicationDbContext context, IMapper mapper, IUserService userService)
        {
            _context = context;
            _mapper = mapper;
            _userService = userService;
        }

        // GET: api/ModulosSLC
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MAccModulos>>> GetMAccModulos(string codigo, string descripcion)
        {
            return await FindMAccModulos(codigo, descripcion);
        }

        // GET: api/ModulosSLC/origen/{T}/programa/{mdl001}
        [HttpGet("origen/{origen}/programa/{programa}", Name = "ModulosSLC")]
        public async Task<ActionResult<DetalleDTO<MAccModulos>>> GetMAccModulos(string origen, string programa, string codigo, string descripcion)
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

            var retorno = new DetalleDTO<MAccModulos>();

            if (origen == "P")
            {
                retorno.datos = await FindMAccModulos(codigo, descripcion, userConnected.AdCuenta, origen);
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
                retorno.datos = await FindMAccModulos(codigo, descripcion, userConnected.AdCuenta, origen);
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

        // GET: api/ModulosSLC/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MAccModulos>> GetAccModulos(int id)
        {
            //Recupero y valido el usuario del token
            AccUsuarios userConnected = _userService.GetUserAllByToken(Request.Headers["Authorization"]);
            if (userConnected == null)
            {
                return StatusCode(419, "Error en autenticación de usuario.");
            }

            var mAccModulos = await _context.MAccModulos.FindAsync(id);

            if (mAccModulos == null)
            {
                return NotFound();
            }

            return mAccModulos;
        }

        // PUT: api/ModulosSLC/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMAccModulos(int id, MAccModulos mAccModulos)
        {
            if (id != mAccModulos.Id)
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

            if (mAccModulos.IdAccModulo != null && !AccModulosExists(mAccModulos.IdAccModulo))
            {
                return StatusCode(421, "El módulo con id " + mAccModulos.IdAccModulo + " no existe.");
            }

            var mAccModulosOriginal = await _context.MAccModulos.FindAsync(id);

            if (mAccModulosOriginal.CreadoPor != userConnected)
            {
                return StatusCode(420, "No se puede modificar un registro creado por otro usuario.");
            }

            mAccModulos.CreadoPor = userConnected;
            mAccModulos.CreadoEn = DateTime.Now;
            mAccModulos.AutorizadoPor = null;
            mAccModulos.AutorizadoEn = null;
            mAccModulos.Modifica = mAccModulosOriginal.Modifica;

            _context.Entry(mAccModulos).State = EntityState.Modified;

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

        // POST: api/ModulosSLC
        [HttpPost]
        public async Task<ActionResult<MAccModulos>> PostMAccModulos(MAccModulos mAccModulos)
        {
            //Recupero y valido el usuario del token
            string userConnected = _userService.GetUserByToken(Request.Headers["Authorization"]);
            if (userConnected == null)
            {
                return StatusCode(419, "Error en autenticación de usuario.");
            }

            if (mAccModulos.IdAccModulo != null && !AccModulosExists(mAccModulos.IdAccModulo))
            {
                return StatusCode(421, "El módulo con id " + mAccModulos.IdAccModulo + " no existe.");
            }

            mAccModulos.Id = 0;
            mAccModulos.CreadoPor = userConnected;
            mAccModulos.CreadoEn = DateTime.Now;
            mAccModulos.AutorizadoPor = null;
            mAccModulos.AutorizadoEn = null;
            mAccModulos.Modifica = "a";

            _context.MAccModulos.Add(mAccModulos);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMAccModulos", new { id = mAccModulos.Id }, mAccModulos);
        }

        // DELETE: api/ModulosSLC/autorizar/5
        [HttpDelete("autorizar/{id}")]
        public async Task<ActionResult<MAccModulos>> DeleteMAccModulosAutorizar(int id)
        {
            var mAccModulos = await _context.MAccModulos.FindAsync(id);

            if (mAccModulos == null)
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

            if (mAccModulos.CreadoPor == userConnected)
            {
                return StatusCode(420, "No se puede autorizar el registro. Debe ser autorizado con un usuario distinto.");
            }

            if (mAccModulos.Modifica == "a")
            {
                var accModulos = _mapper.Map<AccModulos>(mAccModulos);

                accModulos.Id = 0;
                accModulos.AutorizadoPor = userConnected;
                accModulos.AutorizadoEn = DateTime.Now;

                _context.MAccModulos.Remove(mAccModulos);
                _context.AccModulos.Add(accModulos);

                //Guardo el Log
                var lAccModulos = _mapper.Map<LAccModulos>(mAccModulos);
                lAccModulos.Id = 0;

                _context.LAccModulos.Add(lAccModulos);

            }

            if (mAccModulos.Modifica == "b")
            {
                var accModulos = await _context.AccModulos.FindAsync(mAccModulos.IdOrigen);
                _context.MAccModulos.Remove(mAccModulos);
                _context.AccModulos.Remove(accModulos);

                //Guardo el Log
                var lAccModulos = _mapper.Map<LAccModulos>(mAccModulos);
                lAccModulos.Id = 0;

                _context.LAccModulos.Add(lAccModulos);
            }

            if (mAccModulos.Modifica == "m")
            {
                var accModulos = await _context.AccModulos.FindAsync(id);
                accModulos = _mapper.Map<AccModulos>(mAccModulos);

                accModulos.AutorizadoPor = userConnected;
                accModulos.AutorizadoEn = DateTime.Now;

                _context.MAccModulos.Remove(mAccModulos);
                _context.AccModulos.Update(accModulos);

                //Guardo el Log
                var lAccModulos = _mapper.Map<LAccModulos>(mAccModulos);
                lAccModulos.Id = 0;

                _context.LAccModulos.Add(lAccModulos);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.InnerException.Message);
            }

            return mAccModulos;
        }

        // DELETE: api/ModulosSLC/desautorizar/5
        [HttpDelete("desautorizar/{id}")]
        public async Task<ActionResult<MAccModulos>> DeleteMAccModulosDesautorizar(int id)
        {
            var mAccModulos = await _context.MAccModulos.FindAsync(id);

            if (mAccModulos == null)
            {
                return NotFound();
            }

            //Recupero y valido el usuario del token
            string userConnected = _userService.GetUserByToken(Request.Headers["Authorization"]);
            if (userConnected == null)
            {
                return StatusCode(419, "Error en autenticación de usuario.");
            }

            if (mAccModulos.CreadoPor == userConnected)
            {
                return StatusCode(420, "No se puede desautorizar el registro. Debe ser desautorizado con un usuario distinto.");
            }

            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            var nAccModulos = _mapper.Map<NAccModulos>(mAccModulos);

            nAccModulos.Id = 0;
            nAccModulos.AutorizadoPor = userConnected;
            nAccModulos.AutorizadoEn = DateTime.Now;
            nAccModulos.Accionsql = nAccModulos.Accionsql.ToUpper();

            _context.MAccModulos.Remove(mAccModulos);
            _context.NAccModulos.Add(nAccModulos);

            //Guardo el Log
            var lAccModulos = _mapper.Map<LAccModulos>(mAccModulos);
            lAccModulos.Id = 0;
            lAccModulos.Accionsql = lAccModulos.Accionsql.ToUpper();

            _context.LAccModulos.Add(lAccModulos);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.InnerException.Message);
            }

            return mAccModulos;
        }

        // DELETE: api/ModulosSLC/eliminar/5
        [HttpDelete("eliminar/{id}")]
        public async Task<ActionResult<MAccModulos>> DeleteMAccModulosEliminar(int id)
        {
            var mAccModulos = await _context.MAccModulos.FindAsync(id);

            if (mAccModulos == null)
            {
                return NotFound();
            }

            //Recupero y valido el usuario del token
            string userConnected = _userService.GetUserByToken(Request.Headers["Authorization"]);
            if (userConnected == null)
            {
                return StatusCode(419, "Error en autenticación de usuario.");
            }

            if (mAccModulos.CreadoPor != userConnected)
            {
                return StatusCode(420, "No se puede eliminar el registro, ya que fue creado por otro usuario.");
            }

            _context.MAccModulos.Remove(mAccModulos);
            await _context.SaveChangesAsync();

            return mAccModulos;
        }


        private bool MAccModulosExists(int id)
        {
            return _context.MAccModulos.Any(e => e.IdOrigen == id);
        }

        private bool AccModulosExists(int? id)
        {
            return _context.AccModulos.Any(m => m.Id == id);
        }

        private async Task<List<MAccModulos>> FindMAccModulos(string codigo, string descripcion, string adCuenta, string origen)
        {
            Expression<Func<MAccModulos, bool>> where = x => (!string.IsNullOrWhiteSpace(codigo) ? x.Codigo.Contains(codigo) : 1 == 1)
                                && (!string.IsNullOrWhiteSpace(descripcion) ? x.Descripcion.Contains(descripcion) : 1 == 1)
                               && (origen == "P" ? x.CreadoPor == adCuenta : 1 == 1)
                                && (origen == "A" ? x.CreadoPor != adCuenta : 1 == 1);

            return await _context.MAccModulos.Where(where).ToListAsync();
        }

        private async Task<List<MAccModulos>> FindMAccModulos(string codigo, string descripcion)
        {
            Expression<Func<MAccModulos, bool>> where = x => (!string.IsNullOrWhiteSpace(codigo) ? x.Codigo.Contains(codigo) : 1 == 1)
                                && (!string.IsNullOrWhiteSpace(descripcion) ? x.Descripcion.Contains(descripcion) : 1 == 1);

            return await _context.MAccModulos.Where(where).ToListAsync();
        }
    }
}
