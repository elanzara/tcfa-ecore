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
    public class UsuariosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UsuariosController(ApplicationDbContext context, IMapper mapper, IUserService userService)
        {
            _context = context;
            _mapper = mapper;
            _userService = userService;
        }

        //GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAccUsuarios(string nombres, string apellido, string adCuenta, int? idAccPerfil)
        {
            List<AccUsuarios> lista = await FindAccUsuarios(nombres, apellido, adCuenta, idAccPerfil);
            return _mapper.Map<List<User>>(lista);
        }

        // GET: api/Usuarios/origen/{T}/programa/{mdl001}
        [HttpGet("origen/{origen}/programa/{programa}", Name = "Usuarios")]
        public async Task<ActionResult<DetalleDTO<AccUsuarios>>> GetAccUsuarios(string origen, string programa, string nombres, string apellido, string adCuenta, int? idAccPerfil)
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

            var retorno = new DetalleDTO<AccUsuarios>();

            if (origen == "T")
            {
                retorno.datos = await FindAccUsuarios(nombres, apellido, adCuenta, idAccPerfil);

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

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AccUsuarios>> GetAccUsuarios(int id)
        {
            //Recupero y valido el usuario del token
            AccUsuarios userConnected = _userService.GetUserAllByToken(Request.Headers["Authorization"]);
            if (userConnected == null)
            {
                return StatusCode(419, "Error en autenticación de usuario.");
            }

            var accUsuarios = await _context.AccUsuarios.FindAsync(id);

            if (accUsuarios== null)
            {
                return NotFound();
            }

            return accUsuarios;
        }

        // PUT: api/Usuarios/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccUsuarios(int id, AccUsuarios accUsuarios)
        {
            if (id != accUsuarios.Id)
            {
                return BadRequest();
            }

            //Recupero y valido el usuario del token
            string userConnected = _userService.GetUserByToken(Request.Headers["Authorization"]);
            if (userConnected == null)
            {
                return StatusCode(419, "Error en autenticación de usuario.");
            }

            if (!MAccUsuarioExists(id))
            {
                var mAccUsuarios = _mapper.Map<MAccUsuarios>(accUsuarios);
                mAccUsuarios.Id = 0;
                mAccUsuarios.Modifica = "m";
                mAccUsuarios.CreadoPor = userConnected;
                mAccUsuarios.CreadoEn = DateTime.Now;
                mAccUsuarios.AutorizadoPor = null;
                mAccUsuarios.AutorizadoEn = null;

                _context.MAccUsuarios.Add(mAccUsuarios);
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
                if (!AccUsuariosExists(id))
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

        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AccUsuarios>> DeleteAccUsuarios(int id)
        {
            var accUsuarios = await _context.AccUsuarios.FindAsync(id);

            if (accUsuarios == null)
            {
                return NotFound();
            }

            //Recupero y valido el usuario del token
            string userConnected = _userService.GetUserByToken(Request.Headers["Authorization"]);
            if (userConnected == null)
            {
                return StatusCode(419, "Error en autenticación de usuario.");
            }

            if (MAccUsuarioExists(id))
            {
                return StatusCode(420, "Ya existe una modificación pendiente del mismo registro.");
            }

            var mAccUsuarios = _mapper.Map<MAccUsuarios>(accUsuarios);
            mAccUsuarios.Id = 0;
            mAccUsuarios.Modifica = "b";
            mAccUsuarios.CreadoPor = userConnected;
            mAccUsuarios.CreadoEn = DateTime.Now;
            mAccUsuarios.AutorizadoPor = null;
            mAccUsuarios.AutorizadoEn = null;

            _context.MAccUsuarios.Add(mAccUsuarios);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.InnerException.Message);
            }

            return accUsuarios;
        }

        private bool AccUsuariosExists(int id)
        {
            return _context.AccUsuarios.Any(e => e.Id == id);
        }
        private bool MAccUsuarioExists(int id)
        {
            return _context.MAccUsuarios.Any(e => e.IdOrigen == id);
        }

        private async Task<List<AccUsuarios>> FindAccUsuarios(string nombres, string apellido, string adCuenta, int? idAccPerfil)
        {
            Expression<Func<AccUsuarios, bool>> where = x => (!string.IsNullOrWhiteSpace(nombres) ? x.Nombres.Contains(nombres) : 1 == 1)
                                && (!string.IsNullOrWhiteSpace(apellido) ? x.Apellido.Contains(apellido) : 1 == 1)
                                && (!string.IsNullOrWhiteSpace(adCuenta) ? x.AdCuenta.Contains(adCuenta) : 1 == 1)
                                && (idAccPerfil != null ? x.IdAccPerfil.Equals(idAccPerfil) : 1 == 1);

            return await _context.AccUsuarios.Where(where).ToListAsync();
        }
    }
}
