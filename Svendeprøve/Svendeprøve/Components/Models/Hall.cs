namespace Svendeprøve.Components.Models
{
    // Frontend-DTO for vores biograf halls i Blazor-applikationen.
    // Repræsenterer sallens data med detaljer til visning i UI´en og deserialiser API-dataen.

    // Funktionalitet: (i rækkefølge):
    // Id -> Unikt ID for salen, typisk fra en database eller API.
    // Name -> Navnet på salen, f.eks. "Sal 1".
    // SeatCount -> Antal sæder i salen (kapacitet).
    // List<Seat>? -> Liste af sædeobjekter, kan være null.
    public class Hall
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SeatCount { get; set; }
        public List<Seat>? Seats { get; set; }

    }

}
