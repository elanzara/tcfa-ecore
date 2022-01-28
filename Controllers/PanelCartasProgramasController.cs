using AutoMapper;
using eCore.Context;
using eCore.Entities;
using eCore.Models;
using eCore.Services.WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace eCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [EnableCors("AllowOrigin")]
    public class PanelCartasProgramasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public PanelCartasProgramasController(ApplicationDbContext context, IMapper mapper, IUserService userService)
        {
            _context = context;
            _mapper = mapper;
            _userService = userService;
        }

        // GET: api/PanelCartasProgramas/favoritos
        [HttpGet("favoritos")]
        public async Task<ActionResult<IEnumerable<ProgramasDTO>>> GetPanelCartasProgramasFavoritos()
        {
            //Recupero y valido el usuario del token
            AccUsuarios userConnected = _userService.GetUserAllByToken(Request.Headers["Authorization"]);
            if (userConnected == null)
            {
                return StatusCode(419, "Error en autenticación de usuario.");
            }

            List<ProgramasDTO> listaProgramas;
            listaProgramas = null;

            //var accProgramas = await _context.AccProgramas.ToListAsync();
            List<AccProgramasFavoritosXUsuario> accProgramasFavoritos = await _context.AccProgramasFavoritosXUsuario.Include(a => a.IdAccProgramaNavigation).Where(x => x.IdAccUsuario == userConnected.Id).ToListAsync();

            if (accProgramasFavoritos == null)
            {
                return listaProgramas;
            }

            listaProgramas = new List<ProgramasDTO>();

            foreach (var programaFavorito in accProgramasFavoritos)
            {
                var programaReal = programaFavorito.IdAccProgramaNavigation;
                var programa = _mapper.Map<ProgramasDTO>(programaReal);

                //Recuperar cantidades
                Type tipo = Type.GetType("eCore.Entities." + programa.entidad);
                var tableSet = _context.Set(tipo);
                var list = await tableSet.ToListAsync();

                programa.cnt_total = list.Count();

                tipo = Type.GetType("eCore.Entities.M" + programa.entidad);
                var tableSetM = _context.Set(tipo);
                var tabla = AttributeReader.GetTableName(_context, tipo);
                var tablaPar = new SqlParameter("@tabla", tabla);

                var MusuarioPar = new SqlParameter("@usuario", userConnected.AdCuenta);
                var Mlist = await tableSetM.FromSql($"SELECT * FROM {@tabla} Where creado_por = @usuario", tablaPar, MusuarioPar).ToListAsync();

                programa.cnt_mios = Mlist.Count();

                var AusuarioPar = new SqlParameter("@usuario", userConnected.AdCuenta);
                var Alist = await tableSetM.FromSql($"SELECT * FROM {@tabla} Where creado_por != @usuario", tablaPar, AusuarioPar).ToListAsync();

                programa.cnt_autorizar = Alist.Count();

                listaProgramas.Add(programa);
            }

            return listaProgramas;
        }


        // GET: api/PanelCartasProgramas/recientes
        [HttpGet("recientes")]
        public async Task<ActionResult<IEnumerable<ProgramasDTO>>> GetPanelCartasProgramasRecientes()
        {
            //Recupero y valido el usuario del token
            AccUsuarios userConnected = _userService.GetUserAllByToken(Request.Headers["Authorization"]);
            if (userConnected == null)
            {
                return StatusCode(419, "Error en autenticación de usuario.");
            }

            List<ProgramasDTO> listaProgramas;
            listaProgramas = null;

            var accProgramasRecientes = await _context.AccProgramasRecientesXUsuario.Where(x => x.IdAccUsuario == userConnected.Id).GroupBy(x => new { x.IdAccUsuario, x.IdAccPrograma }).Select(x => new { x.Key.IdAccUsuario, x.Key.IdAccPrograma}).Take(10).ToListAsync();

            if (accProgramasRecientes == null)
            {
                return listaProgramas;
            }

            listaProgramas = new List<ProgramasDTO>();

            foreach (var programaReciente in accProgramasRecientes)
            {
                var programaReal = await _context.AccProgramas.FindAsync(programaReciente.IdAccPrograma);
                var programa = _mapper.Map<ProgramasDTO>(programaReal);

                var progRec = await _context.AccProgramasRecientesXUsuario.OrderByDescending(x => x.Fecha).Where(x => x.IdAccUsuario == userConnected.Id).FirstOrDefaultAsync(i => i.IdAccPrograma == programaReciente.IdAccPrograma);
                programa.ultimo_uso = progRec.Fecha;

                //Recuperar cantidades
                Type tipo = Type.GetType("eCore.Entities." + programa.entidad);
                var tableSet = _context.Set(tipo);
                var list = await tableSet.ToListAsync();

                programa.cnt_total = list.Count();

                tipo = Type.GetType("eCore.Entities.M" + programa.entidad);
                var tableSetM = _context.Set(tipo);
                var tabla = AttributeReader.GetTableName(_context, tipo);
                var tablaPar = new SqlParameter("@tabla", tabla);

                var MusuarioPar = new SqlParameter("@usuario", userConnected.AdCuenta);
                var Mlist = await tableSetM.FromSql($"SELECT * FROM {@tabla} Where creado_por = @usuario", tablaPar, MusuarioPar).ToListAsync();

                programa.cnt_mios = Mlist.Count();

                var AusuarioPar = new SqlParameter("@usuario", userConnected.AdCuenta);
                var Alist = await tableSetM.FromSql($"SELECT * FROM {@tabla} Where creado_por != @usuario", tablaPar, AusuarioPar).ToListAsync();

                programa.cnt_autorizar = Alist.Count();

                listaProgramas.Add(programa);
            }

            listaProgramas = listaProgramas.OrderByDescending(x => x.ultimo_uso).ToList();

            return listaProgramas;
        }


        // GET: api/PanelCartasProgramas/programasUsuario
        [HttpGet("programasUsuario")]
        public async Task<ActionResult<IEnumerable<ProgramasDTO>>> GetPanelCartasProgramasProgUsr()
        {
            //Recupero y valido el usuario del token
            AccUsuarios userConnected = _userService.GetUserAllByToken(Request.Headers["Authorization"]);
            if (userConnected == null)
            {
                return StatusCode(419, "Error en autenticación de usuario.");
            }

            var perfilPar = new SqlParameter("@perfil", userConnected.IdAccPerfil);

            var listaProgramas = await _context.AccProgramas.FromSql($@"select     p.[id]
                                                                            ,p.[codigo]
                                                                            ,p.[descripcion]
                                                                            ,p.[objeto]
                                                                            ,p.[parametros]
                                                                            ,p.[icono]
                                                                            ,p.[creado_por]
                                                                            ,p.[creado_en]
                                                                            ,p.[autorizado_por]
                                                                            ,p.[autorizado_en]
                                                                            ,p.[entidad]
                                                            from acc_modulos m
                                                            join acc_programas_x_modulos pm ON pm.id_acc_modulo = m.id
                                                            join acc_programas p ON p.id = pm.id_acc_programa
                                                            join acc_programas_acciones pa ON pa.id_acc_programa = p.id
                                                            join acc_programas_acciones_x_grupo pg ON pg.id_programa_accion = pa.id
                                                            join acc_grupos g ON g.id = pg.id_acc_grupo
                                                            join acc_grupos_x_perfil gp ON gp.id_acc_grupo = g.id
                                                            where gp.id_acc_perfil = @perfil
                                                            group by p.[id]
                                                                    ,p.[codigo]
                                                                    ,p.[descripcion]
                                                                    ,p.[objeto]
                                                                    ,p.[parametros]
                                                                    ,p.[icono]
                                                                    ,p.[creado_por]
                                                                    ,p.[creado_en]
                                                                    ,p.[autorizado_por]
                                                                    ,p.[autorizado_en]
                                                                    ,p.[entidad]", perfilPar).ToListAsync();

            List<ProgramasDTO> retorno = _mapper.Map<List<ProgramasDTO>>(listaProgramas);

            return retorno;
        }

    }
}
