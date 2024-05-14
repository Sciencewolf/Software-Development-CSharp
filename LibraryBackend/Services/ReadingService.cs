using LibraryBackend.Interfaces;
using LibraryBackend.Shared;
using Microsoft.EntityFrameworkCore;

namespace LibraryBackend.Services
{
    public class ReadingService : IReadingService
    {
        private readonly ILogger _logger;
        private readonly LibraryBackendContext _context;

        public ReadingService(ILogger logger, LibraryBackendContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task Add(Reading reading)
        {
            _logger.LogInformation("Add reading");
            await _context.Reading.AddAsync(reading);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid Id)
        {
            _logger.LogInformation("Delete reading");
            var reading = await Get(Id);

            _context.Reading.Remove(reading);

            await _context.SaveChangesAsync();
        }

        public async Task<Reading> Get(Guid Id)
        {
            var reading = await _context.Reading.FindAsync(Id);
            _logger.LogInformation("Get reading");
            return reading;
        }

        public async Task<List<Reading>> GetAll()
        {
            _logger.LogInformation("Get all reading");
            return await _context.Reading.ToListAsync();
        }

        public async Task Update(Reading NewReading)
        {
            _logger.LogInformation("Update reading");
            var reading = await Get(NewReading.Id);
            reading.Id = NewReading.Id;
            reading.BirthDate = NewReading.BirthDate;
            reading.Address = NewReading.Address;
            reading.Name = NewReading.Name;

            await _context.SaveChangesAsync();
        }
    }
}
