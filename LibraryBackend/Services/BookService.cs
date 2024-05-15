using LibraryBackend.Interfaces;
using LibraryBackend.Shared;
using Microsoft.EntityFrameworkCore;

namespace LibraryBackend.Services
{
    public class BookService(LibraryBackendContext context) : IBookService
    {
        private readonly LibraryBackendContext _context = context;

        public async Task Add(Book book)
        {
            await _context.Book.AddAsync(book);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid Id)
        {
            var book = await Get(Id);
            _context.Book.Remove(book);
            await _context.SaveChangesAsync();
        }

        public async Task<Book> Get(Guid Id)
        {
            var book = await _context.Book.FindAsync(Id);
            return book;
        }

        public async Task<IEnumerable<Book>> GetAll() => await _context.Book.ToListAsync();

        public async Task Update(Book NewBook)
        {
            var book = await Get(NewBook.Id);
            book.Id = NewBook.Id;
            book.Author = NewBook.Author;
            book.Publisher = NewBook.Publisher;
            book.YearOfPublication = NewBook.YearOfPublication;

            await _context.SaveChangesAsync();
        }
    }
}
