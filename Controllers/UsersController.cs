using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AutoMapper.Configuration;
using eCore.Models;
using eCore.Services.WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCore.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public ActionResult<User> Authenticate([FromBody]User userParam)
        {
            string msj;
            var user = _userService.Authenticate(userParam.Username, userParam.Password, out msj);

            if (msj != null)
                return StatusCode(420, msj);

            return user;
        }

        [AllowAnonymous]
        [HttpPost("exit")]
        public ActionResult Exit([FromBody]Token tokenParam)
        {
            string msj = _userService.Exit(tokenParam.token);

            if (msj != null)
                return StatusCode(420, msj);

            return NoContent();
        }


        [AllowAnonymous]
        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            try
            {
                ActionResult<IEnumerable<User>> users = _userService.GetAll();
                return users;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}