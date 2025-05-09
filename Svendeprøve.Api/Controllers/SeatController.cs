using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Svendeprøve.Repo.DatabaseContext;
using Svendeprøve.Repo.DTO;
using Svendeprøve.Repo.Interface;

namespace Svendeprøve.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SeatController : ControllerBase
    {
        private readonly Databasecontext _context;
        private readonly ISeat _seatRepo;

        public SeatController(Databasecontext context, ISeat seatRepo)
        {
            _context = context;
            _seatRepo = seatRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Seat>>> GetSeats()
        {
            return await _context.Seat.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Seat>> GetSeat(int id)
        {
            var seat = await _context.Seat.FindAsync(id);

            if (seat == null)
            {
                return NotFound();
            }

            return seat;
        }

        [HttpGet("ByHall/{hallId}")]
        public async Task<ActionResult<IEnumerable<Seat>>> GetSeatsByHall(int hallId)
        {
            return await _context.Seat.Where(s => s.HallId == hallId).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Seat>> CreateSeat(Seat seat)
        {
            _context.Seat.Add(seat);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSeat), new { id = seat.Id }, seat);
        }

        [HttpPost("multiple")]
        public async Task<ActionResult<IEnumerable<Seat>>> CreateMultipleSeats(
        [FromQuery] int rows,
        [FromQuery] int seatsPerRow,
        [FromQuery] int hallId)
        {
            if (rows <= 0 || seatsPerRow <= 0)
                return BadRequest("Rows and seats per row must be greater than 0.");

            var seats = await _seatRepo.CreateMultipleAsync(rows, seatsPerRow, hallId);

            return Ok(seats);
        }

        [HttpGet("isReserved")]
        public async Task<ActionResult<IEnumerable<Seat>>> GetReservedSeats()
        {
            var reservedSeats = await _context.Seat
                .Where(s => s.IsReserved == true)
                .ToListAsync();

            return Ok(reservedSeats);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSeat(int id, Seat seat)
        {
            if (id != seat.Id)
            {
                return BadRequest();
            }

            _context.Entry(seat).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SeatExists(id))
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
        public async Task<IActionResult> DeleteSeat(int id)
        {
            var seat = await _context.Seat.FindAsync(id);
            if (seat == null)
            {
                return NotFound();
            }

            _context.Seat.Remove(seat);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SeatExists(int id)
        {
            return _context.Seat.Any(e => e.Id == id);
        }
    }
}
