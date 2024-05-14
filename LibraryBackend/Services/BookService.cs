using LibraryBackend.Interfaces;
using LibraryBackend.Shared;
using Microsoft.EntityFrameworkCore;

namespace LibraryBackend.Services
{
    public class BookService : IBookService
    {
        private readonly ILogger _logger;
        private readonly LibraryBackendContext _context;

        public BookService(ILogger logger, LibraryBackendContext context)
        { 
            _logger = logger;
            _context = context;
        }

        public async Task Add(Book book)
        {
            _logger.LogInformation("Add book");
            await _context.Book.AddAsync(book);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Save Changes Add book");

        }

        public async Task Delete(Guid Id)
        {
            _logger.LogInformation("Before Delete book");

            var book = await Get(Id);

            _context.Book.Remove(book);

            await _context.SaveChangesAsync();
            _logger.LogInformation("After Delete book");

        }

        public async Task<Book> Get(Guid Id)
        {
            var book = await _context.Book.FindAsync(Id);
            _logger.LogInformation("Get book");
            return book;
        }

        public async Task<IEnumerable<Book>> GetAll()
        {
            _logger.LogInformation("Get all book");
            return await _context.Book.ToListAsync();
        }

        public async Task Update(Book NewBook)
        {
            _logger.LogInformation("Update book");
            var book = await Get(NewBook.Id);
            book.Id = NewBook.Id;
            book.Author = NewBook.Author;
            book.Publisher = NewBook.Publisher;
            book.YearOfPublication = NewBook.YearOfPublication;

            await _context.SaveChangesAsync();
        }
    }
}
