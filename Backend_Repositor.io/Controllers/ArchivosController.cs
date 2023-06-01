using Backend_Repositor.io.Data;
using Backend_Repositor.io.Interfaces;
using Backend_Repositor.io.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Backend_Repositor.io.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArchivosController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IFileService _fileService;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public ArchivosController(AppDbContext context, IFileService fileService, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _fileService = fileService;
            _hostingEnvironment = hostingEnvironment;
        }

        #region Upload File
        [HttpPost("upload/{usuario}/{repoName}")]
        public IActionResult Upload([FromRoute] string usuario, [FromRoute] string repoName)
        {
            try
            {
                var files = Request.Form.Files.ToList();
                var repoBien = repoName.Replace("_", " ");
                var username = _context.Users.Where(u => u.Id == Convert.ToInt32(usuario)).First();
                var directory = "Repositorios\\" + username.Username + "\\" + repoName;
                var fullDirectory = Path.Combine(_hostingEnvironment.WebRootPath, directory);
                Repositorio repositorio = _context.Repositorios.Where(r => r.Nombre == repoBien).FirstOrDefault();

                if (repositorio == null)
                {
                    return BadRequest("El repositorio no existe");
                }

                files.ForEach(f =>
                {
                    var dbFile = new Archivo();

                    dbFile.Filename = f.FileName;
                    dbFile.Filepath = Path.Combine(fullDirectory, f.FileName);
                    dbFile.FechaSubida = DateTime.Now;
                    dbFile.RepositorioId = repositorio.Id;

                    _context.Archivos.Add(dbFile);
                    _context.SaveChanges();
                });


                _fileService.UploadFiles(files, directory);

                return Ok($"Files: {files.Count} Size: {_fileService.SizeConverter(files.Sum(f => f.Length))}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex + " Error al subir archivos");
            }
        }
        #endregion

        #region Download File  
        [HttpGet("download")]
        public IActionResult Download([Required] string subDirectory)
        {

            try
            {
                var (fileType, archiveData, archiveName) = _fileService.DownloadFiles(subDirectory);

                return File(archiveData, fileType, archiveName);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        #endregion


        #region Metodos basicos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Archivo>>> GetArchivos()
        {
            var archivos = await _context.Archivos.ToListAsync();
            return archivos;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Archivo>> GetArchivo(long id)
        {
            var archivo = await _context.Archivos.FindAsync(id);

            if (archivo == null)
            {
                return NotFound("El archivo no existe");
            }

            return archivo;
        }

        [HttpGet]
        [Route("repositorio/{id}")]
        public async Task<ActionResult<IEnumerable<Archivo>>> GetArchivosByRepo(long id)
        {
            var archivos = await _context.Archivos.Where(a => a.RepositorioId == id).ToListAsync();
            return archivos;
        }

        [HttpPost]
        [Route("add")]
        public async Task<ActionResult<Archivo>> PostArchivo([FromBody] Archivo archivo)
        {
            archivo.FechaSubida = DateTime.Now;
            _context.Archivos.Add(archivo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetArchivo), new { id = archivo.Id }, archivo);
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Put(Archivo archivo)
        {
            _context.Entry(archivo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArchivoExists(archivo.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok("Archivo actualizado con exito");
        }

        private bool ArchivoExists(long id)
        {
            return _context.Archivos.Any(u => u.Id == id);
        }

        // DELETE api/<UsersController>/5
        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] long id)
        {
            var archivo = await _context.Archivos.FindAsync(id);

            if (archivo == null)
            {
                return NotFound();
            }

            _context.Archivos.Remove(archivo);
            await _context.SaveChangesAsync();

            return Ok("Archivo borrado con exito");
        }
        #endregion
    }
}
