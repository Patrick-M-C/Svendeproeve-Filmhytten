using System.Text.Json.Serialization;

namespace Svendeprøve.Repo.DTO
{
    public class Ticket
    {
        public int Id { get; set; }
        public int Price { get; set; }
        
        public int UserId { get; set; }

        [JsonIgnore]
        public User? User { get; set; }

        public int SeatId { get; set; }

        public Seat? Seat { get; set; }

        public bool IsCanceled { get; set; }
    }
}
