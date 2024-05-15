using LibraryBackend.Shared;
using LibraryBackend;
using Microsoft.EntityFrameworkCore;
using LibraryBackend.Services;
using Moq;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
namespace LibraryTest;

public class LoanServiceUnitTests : IDisposable
{
    private readonly Mock<ILogger<LoanServiceUnitTests>> _loggerMock;
    private readonly LibraryBackendContext _context;
    private readonly LoanService _loanService;

    public LoanServiceUnitTests()
    {
        _loggerMock = new Mock<ILogger<LoanServiceUnitTests>>();
        var options = new DbContextOptionsBuilder<LibraryBackendContext>()
            .UseInMemoryDatabase(databaseName: "LibraryTestDatabase")
            .Options;
        _context = new LibraryBackendContext(options);

        _loanService = new LoanService(_context);
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    [Fact]
    public async Task Add_ShouldAddLoan()
    {
        var loan = new Loan
        {
            Id = Guid.NewGuid(),
            BookId = Guid.NewGuid(),
            BorrowingDate = DateTime.Now,
            ReturnDeadLine = DateTime.Now.AddDays(14)
        };

        await _loanService.Add(loan);

        var addedLoan = await _context.Loan.FindAsync(loan.Id);
        Assert.NotNull(addedLoan);
        Assert.Equal(loan.BookId, addedLoan.BookId);
    }

    [Fact]
    public async Task Delete_ShouldRemoveLoan()
    {
        var loan = new Loan
        {
            Id = Guid.NewGuid(),
            BookId = Guid.NewGuid(),
            BorrowingDate = DateTime.Now,
            ReturnDeadLine = DateTime.Now.AddDays(14)
        };

        await _context.Loan.AddAsync(loan);
        await _context.SaveChangesAsync();

        await _loanService.Delete(loan.Id);

        var deletedLoan = await _context.Loan.FindAsync(loan.Id);
        Assert.Null(deletedLoan);
    }

    [Fact]
    public async Task Get_ShouldReturnLoan()
    {
        var loan = new Loan
        {
            Id = Guid.NewGuid(),
            BookId = Guid.NewGuid(),
            BorrowingDate = DateTime.Now,
            ReturnDeadLine = DateTime.Now.AddDays(14)
        };

        await _context.Loan.AddAsync(loan);
        await _context.SaveChangesAsync();

        var retrievedLoan = await _loanService.Get(loan.Id);
        Assert.NotNull(retrievedLoan);
        Assert.Equal(loan.BookId, retrievedLoan.BookId);
    }

    [Fact]
    public async Task GetAll_ShouldReturnAllLoans()
    {
        var loan1 = new Loan
        {
            Id = Guid.NewGuid(),
            BookId = Guid.NewGuid(),
            BorrowingDate = DateTime.Now,
            ReturnDeadLine = DateTime.Now.AddDays(14)
        };

        var loan2 = new Loan
        {
            Id = Guid.NewGuid(),
            BookId = Guid.NewGuid(),
            BorrowingDate = DateTime.Now,
            ReturnDeadLine = DateTime.Now.AddDays(21)
        };

        await _context.Loan.AddRangeAsync(new List<Loan> { loan1, loan2 });
        await _context.SaveChangesAsync();

        var loans = await _loanService.GetAll();
        Assert.Equal(2, loans.Count());
    }

    [Fact]
    public async Task Update_ShouldModifyLoan()
    {
        var loan = new Loan
        {
            Id = Guid.NewGuid(),
            BookId = Guid.NewGuid(),
            BorrowingDate = DateTime.Now,
            ReturnDeadLine = DateTime.Now.AddDays(14)
        };

        await _context.Loan.AddAsync(loan);
        await _context.SaveChangesAsync();

        var updatedLoan = new Loan
        {
            Id = loan.Id,
            BookId = Guid.NewGuid(), // New Book ID
            BorrowingDate = DateTime.Now.AddDays(-1),
            ReturnDeadLine = DateTime.Now.AddDays(10)
        };

        await _loanService.Update(updatedLoan);

        var retrievedLoan = await _context.Loan.FindAsync(loan.Id);
        Assert.NotNull(retrievedLoan);
        Assert.Equal(updatedLoan.BookId, retrievedLoan.BookId);
        Assert.Equal(updatedLoan.BorrowingDate, retrievedLoan.BorrowingDate);
        Assert.Equal(updatedLoan.ReturnDeadLine, retrievedLoan.ReturnDeadLine);
    }
}