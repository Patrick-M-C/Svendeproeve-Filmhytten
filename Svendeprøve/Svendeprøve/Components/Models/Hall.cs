namespace Svendeprøve.Components.Models
{
    // Frontend-DTO for vores biograf halls i Blazor-applikationen.
    // Repræsenterer sallens data med detaljer til visning i UI´en og deserialiser API-dataen.
    public class Hall
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SeatCount { get; set; }
        public List<Seat>? Seats { get; set; }

    }

}
