using LibraryBackend.Shared;
using System.Net.Http.Json;

namespace LibraryFrontend.Services
{
    public class ReadingService : IReadingService
    {
        private readonly HttpClient _httpClient;
        private const string Base = "/Reading";

        public ReadingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task AddAsync(Reading reading)
        {
            await _httpClient.PostAsJsonAsync(Base, reading);
        }

        public async Task DeleteAsync(Guid Id)
        {
            await _httpClient.DeleteAsync($"{Base}/{Id}");
        }

        public async Task<Reading> GetAsync(Guid Id)
        {
            return await _httpClient.GetFromJsonAsync<Reading>($"{Base}/{Id}");
        }

        public async Task<IEnumerable<Reading>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Reading>>(Base);

        }

        public async Task UpdateAsync(Guid Id, Reading NewReading)
        {
            await _httpClient.PutAsJsonAsync($"{Base}/{Id}", NewReading);
        }
    }
}
