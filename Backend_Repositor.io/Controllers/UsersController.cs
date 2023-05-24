using Backend_Repositor.io.Data;
using Backend_Repositor.io.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_Repositor.io.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }


        // GET: api/<UsersController>
        [HttpGet]
        public async  Task<ActionResult<IEnumerable<User>>> GetUsuarios()
        {
            var usuarios = await _context.Users.ToListAsync();
            return usuarios;
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUsuario(long id)
        {
            var usuario = await _context.Users.FindAsync(id);

            if (usuario == null)
            {
                return NotFound("El usuario no existe");
            }

            return usuario;
        }

        [HttpGet("relacion/{id}")]
        public async Task<ActionResult<Relacion>> GetRelacion(long id)
        {
            var usuario = await _context.Relaciones.FindAsync(id);

            if (usuario == null)
            {
                return NotFound("El usuario no existe");
            }

            return usuario;
        }

        // POST api/<UsersController>
        [HttpPost]
        [Route("add")]
        public async Task<ActionResult<User>> Post([FromBody] User usuario)
        {
            usuario.FechaAlta = DateTime.Now;
            _context.Users.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUsuario), new { id = usuario.Id }, usuario);
        }

        [HttpPost]
        [Route("relacion/add")]
        public async Task<ActionResult<User>> PostRelacion([FromQuery] long usuario1Id, long usuario2Id)
        {
            var usuario = new Relacion();
            usuario.FechaMod = DateTime.Now;
            usuario.SeguidorId = usuario1Id;
            usuario.SeguidoId = usuario2Id;
            _context.Relaciones.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUsuario), new { id = usuario.Id }, usuario);
        }

        // PUT api/<UsersController>/5
        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Put(User usuario)
        {
            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(usuario.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok("Usuario actualizado con exito");
        }

        private bool UsuarioExists(long id)
        {
            return _context.Users.Any(u => u.Id == id);
        }

        // DELETE api/<UsersController>/5
        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute]long id)
        {
            var usuario = await _context.Users.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            _context.Users.Remove(usuario);
            await _context.SaveChangesAsync();

            return Ok("Usuario borrado con exito");
        }
    }
}
