using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Threading.Tasks;
using eCore.Context;
using eCore.Entities;
using eCore.Models;
using eCore.Services.WebApi.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [EnableCors("AllowOrigin")]
    public class ADUsuariosController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ApplicationDbContext _context;

        public ADUsuariosController(ApplicationDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        // GET: api/ADUsuarios
        [HttpGet]
        public ActionResult<List<ADUsuariosDTO>> Get(bool? noRegistrados, string nombres, string apellido, string adCuenta)
        {
            //Recupero y valido el usuario del token
            /*Formato original sin AD*/
            AccUsuarios userConnected = _userService.GetUserAllByToken(Request.Headers["Authorization"]);
            if (userConnected == null)
            {
                return StatusCode(419, "Error en autenticación de usuario.");
            }

            List<ADUsuariosDTO> resultRows = new List<ADUsuariosDTO>();
            /*Fin - Formato original sin AD*/
            /*Formato con AD*/
            /*
            List<ADUsuariosDTO> resultRows = new List<ADUsuariosDTO>();
            using (DirectoryEntry computerEntry = new DirectoryEntry("WinNT://toyotacfa.com.ar"))
            {
                IEnumerable<string> userNames = computerEntry.Children
                    .Cast<DirectoryEntry>()
                    .Where(childEntry => childEntry.SchemaClassName == "User")
                    .Select(userEntry => userEntry.Name);

                foreach (string name in userNames) {
                    //Console.WriteLine(name);
                    resultRows.Add(new ADUsuariosDTO
                    {
                        SecurityIdentifier = name,
                        AdCuenta = name,
                        Nombres = name,
                        Apellido = name
                    });

                }

            }*/
            /*Fin - Formato con AD*/


            resultRows.Add(new ADUsuariosDTO
            {
                SecurityIdentifier = "11",
                AdCuenta = "usuario11",
                Nombres = "Once",
                Apellido = "Usuario"
            });

            resultRows.Add(new ADUsuariosDTO
            {
                SecurityIdentifier = "12",
                AdCuenta = "usuario12",
                Nombres = "Doce",
                Apellido = "Usuario"
            });

            resultRows.Add(new ADUsuariosDTO
            {
                SecurityIdentifier = "13",
                AdCuenta = "usuario13",
                Nombres = "Trece",
                Apellido = "Usuario"
            });

            resultRows.Add(new ADUsuariosDTO
            {
                SecurityIdentifier = "14",
                AdCuenta = "usuario14",
                Nombres = "Catorce",
                Apellido = "Usuario"
            });

            resultRows.Add(new ADUsuariosDTO
            {
                SecurityIdentifier = "15",
                AdCuenta = "usuario15",
                Nombres = "Quince",
                Apellido = "Usuario"
            });

            resultRows.Add(new ADUsuariosDTO
            {
                SecurityIdentifier = "16",
                AdCuenta = "usuario16",
                Nombres = "Dieciseis",
                Apellido = "Usuario"
            });

            resultRows.Add(new ADUsuariosDTO
            {
                SecurityIdentifier = "17",
                AdCuenta = "usuario17",
                Nombres = "Diecisiete",
                Apellido = "Usuario"
            });

            resultRows.Add(new ADUsuariosDTO
            {
                SecurityIdentifier = "18",
                AdCuenta = "escordamaglia",
                Nombres = "Ezequiel",
                Apellido = "Scordamaglia"
            });
            resultRows.Add(new ADUsuariosDTO
            {
                SecurityIdentifier = "19",
                AdCuenta = "llarrosa",
                Nombres = "Leonardo",
                Apellido = "Larrosa"
            });


            List<ADUsuariosDTO> result = resultRows.FindAll(x => (!string.IsNullOrWhiteSpace(nombres) ? x.Nombres.Contains(nombres) : 1 == 1)
                                    && (!string.IsNullOrWhiteSpace(apellido) ? x.Apellido.Contains(apellido) : 1 == 1)
                                    && (!string.IsNullOrWhiteSpace(adCuenta) ? x.AdCuenta.Contains(adCuenta) : 1 == 1));

            if (noRegistrados.HasValue && noRegistrados.Value)
            {
                HashSet<string> accUsuarios = _context.AccUsuarios.Select(x => x.SecurityIdentifier).ToHashSet();
                HashSet<string> mAccUsuarios = _context.MAccUsuarios.Select(x => x.SecurityIdentifier).ToHashSet();
                List<string> usuarios = accUsuarios.Union(mAccUsuarios).ToList();

                result = result.FindAll(x => !usuarios.Any(y => y.Equals(x.SecurityIdentifier)));
            }

            return result;
        }
    }
}
