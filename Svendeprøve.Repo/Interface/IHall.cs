using Svendeprøve.Repo.DTO;

namespace Svendeprøve.Repo.Interface
{
    public interface IHall
    {
        public Task<List<Hall>> getAll();
        public Task<List<Hall>> getAllIncludeSeats();
        public Task<Hall> getById(int id);
        public Task<Hall> getByIdIncludeSeats(int id);
        public Task<Hall> create(Hall hall);
        public Task<Hall> update(Hall updateHall);
        public Task<Hall> delete(int id);
    }
}
