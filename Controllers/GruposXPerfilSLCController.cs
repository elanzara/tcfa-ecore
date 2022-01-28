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
    public class GruposXPerfilSLCController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public GruposXPerfilSLCController(ApplicationDbContext context, IMapper mapper, IUserService userService)
        {
            _context = context;
            _mapper = mapper;
            _userService = userService;
        }

        // GET: api/GruposXPerfilSLC/perspectiva/{perspectiva}
        [HttpGet("perspectiva/{perspectiva}", Name = "MAccGruposXPerfilPerspectiva")]
        public async Task<ActionResult<IEnumerable<PerspectivaDTO<BasicDTO, BasicDTO>>>> GetMAccGruposXPerfilPerspectiva(string perspectiva, string codigo, string descripcion)
        {
            if (!(perspectiva.Equals("perfiles") || perspectiva.Equals("grupos")))
            {
                return StatusCode(423, "Perspectiva debe ser 'perfiles' o 'grupos'.");
            }

            return await MAccGruposXPerfilPerspectiva(perspectiva, codigo, descripcion);
        }

        // GET: api/GruposXPerfilSLC/origen/{T}/programa/{mdl001}
        [HttpGet("origen/{origen}/programa/{programa}", Name = "MAccGruposXPerfil")]
        public async Task<ActionResult<DetalleDTO<PerspectivaDTO<BasicDTO, BasicDTO>>>> GetMAccGruposXPerfil(string origen, string programa)
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

            if (origen == "P")
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
            else if (origen == "A")
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

        // GET: api/GruposXPerfilSLC/origen/{T}/programa/{gxp001}/perspectiva/{grupos}
        [HttpGet("origen/{origen}/programa/{programa}/perspectiva/{perspectiva}", Name = "MAccGruposXPerfilOrigenProgramaPerspectiva")]
        public async Task<ActionResult<DetalleDTO<PerspectivaDTO<BasicDTO, BasicDTO>>>> GetMAccGruposXPerfilPerspectiva(string origen, string programa, string perspectiva, string codigo, string descripcion)
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

            if (origen == "P")
            {
                retorno.datos = await MAccGruposXPerfilPerspectiva(perspectiva, codigo, descripcion);

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
                retorno.datos = await MAccGruposXPerfilPerspectiva(perspectiva, codigo, descripcion);

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

        // GET: api/GruposXPerfilSLC/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GrupoXPerfilDTO>> GetMAccGruposXPerfil(int id)
        {
            //Recupero y valido el usuario del token
            AccUsuarios userConnected = _userService.GetUserAllByToken(Request.Headers["Authorization"]);
            if (userConnected == null)
            {
                return StatusCode(419, "Error en autenticación de usuario.");
            }

            //var mAccGruposXPerfil = await _context.MAccGruposXPerfil.Include(c => c.IdAccGrupoNavigation).ThenInclude(d => d.MaccGruposXPerfilIdAccGrupoNavigation).FirstOrDefaultAsync(c => c.Id == id);

            var mAccGruposXPerfil = _mapper.Map<GrupoXPerfilDTO>(await _context.MAccGruposXPerfil.Include(c => c.IdAccGrupoNavigation).Include(c => c.IdAccPerfilNavigation).FirstOrDefaultAsync(c => c.Id == id));

            if (mAccGruposXPerfil == null)
            {
                return NotFound();
            }

            return mAccGruposXPerfil;
        }

        // GET: api/GruposXPerfilSLC/5/perspectiva/grupos
        [HttpGet("{id}/perspectiva/{perspectiva}", Name = "GetMAccGruposXPerfilPerspectivaSingle")]
        public async Task<ActionResult<PerspectivaDTO<BasicDTO, BasicDTO>>> GetMAccGruposXPerfilPerspectivaSingle(int id, string perspectiva)
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

            var mAccGruposXPerfil = await GruposXPerfilPerspectiva(id, perspectiva);

            if (mAccGruposXPerfil == null)
            {
                return NotFound();
            }

            return mAccGruposXPerfil;
        }

        // PUT: api/GruposXPerfilSLC/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMAccGruposXPerfil(int id, MAccGruposXPerfil mAccGruposXPerfil)
        {
            if (id != mAccGruposXPerfil.Id)
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

            var mAccGruposXPerfilOriginal = await _context.MAccGruposXPerfil.FindAsync(id);

            if (mAccGruposXPerfilOriginal.CreadoPor != userConnected)
            {
                return StatusCode(420, "No se puede modificar un registro creado por otro usuario.");
            }

            mAccGruposXPerfil.CreadoPor = userConnected;
            mAccGruposXPerfil.CreadoEn = DateTime.Now;
            mAccGruposXPerfil.AutorizadoPor = null;
            mAccGruposXPerfil.AutorizadoEn = null;
            mAccGruposXPerfil.Modifica = mAccGruposXPerfilOriginal.Modifica;

            _context.Entry(mAccGruposXPerfil).State = EntityState.Modified;

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

        // POST: api/GruposXPerfilSLC
        [HttpPost]
        public async Task<ActionResult<MAccGruposXPerfil>> PostMAccGruposXPerfil(MAccGruposXPerfil mAccGruposXPerfil)
        {
            //Recupero y valido el usuario del token
            string userConnected = _userService.GetUserByToken(Request.Headers["Authorization"]);
            if (userConnected == null)
            {
                return StatusCode(419, "Error en autenticación de usuario.");
            }

            mAccGruposXPerfil.Id = 0;
            mAccGruposXPerfil.CreadoPor = userConnected;
            mAccGruposXPerfil.CreadoEn = DateTime.Now;
            mAccGruposXPerfil.AutorizadoPor = null;
            mAccGruposXPerfil.AutorizadoEn = null;
            mAccGruposXPerfil.Modifica = "a";

            _context.MAccGruposXPerfil.Add(mAccGruposXPerfil);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMAccGruposXPerfil", new { id = mAccGruposXPerfil.Id }, mAccGruposXPerfil);
        }

        // DELETE: api/GruposXPerfilSLC/autorizar/5
        [HttpDelete("autorizar/{id}")]
        public async Task<ActionResult<MAccGruposXPerfil>> DeleteMAccGruposXPerfilAutorizar(int id)
        {
            var mAccGruposXPerfil = await _context.MAccGruposXPerfil.FindAsync(id);

            if (mAccGruposXPerfil == null)
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

            if (mAccGruposXPerfil.CreadoPor == userConnected)
            {
                return StatusCode(420, "No se puede autorizar el registro. Debe ser autorizado con un usuario distinto.");
            }

            if (mAccGruposXPerfil.Modifica == "a")
            {
                var accGruposXPerfil = _mapper.Map<AccGruposXPerfil>(mAccGruposXPerfil);

                accGruposXPerfil.Id = 0;
                accGruposXPerfil.AutorizadoPor = userConnected;
                accGruposXPerfil.AutorizadoEn = DateTime.Now;

                _context.MAccGruposXPerfil.Remove(mAccGruposXPerfil);
                _context.AccGruposXPerfil.Add(accGruposXPerfil);

                //Guardo el Log
                var lAccGruposXPerfil = _mapper.Map<LAccGruposXPerfil>(mAccGruposXPerfil);
                lAccGruposXPerfil.Id = 0;

                _context.LAccGruposXPerfil.Add(lAccGruposXPerfil);

            }

            if (mAccGruposXPerfil.Modifica == "b")
            {
                var accGruposXPerfil = await _context.AccGruposXPerfil.FindAsync(mAccGruposXPerfil.IdOrigen);
                _context.MAccGruposXPerfil.Remove(mAccGruposXPerfil);
                _context.AccGruposXPerfil.Remove(accGruposXPerfil);

                //Guardo el Log
                var lAccGruposXPerfil = _mapper.Map<LAccGruposXPerfil>(mAccGruposXPerfil);
                lAccGruposXPerfil.Id = 0;

                _context.LAccGruposXPerfil.Add(lAccGruposXPerfil);
            }

            if (mAccGruposXPerfil.Modifica == "m")
            {
                var accGruposXPerfil = await _context.AccGruposXPerfil.FindAsync(id);
                accGruposXPerfil = _mapper.Map<AccGruposXPerfil>(mAccGruposXPerfil);

                accGruposXPerfil.AutorizadoPor = userConnected;
                accGruposXPerfil.AutorizadoEn = DateTime.Now;

                _context.MAccGruposXPerfil.Remove(mAccGruposXPerfil);
                _context.AccGruposXPerfil.Update(accGruposXPerfil);

                //Guardo el Log
                var lAccGruposXPerfil = _mapper.Map<LAccGruposXPerfil>(mAccGruposXPerfil);
                lAccGruposXPerfil.Id = 0;

                _context.LAccGruposXPerfil.Add(lAccGruposXPerfil);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.InnerException.Message);
            }

            return mAccGruposXPerfil;
        }

        // DELETE: api/GruposXPerfilSLC/desautorizar/5
        [HttpDelete("desautorizar/{id}")]
        public async Task<ActionResult<MAccGruposXPerfil>> DeleteMAccGruposXPerfilDesautorizar(int id)
        {
            var mAccGruposXPerfil = await _context.MAccGruposXPerfil.FindAsync(id);

            if (mAccGruposXPerfil == null)
            {
                return NotFound();
            }

            //Recupero y valido el usuario del token
            string userConnected = _userService.GetUserByToken(Request.Headers["Authorization"]);
            if (userConnected == null)
            {
                return StatusCode(419, "Error en autenticación de usuario.");
            }

            if (mAccGruposXPerfil.CreadoPor == userConnected)
            {
                return StatusCode(420, "No se puede desautorizar el registro. Debe ser desautorizado con un usuario distinto.");
            }

            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            var nAccGruposXPerfil = _mapper.Map<NAccGruposXPerfil>(mAccGruposXPerfil);

            nAccGruposXPerfil.Id = 0;
            nAccGruposXPerfil.AutorizadoPor = userConnected;
            nAccGruposXPerfil.AutorizadoEn = DateTime.Now;
            nAccGruposXPerfil.Accionsql = nAccGruposXPerfil.Accionsql.ToUpper();

            _context.MAccGruposXPerfil.Remove(mAccGruposXPerfil);
            _context.NAccGruposXPerfil.Add(nAccGruposXPerfil);

            //Guardo el Log
            var lAccGruposXPerfil = _mapper.Map<LAccGruposXPerfil>(mAccGruposXPerfil);
            lAccGruposXPerfil.Id = 0;
            lAccGruposXPerfil.Accionsql = lAccGruposXPerfil.Accionsql.ToUpper();

            _context.LAccGruposXPerfil.Add(lAccGruposXPerfil);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.InnerException.Message);
            }

            return mAccGruposXPerfil;
        }

        // DELETE: api/GruposXPerfilSLC/eliminar/5
        [HttpDelete("eliminar/{id}")]
        public async Task<ActionResult<MAccGruposXPerfil>> DeleteMAccGruposXPerfilEliminar(int id)
        {
            var mAccGruposXPerfil = await _context.MAccGruposXPerfil.FindAsync(id);

            if (mAccGruposXPerfil == null)
            {
                return NotFound();
            }

            //Recupero y valido el usuario del token
            string userConnected = _userService.GetUserByToken(Request.Headers["Authorization"]);
            if (userConnected == null)
            {
                return StatusCode(419, "Error en autenticación de usuario.");
            }

            if (mAccGruposXPerfil.CreadoPor != userConnected)
            {
                return StatusCode(420, "No se puede eliminar el registro, ya que fue creado por otro usuario.");
            }

            _context.MAccGruposXPerfil.Remove(mAccGruposXPerfil);
            await _context.SaveChangesAsync();

            return mAccGruposXPerfil;
        }

        private bool MAccGruposXPerfilExists(int id)
        {
            return _context.MAccGruposXPerfil.Any(e => e.Id == id);
        }

        public async Task<List<PerspectivaDTO<BasicDTO, BasicDTO>>> MAccGruposXPerfilPerspectiva(string perspectiva, string codigo, string descripcion)
        {
            switch (perspectiva)
            {
                case "perfiles":
                    Expression<Func<MAccGruposXPerfil, bool>> wherePerfil = x => (!string.IsNullOrWhiteSpace(codigo) ? x.IdAccPerfilNavigation.Codigo.Contains(codigo) : 1 == 1)
                                && (!string.IsNullOrWhiteSpace(descripcion) ? x.IdAccPerfilNavigation.Descripcion.Contains(descripcion) : 1 == 1);

                    var gxpPerfiles = await _context.MAccGruposXPerfil
                                                .Include(c => c.IdAccGrupoNavigation)
                                                .Include(c => c.IdAccPerfilNavigation)
                                                    .ThenInclude(c => c.AccGruposXPerfil)
                                                    .ThenInclude(g => g.IdAccGrupoNavigation)
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
                    Expression<Func<MAccGruposXPerfil, bool>> whereGrupo = x => (!string.IsNullOrWhiteSpace(codigo) ? x.IdAccGrupoNavigation.Codigo.Contains(codigo) : 1 == 1)
                                && (!string.IsNullOrWhiteSpace(descripcion) ? x.IdAccGrupoNavigation.Descripcion.Contains(descripcion) : 1 == 1);

                    var gxpGrupos = await _context.MAccGruposXPerfil
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
                    var perfil = await _context.MAccGruposXPerfil
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
                    var grupo = await _context.MAccGruposXPerfil
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
            var grupoXPerfil = await _context.MAccGruposXPerfil
                                    .Include(c => c.IdAccPerfilNavigation)
                                    .Include(c => c.IdAccGrupoNavigation)
                                    .ToListAsync();

            List<PerspectivaDTO<BasicDTO, BasicDTO>> perspectivas = new List<PerspectivaDTO<BasicDTO, BasicDTO>>();

            grupoXPerfil.ForEach( gxp =>
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
