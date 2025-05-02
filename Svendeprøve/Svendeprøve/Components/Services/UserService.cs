namespace FilmHytten.Components.Services
{
    using Svendeprøve.Components.Models;
    using System.Net.Http.Json;

    public class UserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<User>>("api/User");
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<User>($"api/User/{id}");
        }
    }
}