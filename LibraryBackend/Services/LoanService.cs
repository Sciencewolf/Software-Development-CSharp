using LibraryBackend.Interfaces;
using LibraryBackend.Shared;
using Microsoft.EntityFrameworkCore;

namespace LibraryBackend.Services
{
    public class LoanService(LibraryBackendContext context) : ILoanService
    {
        private readonly LibraryBackendContext _context = context;

        public async Task Add(Loan loan)
        {
            await _context.Loan.AddAsync(loan);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid Id)
        {
            var loan = await Get(Id);
            _context.Loan.Remove(loan);
            await _context.SaveChangesAsync();
        }

        public async Task<Loan> Get(Guid Id)
        {
            var loan = await _context.Loan.FindAsync(Id);
            return loan;
        }

        public async Task<IEnumerable<Loan>> GetAll() => await _context.Loan.ToListAsync();

        public async Task Update(Loan NewLoan)
        {
            var loan = await Get(NewLoan.Id);
            loan.BookId = NewLoan.BookId;
            loan.BorrowingDate = NewLoan.BorrowingDate;
            loan.ReturnDeadLine = NewLoan.ReturnDeadLine;

            await _context.SaveChangesAsync();
        }
    }
}
