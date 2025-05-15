using Microsoft.EntityFrameworkCore;
using Svendeprøve.Repo.DatabaseContext;
using Svendeprøve.Repo.DTO;
using Svendeprøve.Repo.Interface;

namespace Svendeprøve.Repo.Repository
{
    /*
     * SeatRepository
     * 
     * Denne repository-klasse håndterer al datatilgang relateret til sæder (Seat).
     * Den implementerer ISeat-interfacet og benytter Entity Framework til at foretage databaseoperationer.
     * 
     * Funktionalitet:
     * - Henter alle sæder eller specifikke sæder baseret på ID eller HallId.
     * - Opretter et enkelt sæde eller flere sæder samtidig (fx ved oprettelse af en hel sal).
     * - Opdaterer eksisterende sædeinformationer.
     * - Sletter sæder fra databasen.
     * - Validerer om et sæde er reserveret via `IsSeatReservedAsync`. 
     *
     */

    public class SeatRepository : ISeat
    {
        Databasecontext context;

        public SeatRepository(Databasecontext _context)
        {
            context = _context;
        }
        public async Task<List<Seat>> getAll()
        {
            return await context.Seat.ToListAsync();
        }

        public async Task<Seat> getById(int id)
        {
            return await context.Seat.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<List<Seat>> getByHallId(int hallId)
        {
            return await context.Seat.Where(s => s.HallId == hallId).ToListAsync();
        }

        public async Task<bool> IsSeatReservedAsync(int seatId)
        {
            var seat = await context.Seat.FindAsync(seatId);
            if (seat == null)
            {
                throw new KeyNotFoundException($"Seat with ID {seatId} not found.");
            }
            return seat.IsReserved;
        }

        public async Task<Seat> create(int row, int seatnumber, int hallId)
        {
            var seat = new Seat
            {
                Id = 0,
                Row = row,
                SeatNumber = seatnumber,
                IsReserved = false,
                HallId = hallId
            };

            context.Seat.Add(seat);

            await context.SaveChangesAsync();

            return seat;
        }

        public async Task<List<Seat>> CreateMultipleAsync(int rows, int seatsPerRow, int hallId)
        {
            var seats = new List<Seat>();
            int seatId = 1;

            for (int row = 1; row <= rows; row++)
            {
                for (int seatNumber = 1; seatNumber <= seatsPerRow; seatNumber++)
                {
                    seats.Add(new Seat
                    {
                        Id = 0,
                        Row = row,
                        SeatNumber = seatNumber,
                        IsReserved = false,
                        HallId = hallId
                    });
                }
            }

            context.Seat.AddRange(seats); 
            await context.SaveChangesAsync(); 

            return seats;
        }


        public async Task<Seat> delete(int id)
        {
            var seat = await context.Seat.FindAsync(id);

            if (seat != null)
            {
                context.Seat.Remove(seat);
                await context.SaveChangesAsync();
            }
            return seat;
        }

        public async Task<Seat> update(Seat updateSeat)
        {
            var SeatUpdate = await context.Seat.FirstOrDefaultAsync(s => s.Id == updateSeat.Id);

            SeatUpdate.Id = updateSeat.Id;
            SeatUpdate.Row = updateSeat.Row;
            SeatUpdate.IsReserved = updateSeat.IsReserved;
            SeatUpdate.SeatNumber = updateSeat.SeatNumber;
            SeatUpdate.HallId = updateSeat.HallId;

            await context.SaveChangesAsync();
            return SeatUpdate;
        }
    }
}
