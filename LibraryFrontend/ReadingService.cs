﻿using LibraryBackend.Shared;
using System.Net.Http.Json;

namespace LibraryFrontend
{
    public class ReadingService : IReadingService
    {
        private HttpClient _httpClient;
        

        public async Task AddAsync(Reading reading)
        {
            await _httpClient.PostAsJsonAsync("/reading/addreading", reading);
        }

        public async Task DeleteAsync(Guid Id)
        {
            await _httpClient.DeleteAsync($"/reading/{Id}");
        }

        public async Task<Reading> GetAsync(Guid Id)
        {
            return await _httpClient.GetFromJsonAsync<Reading>($"/reading/{Id}");
        }

        public async Task<IEnumerable<Reading>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Reading>>("/reading/all");
        }

        public async Task UpdateAsync(Guid Id, Reading NewReading)
        {
            await _httpClient.PutAsJsonAsync($"/reading/{Id}", NewReading);
        }
    }
}