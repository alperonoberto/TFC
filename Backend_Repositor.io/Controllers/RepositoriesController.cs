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
        public readonly RepositorDbContext context;

        public RepositoriesController(RepositorDbContext context) 
        {
            this.context = context;
        }

        // GET: api/<RepositoriesController>
        [HttpGet]
        [Route("/{id:Guid}")]
        public async Task<IActionResult> GetRepositories([FromRoute] Guid userId)
        {
            var repos = await context.Repositories.FindAsync(userId);

            return Ok(repos);
        }

        // POST api/<RepositoriesController>
        [HttpPost]
        public async Task<IActionResult> GetRepository([FromBody] Repository repositoryRequest)
        {
            repositoryRequest.Id = Guid.NewGuid();

            await context.Repositories.AddAsync(repositoryRequest);
            await context.SaveChangesAsync();

            return Ok(repositoryRequest);
        }

        // PUT api/<RepositoriesController>/5
        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> Put([FromRoute] Guid id, Repository updateRepoRequest)
        {
            var repository = await context.Repositories.FindAsync(id);

            if (repository == null)
            {
                return NotFound();
            }

            repository.Name = updateRepoRequest.Name;
            repository.Description = updateRepoRequest.Description;
            repository.User = updateRepoRequest.User;
            repository.Files = updateRepoRequest.Files;

            await context.SaveChangesAsync();

            return Ok(repository);
        }

        // DELETE api/<RepositoriesController>/5
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var repository = await context.Repositories.FindAsync(id);

            if (repository == null)
            {
                return NotFound();
            }

            context.Repositories.Remove(repository);
            await context.SaveChangesAsync();

            return Ok();
        }
    }
}
