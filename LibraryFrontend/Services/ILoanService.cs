using LibraryBackend.Shared;

namespace LibraryFrontend.Services;

public interface ILoanService
{
    Task Add(Loan loan);

    Task Delete(Guid Id);

    Task<Loan> Get(Guid Id);

    Task<I> GetAll();

    Task Update(Loan loan);
}
