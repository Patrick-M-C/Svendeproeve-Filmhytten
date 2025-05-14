using Microsoft.AspNetCore.Mvc;
using Svendeprøve.Repo.DTO;
using Svendeprøve.Repo.Interface;

namespace Svendeprøve.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketController : Controller
    {        
        private readonly ITicket _ticketRepo;

        public TicketController(ITicket ticketRepo)
        {
            
            _ticketRepo = ticketRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetTickets()
        {
            var tickets = await _ticketRepo.getAll();
            return Ok(tickets);           
        }

        [HttpGet("IncludeUserAndSeat")]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetTicketsIncludeUserAndSeat()
        {
            var tickets = await _ticketRepo.getAll();
            return Ok(tickets);            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Ticket>> GetTicketById(int id)
        {
            var ticket = await _ticketRepo.getById(id);
            if (ticket == null)
                return NotFound();

            return Ok(ticket);            
        }

        [HttpGet("IncludeUserAndSeat/{id}")]
        public async Task<ActionResult<Ticket>> GetTicketByIdIncludeUserAndSeat(int id)
        {
            var ticket = await _ticketRepo.getById(id);
            if (ticket == null)
                return NotFound();

            return Ok(ticket);            
        }

        [HttpPost]
        public async Task<ActionResult<Ticket>> CreateTicket(Ticket ticket)
        {
            if (ticket == null)
                return BadRequest("Ticket cannot be null.");

            var created = await _ticketRepo.create(ticket);
            return CreatedAtAction(nameof(GetTicketById), new { id = created.Id }, created);            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTicket(int id, Ticket ticket)
        {
            if (id != ticket.Id)
                return BadRequest("Ticket ID mismatch.");

            try
            {
                var updated = await _ticketRepo.update(ticket);
                if (updated == null) return NotFound();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return NoContent();            
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            var deleted = await _ticketRepo.delete(id);
            if (deleted == null)
                return NotFound();

            return NoContent();            
        }

        
    }
}