using Backend_Repositor.io.Data;
using Backend_Repositor.io.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Backend_Repositor.io.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepositoriesController : ControllerBase
    {
        public readonly RepositorDbContext _context;

        public RepositoriesController(RepositorDbContext context) 
        {
            _context = context;
        }

        // GET: api/<RepositoriesController>
        [HttpGet]
        [Route("/{id}")]
        public async Task<IActionResult> GetRepositories([FromRoute] int userId)
        {
            var repos = await _context.Repositories.FindAsync(userId);

            return Ok(repos);
        }

        // POST api/<RepositoriesController>
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> GetRepository([FromBody] Repository repositoryRequest)
        { 

            await _context.Repositories.AddAsync(repositoryRequest);
            await _context.SaveChangesAsync();

            return Ok(repositoryRequest);
        }

        // PUT api/<RepositoriesController>/5
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, Repository updateRepoRequest)
        {
            var repository = await _context.Repositories.FindAsync(id);

            if (repository == null)
            {
                return NotFound();
            }

            repository.Name = updateRepoRequest.Name;
            repository.Description = updateRepoRequest.Description;
            repository.User = updateRepoRequest.User;
            repository.Files = updateRepoRequest.Files;

            await _context.SaveChangesAsync();

            return Ok(repository);
        }

        // DELETE api/<RepositoriesController>/5
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var repository = await _context.Repositories.FindAsync(id);

            if (repository == null)
            {
                return NotFound();
            }

            _context.Repositories.Remove(repository);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
