using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Svendeprøve.Repo.DatabaseContext;
using Svendeprøve.Repo.DTO;
using Svendeprøve.Repo.Interface;

namespace Svendeprøve.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketController : Controller
    {
        //private readonly Databasecontext _context;
        private readonly ITicket _ticketRepo;

        public TicketController(/*Databasecontext context,*/ ITicket ticketRepo)
        {
            //_context = context;
            _ticketRepo = ticketRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetTickets()
        {
            var tickets = await _ticketRepo.getAll();
            return Ok(tickets);
            //return await _context.Ticket
            //    .Include(t => t.User)
            //    .Include(t => t.Seat)
            //    //.Include(t => t.Screening)
            //    .ToListAsync();
        }

        [HttpGet("IncludeUserAndSeat")]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetTicketsIncludeUserAndSeat()
        {
            var tickets = await _ticketRepo.getAll();
            return Ok(tickets);
            //return await _context.Ticket
            //    .Include(t => t.User)
            //    .Include(t => t.Seat)
            //    //.Include(t => t.Screening)
            //    .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Ticket>> GetTicketById(int id)
        {
            var ticket = await _ticketRepo.getById(id);
            if (ticket == null)
                return NotFound();

            return Ok(ticket);
            //var ticket = await _context.Ticket
            //    .Include(t => t.User)
            //    .Include(t => t.Seat)
            //    //.Include(t => t.Screening)
            //    .FirstOrDefaultAsync(t => t.Id == id);

            //if (ticket == null)
            //    return NotFound();

            //return Ok(ticket);
        }

        [HttpGet("IncludeUserAndSeat/{id}")]
        public async Task<ActionResult<Ticket>> GetTicketByIdIncludeUserAndSeat(int id)
        {
            var ticket = await _ticketRepo.getById(id);
            if (ticket == null)
                return NotFound();

            return Ok(ticket);
            //var ticket = await _context.Ticket
            //    .Include(t => t.User)
            //    .Include(t => t.Seat)
            //    //.Include(t => t.Screening)
            //    .FirstOrDefaultAsync(t => t.Id == id);

            //if (ticket == null)
            //    return NotFound();

            //return Ok(ticket);
        }

        [HttpPost]
        public async Task<ActionResult<Ticket>> CreateTicket(Ticket ticket)
        {
            if (ticket == null)
                return BadRequest("Ticket cannot be null.");

            var created = await _ticketRepo.create(ticket);
            return CreatedAtAction(nameof(GetTicketById), new { id = created.Id }, created);
            //_context.Ticket.Add(ticket);
            //await _context.SaveChangesAsync();

            //return CreatedAtAction(nameof(GetTicket), new { id = ticket.Id }, ticket);
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
            //if (id != ticket.Id)
            //{
            //    return BadRequest("Ticket id not found.");
            //}

            //var existingTicket = await _context.Ticket.FindAsync(id);
            //if (existingTicket == null)
            //{
            //    return NotFound();
            //}

            //existingTicket.Price = ticket.Price;
            //existingTicket.SeatId = ticket.SeatId;
            //existingTicket.IsCanceled = false;

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!TicketExists(id))
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
        public async Task<IActionResult> DeleteTicket(int id)
        {
            var deleted = await _ticketRepo.delete(id);
            if (deleted == null)
                return NotFound();

            return NoContent();
            //var ticket = await _context.Ticket.FindAsync(id);
            //if (ticket == null)
            //{
            //    return NotFound();
            //}

            //_context.Ticket.Remove(ticket);
            //await _context.SaveChangesAsync();

            //return NoContent();
        }

        //private bool TicketExists(int id)
        //{
        //    return _context.Ticket.Any(e => e.Id == id);
        //}
    }
}