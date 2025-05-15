using Svendeprøve.Repo.DTO;

namespace Svendeprøve.Repo.Interface
{
    // Dette interface indeholder metoder til at arbejde med Hall-data i systemet.
    // Det bruges til at hente, oprette, opdatere og slette biografsale,
    // samt til at hente sale med deres tilknyttede sæder.
    // Ved at bruge et interface bliver det lettere at udskifte eller teste Hall-logikken uden at ændre resten af koden.

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
