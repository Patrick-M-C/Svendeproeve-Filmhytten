using Microsoft.EntityFrameworkCore;
using Svendeprøve.Repo.DatabaseContext;
using Svendeprøve.Repo.DTO;
using Svendeprøve.Repo.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            //_apiKey = Key.APIKey;
            _apiKey = this.configuration.GetSection("TMDB:ApiKey").Value;
            _dbContext = dbcontext;
        }

        public async Task<IEnumerable<Movie>> GetAllMoviesAsync()
        {
            // Fetch the list of movies using the TMDB discover endpoint
            var response = await _httpClient.GetAsync($"{_baseUrl}/discover/movie?api_key={_apiKey}");
            if (response.IsSuccessStatusCode)
            {
                // Parse the response into TMDBMovieResponse object
                //var tmdbResponse = await response.Content.ReadFromJsonAsync<TMDBMovieResponse>(); // Removed
                var tmdbResponse = await response.Content.ReadFromJsonAsync<TMDBResponse>();
                if (tmdbResponse?.Results != null && tmdbResponse.Results.Any())
                {
                    // Fetch genre mappings from the database
                    var genreMappings = await _dbContext.Set<Genre>()
                        .ToDictionaryAsync(g => g.id, g => g.name);

                    // Map TMDBMovie to your Movie model
                    return tmdbResponse.Results.Select(tmdbMovie => new Movie
                    {
                        Id = tmdbMovie.Id,
                        Title = tmdbMovie.Title,
                        //Converting GenreIds into  readable genre names
                        Genres = tmdbMovie.genre_ids != null && tmdbMovie.genre_ids.Any()
                            ? tmdbMovie.genre_ids.Where(id => genreMappings.ContainsKey(id))
                        .Select(id => new Genre { id = id, name = genreMappings[id] })
                        .ToList()
                        : new List<Genre>(),

                        // Create a comma-separated string for Genre
                        Genre = tmdbMovie.genre_ids != null && tmdbMovie.genre_ids.Any()
                            ? string.Join(", ", tmdbMovie.genre_ids.Select(id => genreMappings.ContainsKey(id) ? genreMappings[id] : "Unknown"))
                            : "Unknown",
                        overview = string.IsNullOrWhiteSpace(tmdbMovie.overview) ? "No description available" : tmdbMovie.overview,
                        vote_average = tmdbMovie.vote_average,
                        release_date = string.IsNullOrEmpty(tmdbMovie.release_date) ? "Unknown" : tmdbMovie.release_date,
                        poster_path = string.IsNullOrEmpty(tmdbMovie.poster_path) ? "No image available" : $"{_posterBaseUrl}{tmdbMovie.poster_path}"
                    }).ToList();
                }
            }
            // Return an empty list if the response is unsuccessful or empty
            return Enumerable.Empty<Movie>();
        }

        public Task<IEnumerable<Movie>> GetAllMoviesratelimitAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Movie> GetMovieByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Movie>> GetMoviesAsync(int page, int pageSize)
        {
            throw new NotImplementedException();
        }
    }
}
