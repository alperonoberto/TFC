using Backend_Repositor.io.Data;
using Backend_Repositor.io.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

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

    public class PasswordEncoderDecoder
    {
        public static (string encryptedPassword, string decryptionKey) EncodePassword(string password)
        {
            // Generar una clave aleatoria para el desencriptado
            byte[] key = new byte[32];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(key);
            }
            string decryptionKey = Convert.ToBase64String(key);

            // Codificar la contraseña
            using (var aes = new AesCryptoServiceProvider())
            {
                aes.GenerateIV();
                aes.Key = key;
                byte[] encryptedBytes;

                using (var encryptor = aes.CreateEncryptor())
                {
                    byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                    encryptedBytes = encryptor.TransformFinalBlock(passwordBytes, 0, passwordBytes.Length);
                }

                string encryptedPassword = Convert.ToBase64String(encryptedBytes);
                return (encryptedPassword, decryptionKey);
            }
        }

        public static string DecodePassword(string encryptedPassword, string decryptionKey)
        {
            // Decodificar la cadena base64 del password encriptado y la clave de desencriptado
            byte[] encryptedBytes = Convert.FromBase64String(encryptedPassword);
            byte[] key = Convert.FromBase64String(decryptionKey);

            // Desencriptar la contraseña
            using (var aes = new AesCryptoServiceProvider())
            {
                aes.Key = key;
                byte[] decryptedBytes;

                using (var decryptor = aes.CreateDecryptor())
                {
                    decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
                }

                string decryptedPassword = Encoding.UTF8.GetString(decryptedBytes);
                return decryptedPassword;
            }
        }
    }
}
