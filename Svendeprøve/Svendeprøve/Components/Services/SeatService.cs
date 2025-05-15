namespace Svendeprøve.Components.Services
{
    using Svendeprøve.Components.Models;
    using System.Net.Http.Json;

    // Serviceklasse til håndtering af API-kald relateret til vores sæder.
    // Udfører HTTP-anmodninger for at hente sæderne og den tilhørende hall fra vores backend.

    //Funktioner:
    // SeatService -> Initialiserer HttpClient via dependency injection.
    // Task<List<Seat>> GetSeatsAsync -> Henter en liste af alle sæder fra API'et.
    // Udfører GET-anmodning for at hente alle sæder.

    // Task<Seat> GetSeatByIdAsync(int id) -> Henter et specifikt sæde baseret på dets ID.
    // Udfører GET-anmodning for et enkelt sæde.
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