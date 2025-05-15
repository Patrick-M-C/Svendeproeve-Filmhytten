using Microsoft.EntityFrameworkCore;
using Svendeprøve.Repo.DatabaseContext;
using Svendeprøve.Repo.DTO;
using Svendeprøve.Repo.Interface;

namespace Svendeprøve.Repo.Repository
{
    /*
     * TicketRepository
     * 
     * Ansvarlig for al datatilgang relateret til billetter (Ticket). 
     * Implementerer ITicket-interfacet og anvender Entity Framework til at håndtere CRUD-operationer mod databasen.
     * 
     * Funktionalitet:
     * - Hent alle billetter eller en specifik billet baseret på ID.
     * - Inkluderer relaterede data som tilknyttet bruger og sæde, når det ønskes.
     * - Opretter, opdaterer og sletter billetter i databasen.
     * 
     * Ved at adskille dataadgangslogik fra controllerlaget, sikres en mere testbar og vedligeholdelsesvenlig arkitektur.
     */

    public class TicketRepository : ITicket
    {
        Databasecontext context;
        public TicketRepository(Databasecontext temp)
        {
            context = temp;
        }

        public async Task<List<Ticket>> getAll()
        {
            return await context.Ticket.ToListAsync();
        }
        
        public async Task<List<Ticket>> getAllIncludeUserAndSeat()
        {
            return await context.Ticket
                .Include(t => t.User)
                .Include(t => t.Seat)
                .ToListAsync();
        }

        public async Task<Ticket> getById(int id)
        {
            return await context.Ticket.FirstOrDefaultAsync(t => t.Id == id);
        }
        
        public async Task<Ticket> getByIdIncludeUserAndSeat(int id)
        {
            return await context.Ticket
                .Include(t => t.User)
                .Include(t => t.Seat)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Ticket> create(Ticket ticket)
        {
            context.Ticket.Add(ticket);
            await context.SaveChangesAsync();
            return ticket;
        }

        public async Task<Ticket> update(Ticket updateTicket)
        {
            var TicketUpdate = await context.Ticket.FirstOrDefaultAsync(h => h.Id == updateTicket.Id);
            TicketUpdate.Id = updateTicket.Id;
            TicketUpdate.Price = updateTicket.Price;
            TicketUpdate.UserId = updateTicket.UserId;
            TicketUpdate.SeatId = updateTicket.SeatId;            
            TicketUpdate.IsCanceled = updateTicket.IsCanceled;

            await context.SaveChangesAsync();
            return TicketUpdate;
        }

        public async Task<Ticket> delete(int id)
        {
            var ticket = await context.Ticket.FindAsync(id);
            if (ticket != null)
            {
                context.Ticket.Remove(ticket);
                await context.SaveChangesAsync();
            }
            return ticket;
        }
    }
}
