using LibraryBackend;
using LibraryBackend.Services;
using LibraryBackend.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace LibraryTest;

public class BookServiceUnitTests : IDisposable
{
    private readonly Mock<ILogger<BookServiceUnitTests>> _loggerMock;
    private readonly LibraryBackendContext _context;
    private readonly BookService _bookService;

    public BookServiceUnitTests()
    {
        _loggerMock = new Mock<ILogger<BookServiceUnitTests>>();
        var options = new DbContextOptionsBuilder<LibraryBackendContext>()
            .UseInMemoryDatabase(databaseName: "LibraryTestDatabase")
            .Options;
        _context = new LibraryBackendContext(options);

        _bookService = new BookService(_context);
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    [Fact]
    public async Task Add_ShouldAddBook()
    {
        var book = new Book
        {
            Id = Guid.NewGuid(),
            Title = "Title",
            Author = "Author",
            Publisher = "Publisher",
            YearOfPublication = DateTime.Parse("2021/01/01")
        };

        await _bookService.Add(book);

        var addedBook = await _context.Book.FindAsync(book.Id);
        Assert.NotNull(addedBook);
        Assert.Equal(book.Author, addedBook.Author);
    }

    [Fact]
    public async Task Delete_ShouldRemoveBook()
    {
        var book = new Book
        {
            Id = Guid.NewGuid(),
            Title = "Title",
            Author = "Author",
            Publisher = "Publisher",
            YearOfPublication = DateTime.Parse("2021/01/01")
        };

        await _context.Book.AddAsync(book);
        await _context.SaveChangesAsync();

        await _bookService.Delete(book.Id);

        var deletedBook = await _context.Book.FindAsync(book.Id);
        Assert.Null(deletedBook);
    }

    [Fact]
    public async Task Get_ShouldReturnBook()
    {
        var book = new Book
        {
            Id = Guid.NewGuid(),
            Title = "Title",
            Author = "Author",
            Publisher = "Publisher",
            YearOfPublication = DateTime.Parse("2021/01/01")
        };

        await _context.Book.AddAsync(book);
        await _context.SaveChangesAsync();

        var retrievedBook = await _bookService.Get(book.Id);
        Assert.NotNull(retrievedBook);
        Assert.Equal(book.Author, retrievedBook.Author);
    }

    [Fact]
    public async Task GetAll_ShouldReturnAllBooks()
    {
        var book1 = new Book
        {
            Id = Guid.NewGuid(),
            Title = "Title1",
            Author = "Author1",
            Publisher = "Publisher1",
            YearOfPublication = DateTime.Parse("2021/01/01")
        };

        var book2 = new Book
        {
            Id = Guid.NewGuid(),
            Title = "Title2",
            Author = "Author2",
            Publisher = "Publisher2",
            YearOfPublication = DateTime.Parse("2022/04/11")
        };

        await _context.Book.AddRangeAsync(new List<Book> { book1, book2 });
        await _context.SaveChangesAsync();

        var books = await _bookService.GetAll();
        Assert.Equal(2, books.Count());
    }

    [Fact]
    public async Task Update_ShouldModifyBook()
    {
        var book = new Book
        {
            Id = Guid.NewGuid(),
            Title = "Title",
            Author = "Author",
            Publisher = "Publisher",
            YearOfPublication = DateTime.Parse("2021/01/01")
        };

        await _context.Book.AddAsync(book);
        await _context.SaveChangesAsync();

        var updatedBook = new Book
        {
            Id = book.Id,
            Title = "Title",
            Author = "New Author",
            Publisher = "New Publisher",
            YearOfPublication = DateTime.Parse("2022/03/01")
        };

        await _bookService.Update(updatedBook);

        var retrievedBook = await _context.Book.FindAsync(book.Id);
        Assert.NotNull(retrievedBook);
        Assert.Equal(updatedBook.Author, retrievedBook.Author);
        Assert.Equal(updatedBook.Publisher, retrievedBook.Publisher);
        Assert.Equal(updatedBook.YearOfPublication, retrievedBook.YearOfPublication);
    }

}