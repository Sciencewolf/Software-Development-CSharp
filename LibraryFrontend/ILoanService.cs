using LibraryBackend.Shared;

namespace LibraryFrontend;

public interface ILoanService
{
    Task AddAsync(Loan loan);

    Task DeleteAsync(Guid Id);

    Task<Loan> GetAsync(Guid Id);

    Task<IEnumerable<Loan>> GetAllAsync();

    Task UpdateAsync(Guid Id, Loan loan);
}
