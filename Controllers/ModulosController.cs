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
    public class ModulosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public ModulosController(ApplicationDbContext context, IMapper mapper, IUserService userService)
        {
            _context = context;
            _mapper = mapper;
            _userService = userService;
        }

        //GET: api/Modulos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ModuloResDTO>>> GetAccModulos(string codigo, string descripcion)
        {
            List<AccModulos> lista = await FindAccModulos(codigo, descripcion);
            return _mapper.Map<List<ModuloResDTO>>(lista);
        }

        // GET: api/Modulos/origen/{T}/programa/{mdl001}
        [HttpGet("origen/{origen}/programa/{programa}", Name = "Modulos")]
        public async Task<ActionResult<DetalleDTO<AccModulos>>> GetAccModulos(string origen, string programa, string codigo, string descripcion)
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

            var retorno = new DetalleDTO<AccModulos>();

            if (origen == "T")
            {
                retorno.datos = await FindAccModulos(codigo, descripcion);

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

        // GET: api/Modulos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AccModulos>> GetAccModulos(int id)
        {
            //Recupero y valido el usuario del token
            AccUsuarios userConnected = _userService.GetUserAllByToken(Request.Headers["Authorization"]);
            if (userConnected == null)
            {
                return StatusCode(419, "Error en autenticación de usuario.");
            }

            //var accModulos = await _context.AccModulos.Include(b => b.AccGruposXPerfil).ThenInclude(c => c.IdAccPerfilNavigation).FirstOrDefaultAsync(i => i.Id == id);
            var accModulos = await _context.AccModulos.Include(b => b.AccProgramasXModulos).ThenInclude(c => c.IdAccProgramaNavigation).Include(f => f.InverseIdAccModuloNavigation).ThenInclude(g => g.IdAccModuloNavigation).FirstOrDefaultAsync(i => i.Id == id);

            if (accModulos == null)
            {
                return NotFound();
            }

            return accModulos;
        }

        // PUT: api/Modulos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccModulos(int id, AccModulos accModulos)
        {
            if (id != accModulos.Id)
            {
                return BadRequest();
            }

            //Recupero y valido el usuario del token
            string userConnected = _userService.GetUserByToken(Request.Headers["Authorization"]);
            if (userConnected == null)
            {
                return StatusCode(419, "Error en autenticación de usuario.");
            }

            if (!MAccModulosExists(id))
            {
                var mAccModulos = _mapper.Map<MAccModulos>(accModulos);
                mAccModulos.Id = 0;
                mAccModulos.Modifica = "m";
                mAccModulos.CreadoPor = userConnected;
                mAccModulos.CreadoEn = DateTime.Now;
                mAccModulos.AutorizadoPor = null;
                mAccModulos.AutorizadoEn = null;

                _context.MAccModulos.Add(mAccModulos);
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
                if (!AccModulosExists(id))
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

        // DELETE: api/Modulos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AccModulos>> DeleteAccModulos(int id)
        {
            var accModulos = await _context.AccModulos.FindAsync(id);

            if (accModulos == null)
            {
                return NotFound();
            }

            //Recupero y valido el usuario del token
            string userConnected = _userService.GetUserByToken(Request.Headers["Authorization"]);
            if (userConnected == null)
            {
                return StatusCode(419, "Error en autenticación de usuario.");
            }

            if (MAccModulosExists(id))
            {
                return StatusCode(420, "Ya existe una modificación pendiente del mismo registro.");
            }

            var mAccModulos = _mapper.Map<MAccModulos>(accModulos);
            mAccModulos.Id = 0;
            mAccModulos.Modifica = "b";
            mAccModulos.CreadoPor = userConnected;
            mAccModulos.CreadoEn = DateTime.Now;
            mAccModulos.AutorizadoPor = null;
            mAccModulos.AutorizadoEn = null;

            _context.MAccModulos.Add(mAccModulos);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.InnerException.Message);
            }

            return accModulos;
        }

        private bool AccModulosExists(int id)
        {
            return _context.AccModulos.Any(e => e.Id == id);
        }

        private bool MAccModulosExists(int id)
        {
            return _context.MAccModulos.Any(e => e.IdOrigen == id);
        }

        private async Task<List<AccModulos>> FindAccModulos(string codigo, string descripcion)
        {
            Expression<Func<AccModulos, bool>> where = x => (!string.IsNullOrWhiteSpace(codigo) ? x.Codigo.Contains(codigo) : 1 == 1)
                                && (!string.IsNullOrWhiteSpace(descripcion) ? x.Descripcion.Contains(descripcion) : 1 == 1);

            return await _context.AccModulos.Where(where).ToListAsync();
        }
    }
}

