using LibraryBackend.Classes;
using LibraryBackend.Contexts;
using LibraryBackend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryBackend.Services
{
    public class ReadingService : IReadingService
    {
        private readonly LibraryBackendContext _context;
        private readonly ILogger _logger;

        public ReadingService(ILogger<ReadingService> logger, LibraryBackendContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task Add(Reading reading)
        {
            await _context.Reading.AddAsync(reading);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid Id)
        {
            var reading = await Get(Id);

            _context.Reading.Remove(reading);

            await _context.SaveChangesAsync();
        }

        public async Task<Reading> Get(Guid Id)
        {
            var reading = await _context.Reading.FindAsync(Id);
            return reading;
        }

        public async Task<List<Reading>> GetAll()
        {
            return await _context.Reading.ToListAsync();
        }

        public async Task Update(Reading NewReading)
        {
            var reading = await Get(NewReading.Id);
            reading.Id = NewReading.Id;
            reading.BirthDate = NewReading.BirthDate;
            reading.Address = NewReading.Address;
            reading.Name = NewReading.Name;

            await _context.SaveChangesAsync();
        }
    }
}
