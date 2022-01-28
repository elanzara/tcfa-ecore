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

namespace eCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [EnableCors("AllowOrigin")]
    public class ProgramasRecientesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;

        public ProgramasRecientesController(ApplicationDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        // GET: api/ProgramasRecientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccProgramasRecientesXUsuario>>> GetAccProgramasRecientesXUsuario()
        {
            //Recupero y valido el usuario del token
            int? idUserConnected = _userService.GetIdUserByToken(Request.Headers["Authorization"]);
            if (idUserConnected == null)
            {
                return StatusCode(419, "Error en autenticación de usuario.");
            }

            return await _context.AccProgramasRecientesXUsuario.Where(x => x.IdAccUsuario == idUserConnected.Value).ToListAsync();
        }

        // GET: api/ProgramasRecientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AccProgramasRecientesXUsuario>> GetAccProgramasRecientesXUsuario(int id)
        {
            //Recupero y valido el usuario del token
            int? idUserConnected = _userService.GetIdUserByToken(Request.Headers["Authorization"]);
            if (idUserConnected == null)
            {
                return StatusCode(419, "Error en autenticación de usuario.");
            }

            var accProgramasRecientesXUsuario = await _context.AccProgramasRecientesXUsuario.Include(a => a.IdAccProgramaNavigation).ThenInclude(b => b.AccProgramasFavoritosXUsuario).FirstOrDefaultAsync(i => i.Id == id);

            if (accProgramasRecientesXUsuario == null)
            {
                return NotFound();
            }

            return accProgramasRecientesXUsuario;
        }

        //// PUT: api/ProgramasRecientes/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutAccProgramasRecientesXUsuario(int id, AccProgramasRecientesXUsuario accProgramasRecientesXUsuario)
        //{
        //    if (id != accProgramasRecientesXUsuario.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(accProgramasRecientesXUsuario).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!AccProgramasRecientesXUsuarioExists(id))
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

        // POST: api/ProgramasRecientes
        [HttpPost]
        public async Task<ActionResult<AccProgramasRecientesXUsuario>> PostAccProgramasRecientesXUsuario(AccProgramasRecientesXUsuario accProgramasRecientesXUsuario)
        {
            //Recupero y valido el usuario del token
            AccUsuarios userConnected = _userService.GetUserAllByToken(Request.Headers["Authorization"]);
            if (userConnected == null)
            {
                return StatusCode(419, "Error en autenticación de usuario.");
            }

            var accPrograma = await _context.AccProgramas.FindAsync(accProgramasRecientesXUsuario.IdAccPrograma);

            if (accPrograma == null)
            {
                return NotFound();
            }

            accProgramasRecientesXUsuario.IdAccUsuario = userConnected.Id;
            accProgramasRecientesXUsuario.Fecha = DateTime.Now;

            //20201119 - Edu por error al guardar los recientes
            //Eliminar registros mayores a 100
            //var accProgramasRecientesXUsuarioDelete = await _context.AccProgramasRecientesXUsuario.Where(x => x.IdAccUsuario == userConnected.Id).OrderByDescending(o => o.Fecha).Skip(100).ToListAsync();

            //20201119 - Edu por error al guardar los recientes
            //if (accProgramasRecientesXUsuarioDelete.Count > 0)
            //{
            //    _context.AccProgramasRecientesXUsuario.RemoveRange(accProgramasRecientesXUsuarioDelete);
            //}

            //Agrego la nueva entidad
            _context.AccProgramasRecientesXUsuario.Add(accProgramasRecientesXUsuario);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.InnerException.Message);
            }

            return CreatedAtAction("GetAccProgramasRecientesXUsuario", new { id = accProgramasRecientesXUsuario.Id }, accProgramasRecientesXUsuario);
        }

        // DELETE: api/ProgramasRecientes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AccProgramasRecientesXUsuario>> DeleteAccProgramasRecientesXUsuario(int id)
        {
            var accProgramasRecientesXUsuario = await _context.AccProgramasRecientesXUsuario.FindAsync(id);
            if (accProgramasRecientesXUsuario == null)
            {
                return NotFound();
            }

            _context.AccProgramasRecientesXUsuario.Remove(accProgramasRecientesXUsuario);
            await _context.SaveChangesAsync();

            return accProgramasRecientesXUsuario;
        }

        private bool AccProgramasRecientesXUsuarioExists(int id)
        {
            return _context.AccProgramasRecientesXUsuario.Any(e => e.Id == id);
        }
    }
}
