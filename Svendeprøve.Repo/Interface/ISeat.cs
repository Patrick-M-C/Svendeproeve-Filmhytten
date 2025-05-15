using Svendeprøve.Repo.DTO;

namespace Svendeprøve.Repo.Interface
{
    public interface ISeat
    {
        public Task<List<Seat>> getAll();
        public Task<Seat> getById(int id);
        public Task<List<Seat>> getByHallId(int hallId);
        public Task<bool> IsSeatReservedAsync(int seatId);
        public Task<Seat> create(int row, int seatnumber, int hallId);
        public Task<List<Seat>> CreateMultipleAsync(int rows, int seatsPerRow, int hallId);
        public Task<Seat> update(Seat updateSeat);
        public Task<Seat> delete(int id);
    }
}
