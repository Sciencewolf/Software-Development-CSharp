using LibraryBackend.Shared;
using System.Net.Http.Json;

namespace LibraryFrontend;

public class BookService(HttpClient httpClient) : IBookService
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task AddAsync(Book book)
    {
        await _httpClient.PostAsJsonAsync("/book/addbook", book);
    }

    public async Task DeleteAsync(Guid Id)
    {
        await _httpClient.DeleteAsync($"/book/{Id}");
    }

    public async Task<Book> GetAsync(Guid Id)
    {
        return await _httpClient.GetFromJsonAsync<Book>($"/book/{Id}");
    }

    public async Task<IEnumerable<Book>> GetAllAsync()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<Book>>("/book/all");
    }

    public async Task UpdateAsync(Guid Id, Book NewBook)
    {
        await _httpClient.PutAsJsonAsync($"/book/{Id}", NewBook);
    }
}