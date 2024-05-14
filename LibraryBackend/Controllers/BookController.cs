using LibraryBackend.Interfaces;
using LibraryBackend.Shared;
using Microsoft.AspNetCore.Mvc;

namespace LibraryBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Book>> Get(Guid Id)
        {
            var book = await _bookService.Get(Id);

            if (book is null)
            {
                return NotFound(new { Error = "Book not found." });
            }

            return Ok(book);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetAll() // Changed return type
        {
            try
            {
                return Ok(await _bookService.GetAll());
            }
            catch (Exception e)
            {
                return StatusCode(500, new { Error = "An error occurred while fetching books." }); 
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Book book)
        {
            var existingBook = await _bookService.Get(book.Id);

            if (existingBook is not null)
            {
                return Conflict();
            }

            await _bookService.Add(book);

            return Ok("Book Add");
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var book = await _bookService.Get(id);

            if (book is null)
            {
                return NotFound();
            }

            await _bookService.Delete(id);

            return Ok("Book Delete");
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Book NewBook)
        {
            if (id != NewBook.Id)
            {
                return BadRequest();
            }

            var existingBook = await _bookService.Get(id);

            if (existingBook is null)
            {
                return NotFound();
            }

            await _bookService.Update(NewBook);

            return Ok("Book Update");
        }
    }
}
