namespace Svendeprøve.Components.Models
{
    // Frontend-DTO for vores biograf halls i Blazor-applikationen.
    // Repræsenterer sallens data med detaljer til visning i UI´en og deserialiser API-dataen.

    // Funktionalitet: (i rækkefølge):
    // Id -> Unikt ID for sædet, typisk fra en database.
    // Row -> Rækkenummer, hvor sædet er placeret.
    // SeatsPerRow -> Antal sæder i rækken.
    // IsReserved -> Angiver, om sædet er reserveret.
    // IsSelected -> Angiver, om sædet er valgt af brugeren i UI.
    // SeatNumber -> Sædets nummer i rækken.
    // HallId -> ID for salen, som sædet tilhører.

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