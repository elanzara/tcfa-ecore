using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.DirectoryServices;

namespace eCore.Services
{
    using System;
    using System.Collections.Generic;
    using System.DirectoryServices;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using AutoMapper;
    using eCore.Context;
    using eCore.Entities;
    using eCore.Helpers;
    using eCore.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
  
    namespace WebApi.Services
    {
        public interface IUserService
        {
            string Exit(string token);
            User Authenticate(string username, string password, out string msj);
            ActionResult<IEnumerable<User>> GetAll();
            String GetUserByToken(string token);
            int? GetIdUserByToken(string token);
            AccUsuarios GetUserAllByToken(string token);
        }

        public class UserService : IUserService
        {
            private readonly ApplicationDbContext _context;
            private readonly AppSettings _appSettings;
            private readonly IMapper _mapper;

            //// users hardcoded for simplicity, store in a db with hashed passwords in production applications
            //private List<User> _users = new List<User>
            //{
            //    new User { Id = 1, Apellido = "Test", Nombres = "User", Username = "test", Password = "test" }
            //};

            public UserService(IOptions<AppSettings> appSettings, ApplicationDbContext context, IMapper mapper)
            {
                _appSettings = appSettings.Value;
                _context = context;
                _mapper = mapper;
            }

            public string Exit(string token)
            {
                string msj = null;
                var accUsuariosSesionDelete = _context.AccUsuariosSesiones.FirstOrDefault(x => x.Token == token);

                if (accUsuariosSesionDelete != null)
                {
                    _context.AccUsuariosSesiones.Remove(accUsuariosSesionDelete);

                    try
                    {
                        _context.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        msj = "Se ha producido un error: " + e.Message;
                        return msj;
                    }
                }

                return msj;
            }

            public User Authenticate(string username, string password, out string msj)
            {
                msj = null;
                DateTime fechaHoraExpiracion = DateTime.Now.AddDays(2);
                DateTime fechaHoraActualMenos1Hora = DateTime.Now.AddHours(-1);

                /*Validar AD*/
                //try
                //{
                //    DirectoryEntry entry = new DirectoryEntry("WinNT://toyotacfa.com.ar", username, password);//, System.DirectoryServices.AuthenticationTypes.ReadonlyServer
                //    //msj = entry.;
                //    //return null;
                //    object nativeObject = entry.NativeObject;
                //    //msj = entry.ToString();
                //    //return null;
                //    //msj = "Validado OK";
                //    //return null;
                //}
                //catch (DirectoryServicesCOMException dse)
                //{
                //    msj = "NO Validado por DirectoryServicesCOMException: " + dse.Message + " - " + dse.ExtendedErrorMessage + " - " + dse.ErrorCode + " - " + dse.ExtendedError;
                //    return null;
                //}
                //catch (Exception ex)
                //{
                //    msj = "NO Validado por Exception: " + ex.Message + " - " + ex.ToString();
                //}
                /*Fin Validar AD*/

                var AccUser = _context.AccUsuarios.SingleOrDefault(x => x.AdCuenta == username);

                if (AccUser == null)
                {
                    msj = "El usuario es incorrecto";
                    return null;
                }

                var user = _mapper.Map<User>(AccUser);

                // Genero el jwt token
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                    }),
                    Expires = fechaHoraExpiracion,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                user.Token = tokenHandler.WriteToken(token);

                //Elimino el password para que no se vea
                user.Password = null;

                //Eliminar token expirados de AccUsuariosSesiones y con tiempos de ultima consulta mayores a 1 hora
                var accUsuariosSesionesDelete = _context.AccUsuariosSesiones.Where(x => x.IdAccUsuario == AccUser.Id && (x.FinalizaEn <= DateTime.Now || x.UltimaConexion < fechaHoraActualMenos1Hora)).ToList();

                if (accUsuariosSesionesDelete.Count > 0)
                {
                    _context.AccUsuariosSesiones.RemoveRange(accUsuariosSesionesDelete);
                }

                try
                {
                    _context.SaveChanges();
                }
                catch (Exception e)
                {
                    msj = "Se ha producido un error: " + e.Message;
                    return null;
                }

                //AccUsuariosSesiones accUsuariosSesiones = _context.AccUsuariosSesiones.FirstOrDefault(x => x.IdAccUsuario == AccUser.Id);

                int cnt = _context.AccUsuariosSesiones.Where(x => x.IdAccUsuario == AccUser.Id).ToList().Count();

                AccUsuariosSesiones accUsuariosSesiones;

