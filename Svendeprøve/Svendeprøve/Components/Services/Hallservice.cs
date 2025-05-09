namespace Svendeprøve.Components.Services
{
    using Svendeprøve.Components.Models;
    using System.Net.Http.Json;
    using System.Text.Json;

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
                Console.WriteLine($"Raw JSON: {json}"); // Log rå JSON for fejlfinding

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true, // Ignorer store/små bogstaver
                    AllowTrailingCommas = true          // Tolerer ekstra kommaer
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
            var response = await _httpClient.GetAsync($"api/Hall/GetHallsWithSeats/{id}");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Raw JSON for ID {id}: {json}");
            return JsonSerializer.Deserialize<Hall>(json);
        }
    }
}