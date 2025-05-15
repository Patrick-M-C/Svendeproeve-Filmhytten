using System.Text.Json.Serialization;

namespace Svendeprøve.Components.Models
{
    public class Ticket
    {
        // Billet med pris og reference til bruger og sæde.
        // Forbindelserne UserId og SeatId bruges til at skabe relationer i databasen.
        // IsCanceled markerer om billetten er aktiv eller annulleret.
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