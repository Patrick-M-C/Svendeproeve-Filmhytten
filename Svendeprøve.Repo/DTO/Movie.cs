namespace Svendeprøve.Repo.DTO
{
    public class Movie
    {
        // Filmdata hentet fra TMDB API, inkl. titel, genres, beskrivelse og visuelle detaljer.
        // Bruges kun som DTO og gemmes ikke i databasen for at spare plads.
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

    // Bruges til at deserialisere svar fra TMDBs API, hvor filmene ligger i feltet 'Results'.
    public class TMDBResponse
    {
        public List<Movie> Results { get; set; }
    }
}
