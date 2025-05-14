using Microsoft.AspNetCore.Mvc;
using Svendeprøve.Repo.DTO;
using Svendeprøve.Repo.Interface;

namespace Svendeprøve.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SeatController : ControllerBase
    {
        private readonly ISeat _seatRepo;

        public SeatController(ISeat seatRepo)
        {            
            _seatRepo = seatRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Seat>>> GetSeats()
        {
            var seats = await _seatRepo.getAll();
            return Ok(seats);            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Seat>> GetSeat(int id)
        {
            var seat = await _seatRepo.getById(id);
            if (seat == null)
                return NotFound();

            return Ok(seat);            
        }

        [HttpGet("ByHall/{hallId}")]
        public async Task<ActionResult<IEnumerable<Seat>>> GetSeatsByHall(int hallId)
        {
            var seats = await _seatRepo.getByHallId(hallId);
            return Ok(seats);            
        }

        [HttpPost]
        public async Task<ActionResult<Seat>> CreateSeat(Seat seat)
        {
            var created = await _seatRepo.create(seat.Row, seat.SeatNumber, seat.HallId);
            return CreatedAtAction(nameof(GetSeat), new { id = created.Id }, created);            
        }

        [HttpPost("multiple")]
        public async Task<ActionResult<List<Seat>>> CreateMultipleSeats([FromQuery] int rows, [FromQuery] int seatsPerRow, [FromQuery] int hallId)
        {
            var createdSeats = await _seatRepo.CreateMultipleAsync(rows, seatsPerRow, hallId);
            return Ok(createdSeats);
        }
        //[HttpPost("multiple")]
        //public async Task<ActionResult<IEnumerable<Seat>>> CreateMultipleSeats(
        //[FromQuery] int rows,
        //[FromQuery] int seatsPerRow,
        //[FromQuery] int hallId)
        //{
        //    if (rows <= 0 || seatsPerRow <= 0)
        //        return BadRequest("Rows and seats per row must be greater than 0.");

        //    var seats = await _seatRepo.CreateMultipleAsync(rows, seatsPerRow, hallId);

        //    return Ok(seats);
        //}

        [HttpGet("isReserved")]
        public async Task<ActionResult<IEnumerable<Seat>>> GetReservedSeats()
        {
            var allSeats = await _seatRepo.getAll();
            var reservedSeats = allSeats.Where(s => s.IsReserved).ToList();

            return Ok(reservedSeats);            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSeat(int id, Seat seat)
        {
            if (id != seat.Id)
                return BadRequest();

            var updated = await _seatRepo.update(seat);
            if (updated == null)
                return NotFound();

            return NoContent();            
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSeat(int id)
        {
            var deleted = await _seatRepo.delete(id);
            if (deleted == null)
                return NotFound();

            return NoContent();            
        }       
    }
}
