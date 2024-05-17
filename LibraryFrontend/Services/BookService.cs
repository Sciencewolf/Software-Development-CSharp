using LibraryBackend.Shared;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace LibraryFrontend.Services;

/// <summary>
/// <c>BookService</c> impl IBookService
/// </summary>
public class BookService : IBookService
{
    private readonly HttpClient _httpClient;

    private const string Base = "/Book";

    public BookService(HttpClient httpClient) => _httpClient = httpClient;

    public async Task AddAsync(Book book) => await _httpClient.PostAsJsonAsync(Base, book);

    public async Task DeleteAsync(Guid Id) => await _httpClient.DeleteAsync($"{Base}/{Id}");

    public async Task<Book> GetAsync(Guid Id) => await _httpClient.GetFromJsonAsync<Book>($"{Base}/{Id}");

    public async Task<IEnumerable<Book>> GetAllAsync()
    {
        //return await _httpClient.GetFromJsonAsync<IEnumerable<Book>>(Base);
        var response = await _httpClient.GetAsync(Base);
        var responseContent = await response.Content.ReadAsStringAsync();

        Console.WriteLine(responseContent);

        if (response.IsSuccessStatusCode)
        {
            return JsonSerializer.Deserialize<IEnumerable<Book>>(responseContent);
        }
        
        throw new Exception($"Error fetching books: {responseContent}");
    }

    public async Task UpdateAsync(Guid Id, Book NewBook) => await _httpClient.PutAsJsonAsync($"{Base}/{Id}", NewBook);
}   