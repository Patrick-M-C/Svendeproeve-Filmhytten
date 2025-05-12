using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Svendeprøve.Repo.DatabaseContext;
using Svendeprøve.Repo.DTO;
using Svendeprøve.Repo.Interface;

namespace Svendeprøve.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        //private readonly Databasecontext _context;
        //private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IUser _userRepo;

        //public UserController(Databasecontext context /*, IPasswordHasher<User> passwordHasher*/)
        //{
        //    _context = context;
        //    //_passwordHasher = passwordHasher;
        //}

        public UserController(IUser userRepo)
        {
            _userRepo = userRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _userRepo.GetAllAsync();
            return Ok(users);
            //return await _context.User.ToListAsync();
        }

        [HttpGet("withTickets")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsersIncludeTickets()
        {
            var users = await _userRepo.GetAllIncludeUserTicketAsync();
            return Ok(users);
            //return await _context.User.Include(u => u.Tickets).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _userRepo.GetByIdAsync(id);
            if (user == null) return NotFound();

            return Ok(user);
            //var user = await _context.User.FindAsync(id);

            //if (user == null)
            //{
            //    return NotFound();
            //}
            //return user;
        }

        [HttpGet("byName/{name}")]
        public async Task<ActionResult<User>> GetByName(string name)
        {
            var user = await _userRepo.GetByNameAsync(name);
            if (user == null) return NotFound();

            return Ok(user);
        }

        [HttpGet("byEmail/{email}")]
        public async Task<ActionResult<User>> GetByEmail(string email)
        {
            var user = await _userRepo.GetByEmailAsync(email);
            if (user == null) return NotFound();

            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(User user)
        {
            if (user == null)
                return BadRequest("User cannot be null.");

            user.Tickets = null;

            var created = await _userRepo.CreateAsync(user);
            return CreatedAtAction(nameof(GetUser), new { id = created.Id }, created);
            //if (user == null) return BadRequest("User cannot be null.");

            //user.Tickets = null;
            ////user.Password = _passwordHasher.HashPassword(user, user.Password);

            //_context.User.Add(user);
            //await _context.SaveChangesAsync();

            //return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, User user)
        {
            if (id != user.Id)
                return BadRequest("User ID mismatch.");

            try
            {
                var updated = await _userRepo.UpdateAsync(user);
                if (updated == null) return NotFound();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return NoContent();
            //if (id != user.Id)
            //{
            //    return BadRequest("User ID mismatch.");
            //}

            //var existingUser = await _context.User.FindAsync(id);
            //if (existingUser == null)
            //{
            //    return NotFound();
            //}

            //existingUser.Name = user.Name;
            //existingUser.Email = user.Email;
            //existingUser.PhoneNumber = user.PhoneNumber;

            //existingUser.Password = user.Password;
            ////existingUser.Password = _passwordHasher.HashPassword(user, user.Password);

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!UserExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}
            //return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var deleted = await _userRepo.DeleteAsync(id);
            if (deleted == null) return NotFound();

            return NoContent();
            //var user = await _context.User.FindAsync(id);
            //if (user == null)
            //{
            //    return NotFound();
            //}
            //_context.User.Remove(user);
            //await _context.SaveChangesAsync();

            //return NoContent();
        }

        //private bool UserExists(int id)
        //{
        //    return _context.User.Any(e => e.Id == id);
        //}
    }
}
