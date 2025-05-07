using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Svendeprøve.Repo.DatabaseContext;
using Svendeprøve.Repo.DTO;
using Svendeprøve.Repo.Interface;

namespace Svendeprøve.Api.Controllers
{
    public class SeatController : Controller
    {
        private readonly Databasecontext context;
        private readonly ISeat _seatRepo;

        public SeatController(Databasecontext context, ISeat seatRepo)
        {
            context = context;
            _seatRepo = seatRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Seat>>> GetSeats()
        {
            return await context.Seat.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Seat>> GetSeat(int id)
        {
            var seat = await context.Seat.FindAsync(id);

            if (seat == null)
            {
                return NotFound();
            }

            return seat;
        }

        [HttpGet("ByHall/{hallId}")]
        public async Task<ActionResult<IEnumerable<Seat>>> GetSeatsByHall(int hallId)
        {
            return await context.Seat.Where(s => s.HallId == hallId).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Seat>> CreateSeat(Seat seat)
        {
            context.Seat.Add(seat);
            await context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSeat), new { id = seat.Id }, seat);
        }

        [HttpGet("isreserved/{seatId}")]
        public async Task<ActionResult<bool>> IsSeatReserved(int seatId)
        {
            try
            {
                bool isReserved = await _seatRepo.IsSeatReservedAsync(seatId);
                return Ok(isReserved);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message); // 404 if seat is not found
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSeat(int id, Seat seat)
        {
            if (id != seat.Id)
            {
                return BadRequest();
            }

            context.Entry(seat).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
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
            var seat = await context.Seat.FindAsync(id);
            if (seat == null)
            {
                return NotFound();
            }

            context.Seat.Remove(seat);
            await context.SaveChangesAsync();

            return NoContent();
        }

        private bool SeatExists(int id)
        {
            return context.Seat.Any(e => e.Id == id);
        }
    }
}
