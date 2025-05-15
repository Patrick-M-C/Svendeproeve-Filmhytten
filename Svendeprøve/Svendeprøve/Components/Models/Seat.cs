namespace Svendeprøve.Components.Models
{
    // Frontend-DTO for vores seat class i vores Blazor-applikation.
    // Repræsenterer vores seat class med data til visning i UI´en og deserialiser API-dataen.
    public class Seat
    {
        public int Id { get; set; }
        public int Row { get; set; }
        public int SeatsPerRow { get; set; }
        public bool IsReserved { get; set; }
        public bool IsSelected { get; set; }
        public int SeatNumber { get; set; }
        public int HallId { get; set; }
    }

}