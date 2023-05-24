using Backend_Repositor.io.Data;
using Backend_Repositor.io.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_Repositor.io.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepositoriosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RepositoriosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Repositorio>>> GetRepositorios()
        {
            var repositorios = await _context.Repositorios.ToListAsync();
            return repositorios;
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Repositorio>> GetRepositorio(long id)
        {
            var repositorio = await _context.Repositorios.FindAsync(id);

            if (repositorio == null)
            {
                return NotFound("El repositorio no existe");
            }

            return repositorio;
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Repositorio>>> GetRepositoriosByUser([FromRoute]long userId)
        {
            var repositorios = new List<Repositorio>();
            repositorios = await _context.Repositorios
                .Where(r => r.UsuarioId == userId)
                .ToListAsync();

            if (repositorios == null)
            {
                return NotFound("El usuario no tiene repositorios");
            }

            return repositorios;
        }

        // POST api/<UsersController>
        [HttpPost]
        [Route("add")]
        public async Task<ActionResult<Repositorio>> Post([FromBody] Repositorio repositorio)
        {
            repositorio.FechaMod = DateTime.Now;
            _context.Repositorios.Add(repositorio);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRepositorio), new { id = repositorio.Id }, repositorio);
        }

        // PUT api/<UsersController>/5
        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Put(Repositorio repositorio)
        {
            repositorio.FechaMod = DateTime.Now;
            _context.Entry(repositorio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RepositorioExists(repositorio.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok("Repositorio actualizado con exito");
        }

        private bool RepositorioExists(long id)
        {
            return _context.Repositorios.Any(r => r.Id == id);
        }

        // DELETE api/<UsersController>/5
        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] long id)
        {
            var repositorio = await _context.Repositorios.FindAsync(id);

            if (repositorio == null)
            {
                return NotFound();
            }

            _context.Repositorios.Remove(repositorio);
            await _context.SaveChangesAsync();

            return Ok("Usuario borrado con exito");
        }
    }
}
