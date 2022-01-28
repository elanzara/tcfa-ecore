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
    public class GruposXPerfilController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public GruposXPerfilController(ApplicationDbContext context, IMapper mapper, IUserService userService)
        {
            _context = context;
            _mapper = mapper;
            _userService = userService;
        }

        // GET: api/GruposXPerfil/perspectiva/{perspectiva}
        [HttpGet("perspectiva/{perspectiva}", Name = "GetAccGruposXPerfilPerspectiva")]
        public async Task<ActionResult<IEnumerable<PerspectivaDTO<BasicDTO, BasicDTO>>>> GetAccGruposXPerfilPerspectiva(string perspectiva, string codigo, string descripcion)
        {
            if (! (perspectiva.Equals("perfiles") || perspectiva.Equals("grupos")))
            {
                return StatusCode(423, "Perspectiva debe ser 'perfiles' o 'grupos'.");
            }

            return await GruposXPerfilPerspectiva(perspectiva, codigo, descripcion);
        }

        // GET: api/GruposXPerfil/origen/{T}/programa/{gxp001}/perspectiva/{grupos}
        [HttpGet("origen/{origen}/programa/{programa}/perspectiva/{perspectiva}", Name = "GetAccGruposXPerfilOrigenProgramaPerspectiva")]
        public async Task<ActionResult<DetalleDTO<PerspectivaDTO<BasicDTO, BasicDTO>>>> GetAccGruposXPerfilOrigenProgramaPerspectiva(string origen, string programa, string perspectiva, string codigo, string descripcion)
        {
            //Recupero y valido el usuario del token
            AccUsuarios userConnected = _userService.GetUserAllByToken(Request.Headers["Authorization"]);
            if (userConnected == null)
            {
                return StatusCode(419, "Error en autenticación de usuario.");
            }

            if (!(perspectiva.Equals("perfiles") || perspectiva.Equals("grupos")))
            {
                return StatusCode(423, "Perspectiva debe ser 'perfiles' o 'grupos'.");
            }

            var accPrograma = await _context.AccProgramas.FirstOrDefaultAsync(x => x.Codigo == programa);

            if (accPrograma == null)
            {
                return NotFound();
            }

            var retorno = new DetalleDTO<PerspectivaDTO<BasicDTO, BasicDTO>>();

            if (origen == "T")
            {
                retorno.datos = await GruposXPerfilPerspectiva(perspectiva, codigo, descripcion);

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

        // GET: api/GruposXPerfil/origen/{T}/programa/{mdl001}
        [HttpGet("origen/{origen}/programa/{programa}", Name = "GetAccGruposXPerfil")]
        public async Task<ActionResult<DetalleDTO<PerspectivaDTO<BasicDTO, BasicDTO>>>> GetAccGruposXPerfil(string origen, string programa)
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

            var retorno = new DetalleDTO<PerspectivaDTO<BasicDTO, BasicDTO>>();

            if (origen == "T")
            {
                retorno.datos = await GruposXPerfilPlano();

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

        // GET: api/GruposXPerfil/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GrupoXPerfilDTO>> GetAccGruposXPerfil(int id)
        {
            //Recupero y valido el usuario del token
            AccUsuarios userConnected = _userService.GetUserAllByToken(Request.Headers["Authorization"]);
            if (userConnected == null)
            {
                return StatusCode(419, "Error en autenticación de usuario.");
            }

            var accGruposXPerfil = _mapper.Map<GrupoXPerfilDTO>(await _context.AccGruposXPerfil.Include(c => c.IdAccGrupoNavigation).Include(c => c.IdAccPerfilNavigation).FirstAsync(g => g.Id == id));

            if (accGruposXPerfil == null)
            {
                return NotFound();
            }

            return accGruposXPerfil;
        }

        // GET: api/GruposXPerfil/5/perspectiva/grupos
        [HttpGet("{id}/perspectiva/{perspectiva}", Name = "GetAccGruposXPerfilPerspectivaSingle")]
        public async Task<ActionResult<PerspectivaDTO<BasicDTO, BasicDTO>>> GetAccGruposXPerfilPerspectivaSingle(int id, string perspectiva)
        {
            //Recupero y valido el usuario del token
            AccUsuarios userConnected = _userService.GetUserAllByToken(Request.Headers["Authorization"]);
            if (userConnected == null)
            {
                return StatusCode(419, "Error en autenticación de usuario.");
            }

            if (!(perspectiva.Equals("perfiles") || perspectiva.Equals("grupos")))
            {
                return StatusCode(423, "Perspectiva debe ser 'perfiles' o 'grupos'.");
            }

            var accGruposXPerfil = await GruposXPerfilPerspectiva(id, perspectiva);

            if (accGruposXPerfil == null)
            {
                return NotFound();
            }

            return accGruposXPerfil;
        }

        // PUT: api/GruposXPerfil/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccGruposXPerfil(int id, AccGruposXPerfil accGruposXPerfil)
        {
            if (id != accGruposXPerfil.Id)
            {
                return BadRequest();
            }

            //Recupero y valido el usuario del token
            string userConnected = _userService.GetUserByToken(Request.Headers["Authorization"]);
            if (userConnected == null)
            {
                return StatusCode(419, "Error en autenticación de usuario.");
            }

            if (!MAccGruposXPerfilExists(id))
            {
                var mAccGruposXPerfil = _mapper.Map<MAccGruposXPerfil>(accGruposXPerfil);
                mAccGruposXPerfil.Id = 0;
                mAccGruposXPerfil.Modifica = "m";
                mAccGruposXPerfil.CreadoPor = userConnected;
                mAccGruposXPerfil.CreadoEn = DateTime.Now;
                mAccGruposXPerfil.AutorizadoPor = null;
                mAccGruposXPerfil.AutorizadoEn = null;

                _context.MAccGruposXPerfil.Add(mAccGruposXPerfil);
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
                if (!AccGruposXPerfilExists(id))
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

        // POST: api/GruposXPerfil
        [HttpPost]
        public async Task<ActionResult<AccGruposXPerfil>> PostAccGruposXPerfil(AccGruposXPerfil accGruposXPerfil)
        {
            _context.AccGruposXPerfil.Add(accGruposXPerfil);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAccGruposXPerfil", new { id = accGruposXPerfil.Id }, accGruposXPerfil);
        }

        // DELETE: api/GruposXPerfil/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AccGruposXPerfil>> DeleteAccGruposXPerfil(int id)
        {
            var accGruposXPerfil = await _context.AccGruposXPerfil.FindAsync(id);

            if (accGruposXPerfil == null)
            {
                return NotFound();
            }

            //Recupero y valido el usuario del token
            string userConnected = _userService.GetUserByToken(Request.Headers["Authorization"]);
            if (userConnected == null)
            {
                return StatusCode(419, "Error en autenticación de usuario.");
            }

            if (MAccGruposXPerfilExists(id))
            {
                return StatusCode(420, "Ya existe una modificación pendiente del mismo registro.");
            }

            var mAccGruposXperfil = _mapper.Map<MAccGruposXPerfil>(accGruposXPerfil);
            mAccGruposXperfil.Id = 0;
            mAccGruposXperfil.Modifica = "b";
            mAccGruposXperfil.CreadoPor = userConnected;
            mAccGruposXperfil.CreadoEn = DateTime.Now;
            mAccGruposXperfil.AutorizadoPor = null;
            mAccGruposXperfil.AutorizadoEn = null;

            _context.MAccGruposXPerfil.Add(mAccGruposXperfil);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.InnerException.Message);
            }

            return accGruposXPerfil;
        }

        private bool AccGruposXPerfilExists(int id)
        {
            return _context.AccGruposXPerfil.Any(e => e.Id == id);
        }

        private bool MAccGruposXPerfilExists(int id)
        {
            return _context.MAccGruposXPerfil.Any(e => e.Id == id);
        }

        public async Task<List<PerspectivaDTO<BasicDTO, BasicDTO>>> GruposXPerfilPerspectiva(string perspectiva, string codigo, string descripcion)
        {
            switch (perspectiva)
            {
                case "perfiles":
                    Expression<Func<AccGruposXPerfil, bool>> wherePerfil = x => (!string.IsNullOrWhiteSpace(codigo) ? x.IdAccPerfilNavigation.Codigo.Contains(codigo) : 1 == 1)
                                && (!string.IsNullOrWhiteSpace(descripcion) ? x.IdAccPerfilNavigation.Descripcion.Contains(descripcion) : 1 == 1);

                    var gxpPerfiles = await _context.AccGruposXPerfil
                                            .Include(c => c.IdAccGrupoNavigation)
                                            .Include(c => c.IdAccPerfilNavigation)
                                                .ThenInclude(c => c.AccGruposXPerfil)
                                                .ThenInclude(p => p.IdAccGrupoNavigation)
                                            .Where(wherePerfil)
                                            .GroupBy(g => g.IdAccPerfil)
                                            .Select(g => g.First())
                                            .ToListAsync();

                    List<PerspectivaDTO<BasicDTO, BasicDTO>> resultPerfiles = new List<PerspectivaDTO<BasicDTO, BasicDTO>>();

                    gxpPerfiles.ForEach(p =>
                    {
                        List<BasicDTO> grupos = new List<BasicDTO>();
                        p.IdAccPerfilNavigation.AccGruposXPerfil.ToList().ForEach(gr =>
                        {
                            grupos.Add(_mapper.Map<GrupoDTO>(gr.IdAccGrupoNavigation));
                        });
                        PerspectivaDTO<BasicDTO, BasicDTO> gxpDTO = new PerspectivaDTO<BasicDTO, BasicDTO>();
                        gxpDTO.Perspectiva = _mapper.Map<PerfilDTO>(p.IdAccPerfilNavigation);
                        gxpDTO.Relacionados = grupos;
                        resultPerfiles.Add(gxpDTO);
                    });
                    return resultPerfiles;
                case "grupos":
                    Expression<Func<AccGruposXPerfil, bool>> whereGrupo = x => (!string.IsNullOrWhiteSpace(codigo) ? x.IdAccGrupoNavigation.Codigo.Contains(codigo) : 1 == 1)
                                && (!string.IsNullOrWhiteSpace(descripcion) ? x.IdAccGrupoNavigation.Descripcion.Contains(descripcion) : 1 == 1);

                    var gxpGrupos = await _context.AccGruposXPerfil
                                            .Include(c => c.IdAccPerfilNavigation)
                                            .Include(c => c.IdAccGrupoNavigation)
                                                .ThenInclude(c => c.AccGruposXPerfil)
                                                .ThenInclude(g => g.IdAccPerfilNavigation)
                                            .Where(whereGrupo)
                                            .GroupBy(g => g.IdAccGrupo)
                                            .Select(g => g.First())
                                            .ToListAsync();

                    List<PerspectivaDTO<BasicDTO, BasicDTO>> resultGrupos = new List<PerspectivaDTO<BasicDTO, BasicDTO>>();

                    gxpGrupos.ForEach(g =>
                    {
                        List<BasicDTO> perfiles = new List<BasicDTO>();
                        g.IdAccGrupoNavigation.AccGruposXPerfil.ToList().ForEach(pr =>
                        {
                            perfiles.Add(_mapper.Map<PerfilDTO>(pr.IdAccPerfilNavigation));
                        });
                        PerspectivaDTO<BasicDTO, BasicDTO> gxpDTO = new PerspectivaDTO<BasicDTO, BasicDTO>();
                        gxpDTO.Perspectiva = _mapper.Map<GrupoDTO>(g.IdAccGrupoNavigation); ;
                        gxpDTO.Relacionados = perfiles;
                        resultGrupos.Add(gxpDTO);
                    });
                    return resultGrupos;
                default:
                    return null;
            }
        }

        public async Task<PerspectivaDTO<BasicDTO, BasicDTO>> GruposXPerfilPerspectiva(int id, string perspectiva)
        {
            switch (perspectiva)
            {
                case "perfiles":
                    var perfil = await _context.AccGruposXPerfil
                                                .Include(c => c.IdAccGrupoNavigation)
                                                .Include(c => c.IdAccPerfilNavigation)
                                                    .ThenInclude(c => c.AccGruposXPerfil)
                                                    .ThenInclude(g => g.IdAccGrupoNavigation)
                                                .Where(g => g.Id == id)
                                                .GroupBy(g => g.IdAccPerfil)
                                                .Select(g => g.First())
                                                .ToListAsync();
                    if (perfil.Count() != 1)
                    {
                        return null;
                    }

                    List<BasicDTO> grupos = new List<BasicDTO>();
                    perfil[0].IdAccPerfilNavigation.AccGruposXPerfil.ToList().ForEach(gr =>
                    {
                        grupos.Add(_mapper.Map<GrupoDTO>(gr.IdAccGrupoNavigation));
                    });
                    PerspectivaDTO<BasicDTO, BasicDTO> perspectivaPerfil = new PerspectivaDTO<BasicDTO, BasicDTO>();
                    perspectivaPerfil.Perspectiva = _mapper.Map<PerfilDTO>(perfil[0].IdAccPerfilNavigation);
                    perspectivaPerfil.Relacionados = grupos;
                    return perspectivaPerfil;

                case "grupos":
                    var grupo = await _context.AccGruposXPerfil
                                            .Include(c => c.IdAccPerfilNavigation)
                                            .Include(c => c.IdAccGrupoNavigation)
                                                .ThenInclude(c => c.AccGruposXPerfil)
                                                .ThenInclude(g => g.IdAccPerfilNavigation)
                                            .Where(g => g.Id == id)
                                            .GroupBy(g => g.IdAccGrupo)
                                            .Select(g => g.First())
                                            .ToListAsync();
                    if (grupo.Count() != 1)
                    {
                        return null;
                    }

                    List<BasicDTO> perfiles = new List<BasicDTO>();
                    grupo[0].IdAccGrupoNavigation.AccGruposXPerfil.ToList().ForEach(pr =>
                    {
                        perfiles.Add(_mapper.Map<PerfilDTO>(pr.IdAccPerfilNavigation));
                    });
                    PerspectivaDTO<BasicDTO, BasicDTO> perspectivaGrupo = new PerspectivaDTO<BasicDTO, BasicDTO>();
                    perspectivaGrupo.Perspectiva = _mapper.Map<GrupoDTO>(grupo[0].IdAccGrupoNavigation); ;
                    perspectivaGrupo.Relacionados = perfiles;
                    return perspectivaGrupo;

                default:
                    return null;
            }
        }

        public async Task<List<PerspectivaDTO<BasicDTO, BasicDTO>>> GruposXPerfilPlano()
        {
            var grupoXPerfil = await _context.AccGruposXPerfil
                                    .Include(c => c.IdAccPerfilNavigation)
                                    .Include(c => c.IdAccGrupoNavigation)
                                    .ToListAsync();

            List<PerspectivaDTO<BasicDTO, BasicDTO>> perspectivas = new List<PerspectivaDTO<BasicDTO, BasicDTO>>();

            grupoXPerfil.ForEach(gxp =>
            {
                PerspectivaDTO<BasicDTO, BasicDTO> perspectivaDTO = new PerspectivaDTO<BasicDTO, BasicDTO>();
                var perspectiva = new BasicDTO();

                perspectiva.Id = gxp.Id;
                perspectiva.Codigo = gxp.IdAccGrupoNavigation.Codigo + " / " + gxp.IdAccPerfilNavigation.Codigo;
                perspectiva.Descripcion = gxp.IdAccGrupoNavigation.Descripcion + " / " + gxp.IdAccPerfilNavigation.Descripcion;
                perspectiva.Icono = gxp.IdAccGrupoNavigation.Icono;

                perspectivaDTO.Perspectiva = perspectiva;
                perspectivaDTO.Relacionados = new List<BasicDTO>();

                perspectivas.Add(perspectivaDTO);
            });

            return perspectivas;
        }
    }
}
