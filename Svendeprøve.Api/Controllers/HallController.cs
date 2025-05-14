using Microsoft.AspNetCore.Mvc;
using Svendeprøve.Repo.DTO;
using Svendeprøve.Repo.Interface;

namespace Svendeprøve.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HallController : ControllerBase
    {
        
        private readonly IHall _hallRepo;

        public HallController(IHall hall)
        {
            
            _hallRepo = hall;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hall>>> GetHalls()
        {            
            var halls = await _hallRepo.getAll();
            return Ok(halls);
        }
        
        [HttpGet("withSeat")]
        public async Task<ActionResult<IEnumerable<Hall>>> GetHallsWithSeats()
        {
            var halls = await _hallRepo.getAllIncludeSeats();
            return Ok(halls);            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Hall>> GetHallbyid(int id)
        {
            var hall = await _hallRepo.getById(id);
            if (hall == null)
                return NotFound();

            return Ok(hall);            
        }

        [HttpGet("withseat/{id}")]
        public async Task<ActionResult<Hall>> GetHallWithSeatsbyid(int id)
        {
            var hall = await _hallRepo.getByIdIncludeSeats(id);
            if (hall == null)
                return NotFound();

            return Ok(hall);            
        }

        [HttpPost]
        public async Task<ActionResult<Hall>> CreateHall(Hall hall)
        {
            var created = await _hallRepo.create(hall);
            return CreatedAtAction(nameof(GetHallbyid), new { id = created.Id }, created);            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHall(int id, Hall hall)
        {
            if (id != hall.Id)
                return BadRequest();

            var updated = await _hallRepo.update(hall);
            if (updated == null)
                return NotFound();

            return NoContent();            
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHall(int id)
        {
            var deleted = await _hallRepo.delete(id);
            if (deleted == null)
                return NotFound();

            return NoContent();            
        }        
    }
}
