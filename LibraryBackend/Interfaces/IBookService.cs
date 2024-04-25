using LibraryBackend.Classes;

namespace LibraryBackend.Interfaces
{
    public interface IBookService
    {
        Task Add(Book book);

        Task Delete(Guid Id);

        Task<Book> Get(Guid Id);

        Task<List<Book>> GetAll();

        Task Update(Book book);
    }
}
