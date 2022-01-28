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
    public class ProgramasSLCController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public ProgramasSLCController(ApplicationDbContext context, IMapper mapper, IUserService userService)
        {
            _context = context;
            _mapper = mapper;
            _userService = userService;
        }

        // GET: api/ProgramasSLC
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MAccProgramas>>> GetMAccProgramas(string codigo, string descripcion)
        {
            return await FindMAccProgramas(codigo, descripcion);
        }

        // GET: api/ProgramasSLC/origen/{T}/programa/{mdl001}
        [HttpGet("origen/{origen}/programa/{programa}", Name = "ProgramasSLC")]
        public async Task<ActionResult<DetalleDTO<MAccProgramas>>> GetMAccProgramas(string origen, string programa, string codigo, string descripcion)
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

            var retorno = new DetalleDTO<MAccProgramas>();

            if (origen == "P")
            {
                retorno.datos = await FindMAccProgramas(codigo, descripcion, userConnected.AdCuenta, origen);
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
                retorno.datos = await FindMAccProgramas(codigo, descripcion, userConnected.AdCuenta, origen);
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

        // GET: api/ProgramasSLC/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MAccProgramas>> GetMAccProgramas(int id)
        {
            //Recupero y valido el usuario del token
            AccUsuarios userConnected = _userService.GetUserAllByToken(Request.Headers["Authorization"]);
            if (userConnected == null)
            {
                return StatusCode(419, "Error en autenticación de usuario.");
            }

            var mAccProgramas = await _context.MAccProgramas.FindAsync(id);

            if (mAccProgramas == null)
            {
                return NotFound();
            }

            return mAccProgramas;
        }

        // PUT: api/ProgramasSLC/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMAccProgramas(int id, MAccProgramas mAccProgramas)
        {
            if (id != mAccProgramas.Id)
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

            var mAccProgramasOriginal = await _context.MAccProgramas.FindAsync(id);

            if (mAccProgramasOriginal.CreadoPor != userConnected)
            {
                return StatusCode(420, "No se puede modificar un registro creado por otro usuario.");
            }

            mAccProgramas.CreadoPor = userConnected;
            mAccProgramas.CreadoEn = DateTime.Now;
            mAccProgramas.AutorizadoPor = null;
            mAccProgramas.AutorizadoEn = null;
            mAccProgramas.Modifica = mAccProgramasOriginal.Modifica;

            _context.Entry(mAccProgramas).State = EntityState.Modified;

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

        // POST: api/ProgramasSLC
        [HttpPost]
        public async Task<ActionResult<MAccProgramas>> PostMAccProgramas(MAccProgramas mAccProgramas)
        {
            //Recupero y valido el usuario del token
            string userConnected = _userService.GetUserByToken(Request.Headers["Authorization"]);
            if (userConnected == null)
            {
                return StatusCode(419, "Error en autenticación de usuario.");
            }

            mAccProgramas.Id = 0;
            mAccProgramas.CreadoPor = userConnected;
            mAccProgramas.CreadoEn = DateTime.Now;
            mAccProgramas.AutorizadoPor = null;
            mAccProgramas.AutorizadoEn = null;
            mAccProgramas.Modifica = "a";

            _context.MAccProgramas.Add(mAccProgramas);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMAccProgramas", new { id = mAccProgramas.Id }, mAccProgramas);
        }

        // DELETE: api/ProgramasSLC/autorizar/5
        [HttpDelete("autorizar/{id}")]
        public async Task<ActionResult<MAccProgramas>> DeleteMAccProgramasAutorizar(int id)
        {
            var mAccProgramas = await _context.MAccProgramas.FindAsync(id);

            if (mAccProgramas == null)
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

            if (mAccProgramas.CreadoPor == userConnected)
            {
                return StatusCode(420, "No se puede autorizar el registro. Debe ser autorizado con un usuario distinto.");
            }

            if (mAccProgramas.Modifica == "a")
            {
                var accProgramas= _mapper.Map<AccProgramas>(mAccProgramas);

                accProgramas.Id = 0;
                accProgramas.AutorizadoPor = userConnected;
                accProgramas.AutorizadoEn = DateTime.Now;

                _context.MAccProgramas.Remove(mAccProgramas);
                _context.AccProgramas.Add(accProgramas);

                //Guardo el Log
                var lAccProgramas = _mapper.Map<LAccProgramas>(mAccProgramas);
                lAccProgramas.Id = 0;

                _context.LAccProgramas.Add(lAccProgramas);

            }

            if (mAccProgramas.Modifica == "b")
            {
                var accProgramas = await _context.AccProgramas.FindAsync(mAccProgramas.IdOrigen);
                _context.MAccProgramas.Remove(mAccProgramas);
                _context.AccProgramas.Remove(accProgramas);

                //Guardo el Log
                var lAccProgramas = _mapper.Map<LAccProgramas>(mAccProgramas);
                lAccProgramas.Id = 0;

                _context.LAccProgramas.Add(lAccProgramas);
            }

            if (mAccProgramas.Modifica == "m")
            {
                var accProgramas = await _context.AccProgramas.FindAsync(id);
                accProgramas = _mapper.Map<AccProgramas>(mAccProgramas);

                accProgramas.AutorizadoPor = userConnected;
                accProgramas.AutorizadoEn = DateTime.Now;

                _context.MAccProgramas.Remove(mAccProgramas);
                _context.AccProgramas.Update(accProgramas);

                //Guardo el Log
                var lAccProgramas = _mapper.Map<LAccProgramas>(mAccProgramas);
                lAccProgramas.Id = 0;

                _context.LAccProgramas.Add(lAccProgramas);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.InnerException.Message);
            }

            return mAccProgramas;
        }

        // DELETE: api/ProgramasSLC/desautorizar/5
        [HttpDelete("desautorizar/{id}")]
        public async Task<ActionResult<MAccProgramas>> DeleteMAccProgramasDesautorizar(int id)
        {
            var mAccProgramas= await _context.MAccProgramas.FindAsync(id);

            if (mAccProgramas == null)
            {
                return NotFound();
            }

            //Recupero y valido el usuario del token
            string userConnected = _userService.GetUserByToken(Request.Headers["Authorization"]);
            if (userConnected == null)
            {
                return StatusCode(419, "Error en autenticación de usuario.");
            }

            if (mAccProgramas.CreadoPor == userConnected)
            {
                return StatusCode(420, "No se puede desautorizar el registro. Debe ser desautorizado con un usuario distinto.");
            }

            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            var nAccProgramas = _mapper.Map<NAccProgramas>(mAccProgramas);

            nAccProgramas.Id = 0;
            nAccProgramas.AutorizadoPor = userConnected;
            nAccProgramas.AutorizadoEn = DateTime.Now;
            nAccProgramas.Accionsql = nAccProgramas.Accionsql.ToUpper();

            _context.MAccProgramas.Remove(mAccProgramas);
            _context.NAccProgramas.Add(nAccProgramas);

            //Guardo el Log
            var lAccProgramas = _mapper.Map<LAccProgramas>(mAccProgramas);
            lAccProgramas.Id = 0;
            lAccProgramas.Accionsql = lAccProgramas.Accionsql.ToUpper();

            _context.LAccProgramas.Add(lAccProgramas);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.InnerException.Message);
            }

            return mAccProgramas;
        }

        // DELETE: api/ProgramasSLC/eliminar/5
        [HttpDelete("eliminar/{id}")]
        public async Task<ActionResult<MAccProgramas>> DeleteMAccProgramasEliminar(int id)
        {
            var mAccProgramas = await _context.MAccProgramas.FindAsync(id);

            if (mAccProgramas == null)
            {
                return NotFound();
            }

            //Recupero y valido el usuario del token
            string userConnected = _userService.GetUserByToken(Request.Headers["Authorization"]);
            if (userConnected == null)
            {
                return StatusCode(419, "Error en autenticación de usuario.");
            }

            if (mAccProgramas.CreadoPor != userConnected)
            {
                return StatusCode(420, "No se puede eliminar el registro, ya que fue creado por otro usuario.");
            }

            _context.MAccProgramas.Remove(mAccProgramas);
            await _context.SaveChangesAsync();

            return mAccProgramas;
        }

        private bool MAccProgramasExists(int id)
        {
            return _context.MAccProgramas.Any(e => e.Id == id);
        }

        private async Task<List<MAccProgramas>> FindMAccProgramas(string codigo, string descripcion, string adCuenta, string origen)
        {
            Expression<Func<MAccProgramas, bool>> where = x => (!string.IsNullOrWhiteSpace(codigo) ? x.Codigo.Contains(codigo) : 1 == 1)
                                && (!string.IsNullOrWhiteSpace(descripcion) ? x.Descripcion.Contains(descripcion) : 1 == 1)
                                && (origen == "P" ? x.CreadoPor == adCuenta : 1 == 1)
                                && (origen == "A" ? x.CreadoPor != adCuenta : 1 == 1);

            return await _context.MAccProgramas.Where(where).ToListAsync();
        }

        private async Task<List<MAccProgramas>> FindMAccProgramas(string codigo, string descripcion)
        {
            Expression<Func<MAccProgramas, bool>> where = x => (!string.IsNullOrWhiteSpace(codigo) ? x.Codigo.Contains(codigo) : 1 == 1)
                                && (!string.IsNullOrWhiteSpace(descripcion) ? x.Descripcion.Contains(descripcion) : 1 == 1);

            return await _context.MAccProgramas.Where(where).ToListAsync();
        }
    }
}
