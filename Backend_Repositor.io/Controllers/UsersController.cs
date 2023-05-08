using Backend_Repositor.io.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend_Repositor.io.Models;

namespace Backend_Repositor.io.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly RepositorDbContext _context;

        public UsersController(RepositorDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _context.Users.ToListAsync();

            return Ok(users);
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddUser([FromBody] User userRequest)
        {


            await _context.Users.AddAsync(userRequest);
            await _context.SaveChangesAsync();

            return Ok(userRequest);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetUser([FromRoute] int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateUser([FromRoute] int id, User updateUserRequest)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            user.Name = updateUserRequest.Name;
            user.Email = updateUserRequest.Email;
            user.Password = updateUserRequest.Password;
            user.FriendList = updateUserRequest.FriendList;
            user.Repositories = updateUserRequest.Repositories;

            await _context.SaveChangesAsync();

            return Ok(user);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
