using LibraryBackend.Shared;
using System.Net.Http.Json;

namespace LibraryFrontend.Services;

public class BookService : IBookService
{
    private readonly HttpClient _httpClient;

    public BookService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task AddAsync(Book book)
    {
        await _httpClient.PostAsJsonAsync("/Book/add", book);
    }

    public async Task DeleteAsync(Guid Id)
    {
        await _httpClient.DeleteAsync($"/Book/{Id}");
    }

    public async Task<Book> GetAsync(Guid Id)
    {
        return await _httpClient.GetFromJsonAsync<Book>($"/Book/{Id}");
    }

    public async Task<IEnumerable<Book>> GetAllAsync()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<Book>>("/Book");
    }

    public async Task UpdateAsync(Guid Id, Book NewBook)
    {
        await _httpClient.PutAsJsonAsync($"/Book/{Id}", NewBook);
    }
}   