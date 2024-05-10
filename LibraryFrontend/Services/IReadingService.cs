using LibraryBackend.Shared;

namespace LibraryFrontend.Services;

public interface IReadingService
{
    Task AddAsync(Reading reading);

    Task DeleteAsync(Guid Id);

    Task<Reading> GetAsync(Guid Id);

    Task<IEnumerable<Reading>> GetAllAsync();

    Task UpdateAsync(Guid Id, Reading reading);
}