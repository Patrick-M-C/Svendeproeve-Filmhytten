namespace Svendeprøve.Components.Services
{
    using Svendeprøve.Components.Models;
    using System.Net.Http.Json;

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