using Microsoft.EntityFrameworkCore;
using Svendeprøve.Repo.DatabaseContext;
using Svendeprøve.Repo.DTO;
using Svendeprøve.Repo.Interface;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;

namespace Svendeprøve.Repo.Repository
{
    public class MovieRepository : IMovie
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://api.themoviedb.org/3";
        private readonly string? _apiKey;
        private readonly string _posterBaseUrl = "https://image.tmdb.org/t/p/w500"; // Base URL for TMDB poster images
        private readonly Databasecontext _dbContext;
        private readonly IConfiguration configuration;

        public MovieRepository(HttpClient httpClient, Databasecontext dbcontext,IConfiguration configuration)
        {
            this.configuration = configuration;
            _httpClient = httpClient;            
            _apiKey = this.configuration.GetSection("TMDB:ApiKey").Value;
            _dbContext = dbcontext;
        }

        public async Task<IEnumerable<Movie>> GetAllMoviesAsync()
        {
            const int maxMovies = 100;
            const int requestDelayMilliseconds = 21;
            const int moviesPerPage = 20;

            var movies = new List<Movie>();
            var genreMappings = await _dbContext.Set<Genre>()
                .ToDictionaryAsync(g => g.id, g => g.name);

            int totalFetched = 0;
            int page = 1;

            while (totalFetched < maxMovies)
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/discover/movie?api_key={_apiKey}&page={page}");
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Error fetching page {page}: {response.StatusCode}");
                    break;
                }

                var tmdbResponse = await response.Content.ReadFromJsonAsync<TMDBResponse>();
                if (tmdbResponse?.Results == null || !tmdbResponse.Results.Any())
                {
                    break;
                }

                var batchMovies = tmdbResponse.Results.Select(tmdbMovie => new Movie
                {
                    Id = tmdbMovie.Id,
                    Title = tmdbMovie.Title,
                    Genres = tmdbMovie.genre_ids != null && tmdbMovie.genre_ids.Any()
                        ? tmdbMovie.genre_ids.Where(id => genreMappings.ContainsKey(id))
                            .Select(id => new Genre { id = id, name = genreMappings[id] })
                            .ToList()
                        : new List<Genre>(),
                    Genre = tmdbMovie.genre_ids != null && tmdbMovie.genre_ids.Any()
                        ? string.Join(", ", tmdbMovie.genre_ids.Select(id => genreMappings.ContainsKey(id) ? genreMappings[id] : "Unknown"))
                        : "Unknown",
                    overview = string.IsNullOrWhiteSpace(tmdbMovie.overview) ? "No description available" : tmdbMovie.overview,
                    vote_average = tmdbMovie.vote_average,
                    release_date = string.IsNullOrEmpty(tmdbMovie.release_date) ? "Unknown" : tmdbMovie.release_date,
                    poster_path = string.IsNullOrEmpty(tmdbMovie.poster_path) ? "No image available" : $"{_posterBaseUrl}{tmdbMovie.poster_path}"
                }).ToList();

                movies.AddRange(batchMovies);
                totalFetched += batchMovies.Count;

                if (totalFetched >= maxMovies)
                {
                    movies = movies.Take(maxMovies).ToList();
                    break;
                }

                page++;
                await Task.Delay(requestDelayMilliseconds);
            }

            return movies;
        }

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/movie/{id}?api_key={_apiKey}");
            if (response.IsSuccessStatusCode)
            {
                var movie = await response.Content.ReadFromJsonAsync<Movie>();
                //var genres = await GetGenreMappingsAsync(); // Fetch genre mappings

                var genreNames = movie.Genres != null && movie.Genres.Any()
                    ? string.Join(", ", movie.Genres.Select(g => g.name))
                    : "Unknown";

                return new Movie
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    Genre = genreNames, // Mapped genre names
                    Genres = movie.Genres, // Original genre objects
                    overview = movie.overview,
                    Runtime = movie.Runtime,
                    vote_average = movie.vote_average,
                    release_date = movie.release_date,
                    poster_path = $"{_posterBaseUrl}{movie.poster_path}"
                };
            }
            return null;
        }        
    }
}
