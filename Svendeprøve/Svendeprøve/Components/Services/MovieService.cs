namespace Svendeprøve.Components.Services
{
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;
    using Svendeprøve.Components.Models;

    public class MovieService
    {
        private readonly HttpClient _httpClient;

        public MovieService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7009/");
        }

        public async Task<List<Movie>> GetMoviesAsync()
        {
            try
            {
                // Sender en GET-anmodning til API'et for at hente film
                var response = await _httpClient.GetFromJsonAsync<List<Movie>>("api/Movie");

                // Hvis der ikke er nogen data, returneres en tom liste
                if (response == null)
                {
                    return new List<Movie>();
                }

                return response;
            }
            catch (Exception ex)
            {
                // Håndter fejl (kan f.eks. logge fejlen her)
                Console.WriteLine($"Fejl ved hentning af film: {ex.Message}");
                return new List<Movie>(); // Returner en tom liste ved fejl
            }
        }
        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Movie>($"api/Movie/{id}");
        }
    }
}