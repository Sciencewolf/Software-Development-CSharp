using LibraryBackend.Shared;
using System.Net.Http.Json;

namespace LibraryFrontend.Services
{
    public class LoanService : ILoanService
    {
        private readonly HttpClient _httpClient;

        public LoanService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task AddAsync(Loan loan)
        {
            await _httpClient.PostAsJsonAsync("/api/Loan", loan);
        }

        public async Task DeleteAsync(Guid Id)
        {
            await _httpClient.DeleteAsync($"/api/Loan/{Id}");
        }

        public async Task<Loan> GetAsync(Guid Id)
        {
            return await _httpClient.GetFromJsonAsync<Loan>($"/api/Loan/{Id}");
        }

        public async Task<IEnumerable<Loan>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Loan>>("/api/Loan");
        }

        public async Task UpdateAsync(Guid Id, Loan NewLoan)
        {
            await _httpClient.PutAsJsonAsync($"/api/Loan/{Id}", NewLoan);
        }
    }
}
