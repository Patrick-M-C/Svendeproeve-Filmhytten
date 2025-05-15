namespace Svendeprøve.Components.Services
{
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;
    using Svendeprøve.Components.Models;

    // Serviceklasse til håndtering af API-kald relateret til vores film.
    // Udfører HTTP-anmodninger for at hente film fra vores API via det HTTP Call.

    //Funktioner: 
    // MovieService -> Initialiserer HttpClient med en baseadresse via dependency injection.
    // GetMoviesAsync -> Henter en liste af alle film fra API'et.3
    // Returnerer tom liste, hvis ingen data modtages.
    // Returnerer listen af film.
    // Logger fejl ved API-kald.
    // og Returnerer tom liste ved fejl.

    // GetMovieByIdAsync -> Henter en specifik film baseret på dens ID.
    // Udfører GET-anmodning for en enkelt film.
    public class MovieService
    {
        private readonly HttpClient _httpClient;

        public MovieService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7149/");
        }

        public async Task<List<Movie>> GetMoviesAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<Movie>>("api/Movie");

                if (response == null)
                {
                    return new List<Movie>();
                }

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fejl ved hentning af film: {ex.Message}");
                return new List<Movie>();
            }
        }
        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Movie>($"api/Movie/{id}");
        }
    }
}