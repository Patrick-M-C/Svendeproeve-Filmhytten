using Microsoft.EntityFrameworkCore;
using Svendeprøve.Repo.DatabaseContext;
using Svendeprøve.Repo.DTO;
using Svendeprøve.Repo.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Svendeprøve.Repo.Repository
{
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

        public async Task<Ticket> getById(int id)
        {
            return await context.Ticket.FirstOrDefaultAsync(t => t.Id == id);
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
            TicketUpdate.ScreeningId = updateTicket.ScreeningId;
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
