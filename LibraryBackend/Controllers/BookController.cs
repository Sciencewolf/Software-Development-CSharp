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
                return NotFound();
            }

            return Ok(book);
        }

        [HttpGet]
        public async Task<ActionResult<List<Loan>>> GetAll()
        {
            return Ok(await _bookService.GetAll());
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

            return Ok("Book Added");
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

            return Ok("Book Deleted");
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

            return Ok("Book Updated");
        }
    }
}
