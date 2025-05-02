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
    public class SeatRepository : ISeat
    {
        Databasecontext context;

        public SeatRepository(Databasecontext _context)
        {
            context = _context;
        }
        public Task<Seat> create(int row, int seatnumber, int hallId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Seat>> CreateMultipleAsync(int rows, int seatsPerRow, int hallId)
        {
            throw new NotImplementedException();
        }

        public Task<Seat> delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Seat>> getAll()
        {
            throw new NotImplementedException();
        }

        public Task<List<Seat>> getByHallId(int hallId)
        {
            throw new NotImplementedException();
        }

        public Task<Seat> getById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsSeatReservedAsync(int seatId)
        {
            throw new NotImplementedException();
        }

        public Task<Seat> update(Seat updateSeat)
        {
            throw new NotImplementedException();
        }
    }
}