                if (cnt == 0)
                {
                    accUsuariosSesiones = new AccUsuariosSesiones();
                    accUsuariosSesiones.Id = 0;
                    accUsuariosSesiones.IdAccUsuario = AccUser.Id;
                    accUsuariosSesiones.Token = user.Token;
                    accUsuariosSesiones.CreadoEn = DateTime.Now;
                    accUsuariosSesiones.FinalizaEn = fechaHoraExpiracion;
                    accUsuariosSesiones.UltimaConexion = null;
                    accUsuariosSesiones.Eventos = null;

                    _context.AccUsuariosSesiones.Add(accUsuariosSesiones);
                }
                else
                {
                    if (cnt >= AccUser.MaxCantidadConexiones)
                    {
                        msj = "El usuario supera la maxima cantidad de conexiones al mismo tiempo";
                        return null;
                    }

                    accUsuariosSesiones = new AccUsuariosSesiones();
                    accUsuariosSesiones.Id = 0;
                    accUsuariosSesiones.IdAccUsuario = AccUser.Id;
                    accUsuariosSesiones.Token = user.Token;
                    accUsuariosSesiones.CreadoEn = DateTime.Now;
                    accUsuariosSesiones.FinalizaEn = fechaHoraExpiracion;
                    accUsuariosSesiones.UltimaConexion = null;
                    accUsuariosSesiones.Eventos = null;

                    _context.AccUsuariosSesiones.Add(accUsuariosSesiones);
                }

                try
                {
                    _context.SaveChanges();
                }
                catch (Exception e)
                {
                    msj = "Se ha producido un error: " + e.Message;
                    return null;
                }

                return user;
            }

            public ActionResult<IEnumerable<User>> GetAll()
            {
                //Retorno usuarios
                var AccUsers = _context.AccUsuarios.ToList();
                var users = _mapper.Map<List<User>>(AccUsers);

                return users;
            }

            public string GetUserByToken(string token)
            {
                token = token.Replace("Bearer ", "");

                string retorno;

                var accUsuarioSesion = _context.AccUsuariosSesiones.Include(b => b.IdAccUsuarioNavigation).FirstOrDefault(x => x.Token == token);
                
                if (accUsuarioSesion == null)
                {
                    return null;
                }
                else
                {
                    var accUsuario = accUsuarioSesion.IdAccUsuarioNavigation;

                    if (accUsuario == null)
                    {
                        return null;
                    }

                    accUsuarioSesion.UltimaConexion = DateTime.Now;
                    accUsuario.UltimaConexion = DateTime.Now;

                    _context.AccUsuariosSesiones.Update(accUsuarioSesion);
                    _context.AccUsuarios.Update(accUsuario);

                    retorno = accUsuario.AdCuenta;
                }

                try
                {
                    _context.SaveChanges();
                }
                catch (Exception e)
                {
                    //No se hace nada
                }

                return retorno;
            }

            public int? GetIdUserByToken(string token)
            {
                token = token.Replace("Bearer ", "");

                int retorno;

                var accUsuarioSesion = _context.AccUsuariosSesiones.Include(b => b.IdAccUsuarioNavigation).FirstOrDefault(x => x.Token == token);

                if (accUsuarioSesion == null)
                {
                    return null;
                }
                else
                {
                    var accUsuario = accUsuarioSesion.IdAccUsuarioNavigation;

                    if (accUsuario == null)
                    {
                        return null;
                    }

                    accUsuarioSesion.UltimaConexion = DateTime.Now;
                    accUsuario.UltimaConexion = DateTime.Now;

                    _context.AccUsuariosSesiones.Update(accUsuarioSesion);
                    _context.AccUsuarios.Update(accUsuario);

                    retorno = accUsuario.Id;
                }

                try
                {
                    _context.SaveChanges();
                }
                catch (Exception e)
                {
                    //No se hace nada
                }

                return retorno;
            }

            public AccUsuarios GetUserAllByToken(string token)
            {
                token = token.Replace("Bearer ", "");

                AccUsuarios retorno;

                var accUsuarioSesion = _context.AccUsuariosSesiones.Include(b => b.IdAccUsuarioNavigation).FirstOrDefault(x => x.Token == token);

                if (accUsuarioSesion == null)
                {
                    return null;
                }
                else
                {
                    var accUsuario = accUsuarioSesion.IdAccUsuarioNavigation;

                    if (accUsuario == null)
                    {
                        return null;
                    }

                    accUsuarioSesion.UltimaConexion = DateTime.Now;
                    accUsuario.UltimaConexion = DateTime.Now;

                    _context.AccUsuariosSesiones.Update(accUsuarioSesion);
                    _context.AccUsuarios.Update(accUsuario);

                    retorno = accUsuario;
                }

                try
                {
                    _context.SaveChanges();
                }
                catch (Exception e)
                {
                    //No se hace nada
                }

                return retorno;
            }

        }
    }
}
