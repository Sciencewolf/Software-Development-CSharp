using LibraryBackend.Shared;

namespace LibraryBackend.Interfaces;

public interface ILoanService
{
    Task Add(Loan loan);

    Task Delete(Guid Id);

    Task<Loan> Get(Guid Id);

    Task<List<Loan>> GetAll();

    Task Update(Loan loan);
}
