namespace Svendeprøve.Repo.DTO
{
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

    public class TMDBResponse
    {
        public List<Movie> Results { get; set; }
    }
}
