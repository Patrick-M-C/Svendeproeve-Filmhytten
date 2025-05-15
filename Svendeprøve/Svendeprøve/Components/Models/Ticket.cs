using System.Text.Json.Serialization;

namespace Svendeprøve.Components.Models
{
    public class Ticket
    {
        // Billet med pris og reference til bruger og sæde.
        // Forbindelserne UserId og SeatId bruges til at skabe relationer i databasen.
        // IsCanceled markerer om billetten er aktiv eller annulleret.

        // Funktionalitet: (i rækkefølge):
        // Id -> Unikt ID for billetten, typisk fra en database.
        // Price -> Prisen for billetten.
        // UserId -> ID for brugeren, der ejer billetten, bruges til database-relation.
        // User -> Reference til brugerobjektet, ignoreres ved JSON-serialisering.
        // SeatId -> ID for sædet, billetten er knyttet til, bruges til database-relation.
        // Seat -> Reference til sædeobjektet, billetten er knyttet til.
        // IsCanceled -> Angiver, om billetten er annulleret.

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