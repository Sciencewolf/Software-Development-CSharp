using LibraryBackend.Shared;
using LibraryBackend;
using Microsoft.EntityFrameworkCore;
using LibraryBackend.Services;
using Moq;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
namespace LibraryTest;

public class ReadingServiceUnitTests : IDisposable
{
    private readonly Mock<ILogger<ReadingService>> _loggerMock;
    private readonly LibraryBackendContext _context;
    private readonly ReadingService _readingService;

    public ReadingServiceUnitTests()
    {
        _loggerMock = new Mock<ILogger<ReadingService>>();

        var options = new DbContextOptionsBuilder<LibraryBackendContext>()
            .UseInMemoryDatabase(databaseName: "LibraryTestDatabase")
            .Options;
        _context = new LibraryBackendContext(options);

        _readingService = new ReadingService(_loggerMock.Object, _context);
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    [Fact]
    public async Task Add_ShouldAddReading()
    {
        var reading = new Reading
        {
            Id = Guid.NewGuid(),
            BirthDate = new DateTime(2000, 1, 1),
            Address = "123 Main St",
            Name = "John Doe"
        };

        await _readingService.Add(reading);

        var addedReading = await _context.Reading.FindAsync(reading.Id);
        Assert.NotNull(addedReading);
        Assert.Equal(reading.Name, addedReading.Name);
    }

    [Fact]
    public async Task Delete_ShouldRemoveReading()
    {
        var reading = new Reading
        {
            Id = Guid.NewGuid(),
            BirthDate = new DateTime(2000, 1, 1),
            Address = "123 Main St",
            Name = "John Doe"
        };

        await _context.Reading.AddAsync(reading);
        await _context.SaveChangesAsync();

        await _readingService.Delete(reading.Id);

        var deletedReading = await _context.Reading.FindAsync(reading.Id);
        Assert.Null(deletedReading);
    }

    [Fact]
    public async Task Get_ShouldReturnReading()
    {
        var reading = new Reading
        {
            Id = Guid.NewGuid(),
            BirthDate = new DateTime(2000, 1, 1),
            Address = "123 Main St",
            Name = "John Doe"
        };

        await _context.Reading.AddAsync(reading);
        await _context.SaveChangesAsync();

        var retrievedReading = await _readingService.Get(reading.Id);
        Assert.NotNull(retrievedReading);
        Assert.Equal(reading.Name, retrievedReading.Name);
    }

    [Fact]
    public async Task GetAll_ShouldReturnAllReadings()
    {
        var reading1 = new Reading
        {
            Id = Guid.NewGuid(),
            BirthDate = new DateTime(2000, 1, 1),
            Address = "123 Main St",
            Name = "John Doe"
        };

        var reading2 = new Reading
        {
            Id = Guid.NewGuid(),
            BirthDate = new DateTime(1990, 5, 15),
            Address = "456 Elm St",
            Name = "Jane Doe"
        };

        await _context.Reading.AddRangeAsync(new List<Reading> { reading1, reading2 });
        await _context.SaveChangesAsync();

        var readings = await _readingService.GetAll();
        Assert.Equal(2, readings.Count);
    }

    [Fact]
    public async Task Update_ShouldModifyReading()
    {
        var reading = new Reading
        {
            Id = Guid.NewGuid(),
            BirthDate = new DateTime(2000, 1, 1),
            Address = "123 Main St",
            Name = "John Doe"
        };

        await _context.Reading.AddAsync(reading);
        await _context.SaveChangesAsync();

        var updatedReading = new Reading
        {
            Id = reading.Id,
            BirthDate = new DateTime(1995, 8, 20),
            Address = "789 Oak St",
            Name = "John Smith"
        };

        await _readingService.Update(updatedReading);

        var retrievedReading = await _context.Reading.FindAsync(reading.Id);
        Assert.NotNull(retrievedReading);
        Assert.Equal(updatedReading.Name, retrievedReading.Name);
        Assert.Equal(updatedReading.BirthDate, retrievedReading.BirthDate);
        Assert.Equal(updatedReading.Address, retrievedReading.Address);
    }
}