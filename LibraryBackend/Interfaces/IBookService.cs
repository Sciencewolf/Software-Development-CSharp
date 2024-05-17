using LibraryBackend.Shared;

namespace LibraryBackend.Interfaces;

public interface IBookService
{
    Task Add(Book book);

    Task Delete(Guid Id);

    Task<Book> Get(Guid Id);

    Task<IEnumerable<Book>> GetAll();

    Task Update(Book book);
}