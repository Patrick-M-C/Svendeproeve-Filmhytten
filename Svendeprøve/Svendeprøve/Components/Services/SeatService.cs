namespace Svendeprøve.Components.Services
{
    using Svendeprøve.Components.Models;
    using System.Net.Http.Json;

    // Serviceklasse til håndtering af API-kald relateret til vores sæder.
    // Udfører HTTP-anmodninger for at hente sæderne og den tilhørende hall fra vores backend.

    public class SeatService
    {
        private readonly HttpClient _httpClient;

        public SeatService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Seat>> GetSeatsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Seat>>("api/Seat");
        }

        public async Task<Seat> GetSeatByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Seat>($"api/Seat/{id}");
        }
    }
}