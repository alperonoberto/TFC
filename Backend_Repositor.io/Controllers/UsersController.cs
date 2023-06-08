using Backend_Repositor.io.Data;
using Backend_Repositor.io.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

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

        [HttpGet("username/{username}")]
        public async Task<ActionResult<User>> GetUsuarioByUsername(string username)
        {
            var usuario = await _context.Users.Where(u => u.Username == username).FirstOrDefaultAsync();

            if (usuario == null)
            {
                return NotFound("El usuario no existe");
            }

            return usuario;
        }

        [HttpGet("encrypt")]
        public async Task<string> GetUsuarioEncrypted(string login)
        {
            var enc = Encryption.EncodePassword(login);
            return JsonSerializer.Serialize(enc);   
        }

        // POST api/<UsersController>
        [HttpPost]
        [Route("add")]
        public async Task<ActionResult<User>> Post([FromBody] User usuario)
        {
            var user = await _context.Users.FindAsync(usuario);

            if(user.Username != "")
            {
                return BadRequest("El usuario ya existe");
            }

            usuario.FechaAlta = DateTime.Now;
            usuario.Password = Encryption.EncodePassword(usuario.Password);
            _context.Users.Add(usuario);
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
            var relaciones = await _context.Relaciones.Where(r => (r.SeguidorId == id || r.SeguidoId == id)).ToListAsync();
            var usuario = await _context.Users.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            foreach(var rel in relaciones)
            {
                _context.Relaciones.Remove(rel);
            }

            _context.Users.Remove(usuario);
            await _context.SaveChangesAsync();

            return Ok("Usuario borrado con exito");
        }

    }

    public class Encryption
    {
        public static string EncodePassword(string password)
        {
            SHA256 sha256 = SHA256.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(password));

            for(var i = 0; i < stream.Length; i++)
            {
                sb.AppendFormat("{0:x2}", stream[i]);
            }

            return sb.ToString();
        }
    }
}
