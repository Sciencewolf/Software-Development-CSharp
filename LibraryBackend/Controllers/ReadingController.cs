using LibraryBackend.Interfaces;
using LibraryBackend.Shared;
using Microsoft.AspNetCore.Mvc;

namespace LibraryBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReadingController : ControllerBase
    {
        private readonly IReadingService _readingService;

        public ReadingController(IReadingService readingService)
        {
            _readingService = readingService;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Reading>> Get(Guid Id)
        {
            var reading = await _readingService.Get(Id);

            if (reading is null)
            {
                return NotFound();
            }

            return Ok(reading);
        }

        [HttpGet]
        public async Task<ActionResult<List<Loan>>> GetAll()
        {
            return Ok(await _readingService.GetAll());
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Reading reading)
        {
            var existingReading = await _readingService.Get(reading.Id);

            if (existingReading is not null)
            {
                return Conflict();
            }

            await _readingService.Add(reading);

            return Ok("Reading Add");
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var reading = await _readingService.Get(id);

            if (reading is null)
            {
                return NotFound();
            }

            await _readingService.Delete(id);

            return Ok("Reading Delete");
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Reading NewReading)
        {
            if (id != NewReading.Id)
            {
                return BadRequest();
            }

            var existingBook = await _readingService.Get(id);

            if (existingBook is null)
            {
                return NotFound();
            }

            await _readingService.Update(NewReading);

            return Ok("Reading Update");
        }

    }
}
