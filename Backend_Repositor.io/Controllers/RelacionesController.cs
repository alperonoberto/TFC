using Backend_Repositor.io.Data;
using Backend_Repositor.io.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_Repositor.io.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelacionesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RelacionesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Relacion>>> GetRelaciones()
        {
            var relaciones = await _context.Relaciones.ToListAsync();

            return relaciones;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Relacion>>> GetRelacionesById(long id)
        {
            var relaciones = await _context.Relaciones.Where(r => r.Id == id).ToListAsync();

            if (relaciones == null)
            {
                return NotFound("El usuario no existe");
            }

            return relaciones;
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Relacion>>> GetRelacionesByUser([FromRoute]long userId)
        {
            var relaciones = await _context.Relaciones.Where(r => (r.SeguidorId == userId || r.SeguidoId == userId)).ToListAsync();

            if (relaciones == null)
            {
                return NotFound("El usuario no existe");
            }

            return relaciones;
        }

        [HttpPost]
        [Route("relacion/add")]
        public async Task<ActionResult<Relacion>> PostRelacion([FromBody] Relacion relacion)
        {
            relacion.FechaMod = DateTime.Now;
            _context.Relaciones.Add(relacion);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRelaciones), new { id = relacion.Id }, relacion);
        }
    }
}
