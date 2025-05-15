using Svendeprøve.Repo.DTO;
using Svendeprøve.Repo.Interface;
using Svendeprøve.Repo.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Svendeprøve.Repo.Repository
{
    /*
     * HallRepository
     * 
     * Repository-klassen håndterer datatilgang og CRUD-operationer for Hall-entiteten.
     * Den implementerer IHall-interfacet og giver en struktureret tilgang til at interagere med databasen gennem Entity Framework Core.
     * 
     * Funktionalitet:
     * - Hent alle sale, med eller uden tilknyttede sæder.
     * - Hent en enkelt sal baseret på ID, med mulighed for at inkludere sæder.
     * - Opret en ny sal i databasen.
     * - Opdater eksisterende sal baseret på ID.
     * - Slet en sal.
     * 
     */
    public class HallRepository : IHall
    {
        Databasecontext context;
        public HallRepository(Databasecontext temp)
        {
            context = temp;
        }
        public async Task<List<Hall>> getAll()
        {
            return await context.Hall.ToListAsync();
        }

        public async Task<List<Hall>> getAllIncludeSeats()
        {
            return await context.Hall.Include(s => s.Seats).ToListAsync();
        }

        public async Task<Hall> getById(int id)
        {
            return await context.Hall.FirstOrDefaultAsync(h => h.Id == id); 
        }

        public async Task<Hall> getByIdIncludeSeats(int id)
        {
            return await context.Hall.Include(h => h.Seats).FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task<Hall> create(Hall hall)
        {
            if (hall == null)
                throw new ArgumentNullException(nameof(hall));

            context.Hall.Add(hall);
            await context.SaveChangesAsync();

            return hall;
        }

        public async Task<Hall> delete(int id)
        {
            var hall = await context.Hall.FindAsync(id);
            if (hall != null)
            {
                context.Hall.Remove(hall);
                await context.SaveChangesAsync();
            }
            return hall;
        }

        public async Task<Hall> update(Hall updateHall)
        {
            var HallUpdate = await context.Hall.FirstOrDefaultAsync(h => h.Id == updateHall.Id);
            if (HallUpdate == null) return null;

            HallUpdate.Id = updateHall.Id;
            HallUpdate.SeatCount = updateHall.SeatCount;
            HallUpdate.Name = updateHall.Name;

            await context.SaveChangesAsync();
            return HallUpdate;
        }
    }
}
