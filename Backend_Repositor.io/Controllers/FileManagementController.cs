using Backend_Repositor.io.Data;
using Backend_Repositor.io.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_Repositor.io.Controllers
{
    public class FileManagementController : Controller
    {
        [Route("api/[controller]")]
        [ApiController]
        public class UsersController : ControllerBase
        {
            private readonly RepositorDbContext _context;
            private readonly IWebHostEnvironment _env;

            public UsersController(RepositorDbContext context, IWebHostEnvironment env)
            {
                _context = context;
                _env = env;
            }

            [HttpGet]
            public async Task<IActionResult> GetAllFiles()
            {
                var files = await _context.Files.ToListAsync();

                return Ok(files);
            }

            [HttpPost]
            [Route("/add")]
            public async Task<IActionResult> AddFile([FromBody] UserFile fileRequest, IFormFile file)
            {
                fileRequest.Id = Guid.NewGuid();

                var path = Path.Combine(_env.ContentRootPath, "Uploads");

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                if (file.Length > 0)
                {
                    var filePath = Path.Combine(path, file.Name);
                    using (var filestream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(filestream);
                    }
                }



                await _context.Files.AddAsync(fileRequest);
                await _context.SaveChangesAsync();

                return Ok(fileRequest);
            }

            [HttpGet]
            [Route("/{id:Guid}")]
            public async Task<IActionResult> GetFile([FromRoute] Guid id)
            {
                var file = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

                if (file == null)
                {
                    return NotFound();
                }

                return Ok(file);
            }

            [HttpPut]
            [Route("/{id:Guid}")]
            public async Task<IActionResult> UpdateFile([FromRoute] Guid id, UserFile updateFileRequest)
            {
                var file = await _context.Files.FindAsync(id);

                if (file == null)
                {
                    return NotFound();
                }

                file.Value = updateFileRequest.Value;
                file.Repository = updateFileRequest.Repository;

                await _context.SaveChangesAsync();

                return Ok(file);
            }

            [HttpDelete]
            [Route("/{id:Guid}")]
            public async Task<IActionResult> DeleteFile([FromRoute] Guid id)
            {
                var file = await _context.Files.FindAsync(id);

                if (file == null)
                {
                    return NotFound();
                }

                _context.Files.Remove(file);
                await _context.SaveChangesAsync();

                return Ok();
            }
        }
    }
}
