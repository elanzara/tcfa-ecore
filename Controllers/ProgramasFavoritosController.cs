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
using System.Data.SqlClient;
using eCore.Models;
using AutoMapper;

namespace eCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [EnableCors("AllowOrigin")]
    public class ProgramasFavoritosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public ProgramasFavoritosController(ApplicationDbContext context, IMapper mapper, IUserService userService)
        {
            _context = context;
            _mapper = mapper;
            _userService = userService;
        }

        // GET: api/ProgramasFavoritos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccProgramasFavoritosXUsuario>>> GetAccProgramasFavoritosXUsuario()
        {
            //Recupero y valido el usuario del token
            int? idUserConnected = _userService.GetIdUserByToken(Request.Headers["Authorization"]);
            if (idUserConnected == null)
            {
                return StatusCode(419, "Error en autenticación de usuario.");
            }

            return await _context.AccProgramasFavoritosXUsuario.Where(x => x.IdAccUsuario == idUserConnected.Value).ToListAsync();
        }

        // GET: api/ProgramasFavoritos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AccProgramasFavoritosXUsuario>> GetAccProgramasFavoritosXUsuario(int id)
        {
            //Recupero y valido el usuario del token
            int? idUserConnected = _userService.GetIdUserByToken(Request.Headers["Authorization"]);
            if (idUserConnected == null)
            {
                return StatusCode(419, "Error en autenticación de usuario.");
            }

            var accProgramasFavoritosXUsuario = await _context.AccProgramasFavoritosXUsuario.Include(a => a.IdAccProgramaNavigation).ThenInclude(b => b.AccProgramasFavoritosXUsuario).FirstOrDefaultAsync(i => i.Id == id);

            if (accProgramasFavoritosXUsuario == null)
            {
                return NotFound();
            }

            return accProgramasFavoritosXUsuario;
        }

        //// PUT: api/ProgramasFavoritos/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutAccProgramasFavoritosXUsuario(int id, AccProgramasFavoritosXUsuario accProgramasFavoritosXUsuario)
        //{
        //    if (id != accProgramasFavoritosXUsuario.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(accProgramasFavoritosXUsuario).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!AccProgramasFavoritosXUsuarioExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/ProgramasFavoritos
        [HttpPost]
        public async Task<ActionResult<AccProgramasFavoritosXUsuario>> PostAccProgramasFavoritosXUsuario(AccProgramasFavoritosXUsuario accProgramasFavoritosXUsuario)
        {
            //Recupero y valido el usuario del token
            AccUsuarios userConnected = _userService.GetUserAllByToken(Request.Headers["Authorization"]);
            if (userConnected == null)
            {
                return StatusCode(419, "Error en autenticación de usuario.");
            }

            var accPrograma = await _context.AccProgramas.FindAsync(accProgramasFavoritosXUsuario.IdAccPrograma);

            if (accPrograma == null)
            {
                return NotFound();
            }

            var accProgramasFavoritosXUsuarioExist = await _context.AccProgramasFavoritosXUsuario.FirstOrDefaultAsync(i => i.IdAccUsuario == userConnected.Id && i.IdAccPrograma == accProgramasFavoritosXUsuario.IdAccPrograma);

            if (accProgramasFavoritosXUsuarioExist != null)
            {
                return StatusCode(409, "El programa seleccionado ya está en Favoritos");
            }

            accProgramasFavoritosXUsuario.IdAccUsuario = userConnected.Id;
            accProgramasFavoritosXUsuario.CreadoEn = DateTime.Now;
            accProgramasFavoritosXUsuario.CreadoPor = userConnected.AdCuenta;

            _context.AccProgramasFavoritosXUsuario.Add(accProgramasFavoritosXUsuario);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.InnerException.Message);
            }

            return CreatedAtAction("GetAccProgramasFavoritosXUsuario", new { id = accProgramasFavoritosXUsuario.Id }, accProgramasFavoritosXUsuario);
        }

        //// DELETE: api/ProgramasFavoritos/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<AccProgramasFavoritosXUsuario>> DeleteAccProgramasFavoritosXUsuario(int id)
        //{
        //    //Recupero y valido el usuario del token
        //    AccUsuarios userConnected = _userService.GetUserAllByToken(Request.Headers["Authorization"]);
        //    if (userConnected == null)
        //    {
        //        return StatusCode(419, "Error en autenticación de usuario.");
        //    }

        //    var accProgramasFavoritosXUsuario = await _context.AccProgramasFavoritosXUsuario.FindAsync(id);

        //    if (accProgramasFavoritosXUsuario == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.AccProgramasFavoritosXUsuario.Remove(accProgramasFavoritosXUsuario);
        //    await _context.SaveChangesAsync();

        //    return accProgramasFavoritosXUsuario;
        //}

        // DELETE: api/ProgramasFavoritos/grp001
        [HttpDelete("{codigo}")]
        public async Task<ActionResult<AccProgramasFavoritosXUsuario>> DeleteAccProgramasFavoritosXUsuarioPorCodigo(string codigo)
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
                return NotFound();
            }

            var accProgramasFavoritosXUsuario = await _context.AccProgramasFavoritosXUsuario.FirstOrDefaultAsync(x => x.IdAccPrograma == accPrograma.Id && x.IdAccUsuario == userConnected.Id);

            if (accProgramasFavoritosXUsuario == null)
            {
                return NotFound();
            }

            _context.AccProgramasFavoritosXUsuario.Remove(accProgramasFavoritosXUsuario);
            await _context.SaveChangesAsync();

            return accProgramasFavoritosXUsuario;
        }


        private bool AccProgramasFavoritosXUsuarioExists(int id)
        {
            return _context.AccProgramasFavoritosXUsuario.Any(e => e.Id == id);
        }
    }
}
