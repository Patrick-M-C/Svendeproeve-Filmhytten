using Svendeprøve.Repo.DTO;

namespace Svendeprøve.Repo.Interface
{
    public interface ITicket
    {
        public Task<List<Ticket>> getAll();
        public Task<List<Ticket>> getAllIncludeUserAndSeat();
        public Task<Ticket> getById(int id);
        public Task<Ticket> getByIdIncludeUserAndSeat(int id);
        public Task<Ticket> create(Ticket ticket);
        public Task<Ticket> update(Ticket updateTicket);
        public Task<Ticket> delete(int id);
    }
}
