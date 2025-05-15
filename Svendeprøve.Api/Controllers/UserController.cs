using Microsoft.AspNetCore.Mvc;
using Svendeprøve.Repo.DTO;
using Svendeprøve.Repo.Interface;

namespace Svendeprøve.Api.Controllers
{
    /*
 * UserController
 * 
 * Giver adgang til API-endpoints relateret til brugeradministration. Funktionerne dækker oprettelse,
 * opdatering, sletning og forespørgsler på brugere – både alene og sammen med deres billetter.
 * 
 * Funktionalitet:
 * - Hent alle brugere (med og uden billetter)
 * - Find bruger baseret på ID, navn eller email
 * - Opret ny bruger
 * - Rediger eksisterende bruger
 * - Slet bruger
 */
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {        
        private readonly IUser _userRepo;     

        public UserController(IUser userRepo)
        {
            _userRepo = userRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _userRepo.GetAllAsync();
            return Ok(users);            
        }

        [HttpGet("withTickets")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsersIncludeTickets()
        {
            var users = await _userRepo.GetAllIncludeUserTicketAsync();
            return Ok(users);            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _userRepo.GetByIdAsync(id);
            if (user == null) return NotFound();

            return Ok(user);            
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
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var deleted = await _userRepo.DeleteAsync(id);
            if (deleted == null) return NotFound();

            return NoContent();            
        }        
    }
}
