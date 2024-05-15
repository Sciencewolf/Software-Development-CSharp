using LibraryBackend.Shared;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace LibraryFrontend.Services;

public class BookService : IBookService
{
    private readonly HttpClient _httpClient;

    public BookService(HttpClient httpClient) => _httpClient = httpClient;

    public async Task AddAsync(Book book) => await _httpClient.PostAsJsonAsync("/Book", book);

    public async Task DeleteAsync(Guid Id) => await _httpClient.DeleteAsync($"/Book/{Id}");

    public async Task<Book> GetAsync(Guid Id) => await _httpClient.GetFromJsonAsync<Book>($"/Book/{Id}");

    public async Task<IEnumerable<Book>> GetAllAsync()
    {
        //return await _httpClient.GetFromJsonAsync<IEnumerable<Book>>("/Book");
        var response = await _httpClient.GetAsync("/Book");
        var responseContent = await response.Content.ReadAsStringAsync();

        // Log the response content for debugging
        Console.WriteLine(responseContent);

        if (response.IsSuccessStatusCode)
        {
            return JsonSerializer.Deserialize<IEnumerable<Book>>(responseContent);
        }
        else
        {
            // Handle error response appropriately
            throw new Exception($"Error fetching books: {responseContent}");
        }
    }

    public async Task UpdateAsync(Guid Id, Book NewBook) => await _httpClient.PutAsJsonAsync($"/Book/{Id}", NewBook);
}   