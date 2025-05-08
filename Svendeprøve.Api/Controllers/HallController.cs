using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Svendeprøve.Repo.DatabaseContext;
using Svendeprøve.Repo.DTO;
using Svendeprøve.Repo.Interface;

namespace Svendeprøve.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HallController : ControllerBase
    {
        private readonly Databasecontext _context;
        private readonly IHall _hallRepo;

        public HallController(Databasecontext context, IHall hall)
        {
            _context = context;
            _hallRepo = hall;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hall>>> GetHalls()
        {
            return await _context.Hall.ToListAsync();
        }
        
        [HttpGet("withSeat")]
        public async Task<ActionResult<IEnumerable<Hall>>> GetHallsWithSeats()
        {
            return await _context.Hall.Include(h => h.Seats).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Hall>> GetHallbyid(int id)
        {
            var hall = await _context.Hall.FindAsync(id);

            if (hall == null)
            {
                return NotFound();
            }

            return hall;
        }

        [HttpGet("withseat/{id}")]
        public async Task<ActionResult<Hall>> GetHallWithSeatsbyid(int id)
        {
            var hall = await _context.Hall.Include(h => h.Seats).FirstOrDefaultAsync(h => h.Id == id);

            if (hall == null)
            {
                return NotFound();
            }

            return hall;
        }

        [HttpPost]
        public async Task<ActionResult<Hall>> CreateHall(Hall hall)
        {
            _context.Hall.Add(hall);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetHallbyid), new { id = hall.Id }, hall);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHall(int id, Hall hall)
        {
            if (id != hall.Id)
            {
                return BadRequest();
            }

            _context.Entry(hall).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HallExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHall(int id)
        {
            var hall = await _context.Hall.FindAsync(id);
            if (hall == null)
            {
                return NotFound();
            }

            _context.Hall.Remove(hall);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HallExists(int id)
        {
            return _context.Hall.Any(e => e.Id == id);
        }
    }
}
