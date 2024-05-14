using LibraryBackend.Interfaces;
using LibraryBackend.Shared;
using Microsoft.EntityFrameworkCore;

namespace LibraryBackend.Services
{
    public class LoanService : ILoanService
    {
        private readonly ILogger _logger;
        private readonly LibraryBackendContext _context;

        public LoanService(ILogger logger, LibraryBackendContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task Add(Loan loan)
        {
            _logger.LogInformation("Add loan");
            await _context.Loan.AddAsync(loan);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid Id)
        {
            _logger.LogInformation("Delete loan");
            var loan = await Get(Id);

            _context.Loan.Remove(loan);

            await _context.SaveChangesAsync();
        }

        public async Task<Loan> Get(Guid Id)
        {
            var loan = await _context.Loan.FindAsync(Id);
            _logger.LogInformation("Get loan");
            return loan;
        }

        public async Task<List<Loan>> GetAll()
        {
            _logger.LogInformation("Get all loan");
            return await _context.Loan.ToListAsync();
        }

        public async Task Update(Loan NewLoan)
        {
            _logger.LogInformation("Update loan");
            var loan = await Get(NewLoan.Id);
            loan.BookId = NewLoan.BookId;
            loan.BorrowingDate = NewLoan.BorrowingDate;
            loan.ReturnDeadLine = NewLoan.ReturnDeadLine;

            await _context.SaveChangesAsync();
        }
    }
}
