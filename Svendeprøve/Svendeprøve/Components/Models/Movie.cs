using Svendeprøve.Repo.DTO;

namespace Svendeprøve.Components.Models
{
    // Frontend-DTO for vores film class i vores Blazor-applikation.
    // Repræsenterer vores film class med data til visning i UI´en og deserialiser API-dataen.

    // Funktionalitet: (i rækkefølge):
    // Id -> Unikt ID for filmen, typisk fra en database eller API.
    // Title -> Filmens titel.
    // Genres -> Liste af genrer som objekter, kan være null.
    // Genre -> Genre som tekststreng, kan være null.
    // genre_ids -> Liste af genre-ID'er, kan være null.
    // overview -> Beskrivelse af filmens handling.
    // Runtime -> Spilletid i minutter, kan være null.
    // vote_average -> Gennemsnitlig brugerbedømmelse.
    // release_date -> Udgivelsesdato som tekst.
    // poster_path -> Sti til filmens plakatbillede.

    public class Movie
    {    
        public int Id { get; set; }
        public string Title { get; set; }
        public List<Genre>? Genres { get; set; }
        public string? Genre { get; set; }
        public List<int>? genre_ids { get; set; }
        public string overview { get; set; }
        public int? Runtime { get; set; }
        public double vote_average { get; set; }
        public string release_date { get; set; }
        public string poster_path { get; set; }
    }
}