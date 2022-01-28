using AutoMapper;
using eCore.Context;
using eCore.Models;
using eCore.Entities;
using Microsoft.AspNetCore.Mvc;
using Z.EntityFramework.Plus;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using eCore.Services.WebApi.Services;
using System.Data.SqlClient;

namespace eCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [EnableCors("AllowOrigin")]
    public class PanelMenuController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public PanelMenuController(ApplicationDbContext context, IMapper mapper, IUserService userService)
        {
            _context = context;
            _mapper = mapper;
            _userService = userService;
        }

        // GET: api/PanelMenu
        [HttpGet]
        public async Task<ActionResult<PanelMenuDTO>> GetPanelMenu()
        {
            //var accModulos = await _context.AccModulos.Include(b => b.AccProgramasXModulos).ThenInclude(c => c.IdAccProgramaNavigation).ToListAsync();

            //Recupero y valido el usuario del token
            AccUsuarios userConnected = _userService.GetUserAllByToken(Request.Headers["Authorization"]);
            if (userConnected == null)
            {
                return StatusCode(419, "Error en autenticación de usuario.");
            }

            List<ModulosDTO> modulosDTO = new List<ModulosDTO>();
            ModulosDTO moduloDTOAux;
            PanelMenuDTO retorno = new PanelMenuDTO();
            
            retorno.codigo = "0000";
            retorno.descripcion = "Inicio";
            retorno.modulos = null;

            var perfilPar = new SqlParameter("@perfil", userConnected.IdAccPerfil);
            var accModulos = await _context.AccModulos.FromSql($@"select     m.[id]
                                                                            ,m.[codigo]
                                                                            ,m.[descripcion]
                                                                            ,m.[icono]
                                                                            ,m.[id_acc_modulo]
                                                                            ,m.[creado_por]
                                                                            ,m.[creado_en]
                                                                            ,m.[autorizado_por]
                                                                            ,m.[autorizado_en]
                                                                    from acc_modulos m
                                                                    join acc_programas_x_modulos pm ON pm.id_acc_modulo = m.id
                                                                    join acc_programas p ON p.id = pm.id_acc_programa
                                                                    join acc_programas_acciones pa ON pa.id_acc_programa = p.id
                                                                    join acc_programas_acciones_x_grupo pg ON pg.id_programa_accion = pa.id
                                                                    join acc_grupos g ON g.id = pg.id_acc_grupo
                                                                    join acc_grupos_x_perfil gp ON gp.id_acc_grupo = g.id
                                                                    where gp.id_acc_perfil = @perfil
                                                                    and m.id_acc_modulo IS NULL
                                                                    group by     m.[id]
                                                                                ,m.[codigo]
                                                                                ,m.[descripcion]
                                                                                ,m.[icono]
                                                                                ,m.[id_acc_modulo]
                                                                                ,m.[creado_por]
                                                                                ,m.[creado_en]
                                                                                ,m.[autorizado_por]
                                                                                ,m.[autorizado_en]", perfilPar).ToListAsync();

            //var accModulos = await _context.AccProgramasAccionesXGrupo.Include(b => b.).ThenInclude(c => c.IdAccProgramaNavigation).ToListAsync();

            if (accModulos == null)
            {
                return retorno;
            }

            retorno.modulos = new List<ModulosDTO>();

            foreach (var modulo in accModulos)
            {
                moduloDTOAux = _mapper.Map<ModulosDTO>(modulo);

                moduloDTOAux = CargarModulo(moduloDTOAux, userConnected.IdAccPerfil);

                retorno.modulos.Add(moduloDTOAux);
            }
       
            return retorno;
        }

        // GET: api/PanelMenu/tituloPrograma/grp001
        [HttpGet("tituloPrograma/{codigo}")]
        public async Task<ActionResult<String>> GetTituloPrograma(string codigo)
        {
            //Recupero y valido el usuario del token
            AccUsuarios userConnected = _userService.GetUserAllByToken(Request.Headers["Authorization"]);
            if (userConnected == null)
            {
                return StatusCode(419, "Error en autenticación de usuario.");
            }

            var accPrograma = await _context.AccProgramas.FirstOrDefaultAsync(x => x.Codigo == codigo);

            if (accPrograma == null)
            {
                return "";
            }

            return accPrograma.Codigo.ToUpper() + " - " + accPrograma.Descripcion.ToUpper();
        }


        private ModulosDTO CargarModulo(ModulosDTO modulo, int perfil)
        {
            var perfilPar = new SqlParameter("@perfil", perfil);
            var moduloPar = new SqlParameter("@modulo", modulo.id);

            var listaProgramas = _context.AccProgramas.FromSql($@"select     p.[id]
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
                                                            and m.id = @modulo
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
                                                                    ,p.[entidad]", perfilPar, moduloPar).ToList();

            modulo.programas = _mapper.Map<List<ProgramasDTO>>(listaProgramas);

            var listaModulos = _context.AccModulos.FromSql($@"select m.[id]
                                                                    ,m.[codigo]
                                                                    ,m.[descripcion]
                                                                    ,m.[icono]
                                                                    ,m.[id_acc_modulo]
                                                                    ,m.[creado_por]
                                                                    ,m.[creado_en]
                                                                    ,m.[autorizado_por]
                                                                    ,m.[autorizado_en]
                                                        from acc_modulos m
                                                        join acc_programas_x_modulos pm ON pm.id_acc_modulo = m.id
                                                        join acc_programas p ON p.id = pm.id_acc_programa
                                                        join acc_programas_acciones pa ON pa.id_acc_programa = p.id
                                                        join acc_programas_acciones_x_grupo pg ON pg.id_programa_accion = pa.id
                                                        join acc_grupos g ON g.id = pg.id_acc_grupo
                                                        join acc_grupos_x_perfil gp ON gp.id_acc_grupo = g.id
                                                        where gp.id_acc_perfil = @perfil
                                                        and m.id_acc_modulo = @modulo
                                                        group by m.[id]
                                                                ,m.[codigo]
                                                                ,m.[descripcion]
                                                                ,m.[icono]
                                                                ,m.[id_acc_modulo]
                                                                ,m.[creado_por]
                                                                ,m.[creado_en]
                                                                ,m.[autorizado_por]
                                                                ,m.[autorizado_en]", perfilPar, moduloPar).ToList();

            var listaModulosInternos = _mapper.Map<List<ModulosDTO>>(listaModulos);

            modulo.modulos = new List<ModulosDTO>();

            foreach (var moduloInterno in listaModulosInternos)
            {
                var accModuloAux = CargarModulo(moduloInterno, perfil);

                modulo.modulos.Add(accModuloAux);
            }

            return modulo;
        }

    }
}

