using LibraryBackend.Shared;
using System.Net.Http.Json;

namespace LibraryFrontend.Services
{
    public class ReadingService : IReadingService
    {
        private readonly HttpClient _httpClient;

        public ReadingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task AddAsync(Reading reading)
        {
            await _httpClient.PostAsJsonAsync("/api/Reading", reading);
        }

        public async Task DeleteAsync(Guid Id)
        {
            await _httpClient.DeleteAsync($"/api/Reading/{Id}");
        }

        public async Task<Reading> GetAsync(Guid Id)
        {
            return await _httpClient.GetFromJsonAsync<Reading>($"/api/Reading/{Id}");
        }

        public async Task<IEnumerable<Reading>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Reading>>("/api/Reading");

        }

        public async Task UpdateAsync(Guid Id, Reading NewReading)
        {
            await _httpClient.PutAsJsonAsync($"/api/Reading/{Id}", NewReading);
        }
    }
}
