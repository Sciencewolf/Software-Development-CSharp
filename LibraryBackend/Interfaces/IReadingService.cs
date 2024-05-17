using LibraryBackend.Shared;

namespace LibraryBackend.Interfaces;

public interface IReadingService
{
    Task Add(Reading reading);

    Task Delete(Guid Id);

    Task<Reading> Get(Guid Id);

    Task<IEnumerable<Reading>> GetAll();

    Task Update(Reading reading);
}