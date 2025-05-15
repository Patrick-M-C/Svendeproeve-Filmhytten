namespace Svendeprøve.Components.Services
{
    using Svendeprøve.Components.Models;
    using System.Net.Http.Json;
    using System.Text.Json;

    // Serviceklasse til håndtering af API-kald relateret til biografsalen.
    // Udfører HTTP-anmodninger for at hente sale og deres sædeoplysninger fra en backend.

    // Funktioner: (i rækkefølge)
    // HTTPClient -> Initialiserer HttpClient via dependency injection.
    // JsonSerializer -> Ignorerer store/små bogstaver i JSON og Tillader ekstra kommaer i JSON
    // GetHallByIdAsync -> Henter en specifik sal inklusive dens sæder baseret på salens ID
    public class HallService
    {
        private readonly HttpClient _httpClient;

        public HallService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Hall>> GetHallsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Hall/withSeat");
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Raw JSON: {json}");

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    AllowTrailingCommas = true 
                };
                return JsonSerializer.Deserialize<List<Hall>>(json, options);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fejl i GetHallsAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<Hall> GetHallByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/Hall/withseat/{id}");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<Hall>();
        }
    }
}