using System.Text.Json.Serialization;

namespace Svendeprøve.Repo.DTO
{
    // Hall-klassen repræsenterer en biografsal med navn og antal sæder.
    // Forbindelsen til sæder (Seats) muliggør opsætning af siddepladser per sal.
    // Klassen har en én-til-mange-relation til Seat, hvor hver sal kan indeholde flere sæder.
    public class Hall
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int SeatCount { get; set; }
        public List<Seat>? Seats { get; set; }
    }
}
