using LibraryBackend.Shared;

namespace LibraryFrontend;

public interface IBookService
{
    Task AddAsync(Book book);

    Task DeleteAsync(Guid Id);

    Task<Book> GetAsync(Guid Id);

    Task<IEnumerable<Book>> GetAllAsync();

    Task UpdateAsync(Guid Id, Book book);
}