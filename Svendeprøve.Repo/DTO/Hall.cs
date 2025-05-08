using System.Text.Json.Serialization;

namespace Svendeprøve.Repo.DTO
{
    public class Hall
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int SeatCount { get; set; }
        public List<Seat>? Seats { get; set; }
    }
}
