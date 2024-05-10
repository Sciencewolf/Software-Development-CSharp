using LibraryBackend.Shared;
using System.Net.Http.Json;

namespace LibraryFrontend.Services
{
    public class LoanService : ILoanService
    {
        private HttpClient _httpClient;

        public async Task AddAsync(Loan loan)
        {
            await _httpClient.PostAsJsonAsync("/loan/Loan", loan);
        }

        public async Task DeleteAsync(Guid Id)
        {
            await _httpClient.DeleteAsync($"/loan/{Id}");
        }

        public async Task<Loan> GetAsync(Guid Id)
        {
            return await _httpClient.GetFromJsonAsync<Loan>($"/loan/{Id}");
        }

        public async Task<IEnumerable<Loan>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Loan>>("/loan/all");
        }

        public async Task UpdateAsync(Guid Id, Loan NewLoan)
        {
            await _httpClient.PutAsJsonAsync($"/loan/{Id}", NewLoan);
        }
    }
}
