using LibraryBackend.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using LibraryBackend.Shared;

namespace LibraryBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoanController : ControllerBase
    {
        private readonly ILoanService _loanService;

        public LoanController(ILoanService loanService)
        {
            _loanService = loanService;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Loan>> Get(Guid Id)
        {
            var loan = await _loanService.Get(Id);

            if(loan is null)
            {
                return NotFound();
            }

            return Ok(loan);
        }

        [HttpGet]
        public async Task<ActionResult<List<Loan>>> GetAll()
        {
            return Ok(await _loanService.GetAll());
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Loan loan)
        {
            var existingLoan = await _loanService.Get(loan.Id);

            if (existingLoan is not null)
            {
                return Conflict();
            }

            await _loanService.Add(loan);

            return Ok();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var loan = await _loanService.Get(id);

            if (loan is null)
            {
                return NotFound();
            }

            await _loanService.Delete(id);

            return Ok();
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Loan NewLoan)
        {
            if (id != NewLoan.Id)
            {
                return BadRequest();
            }

            var existingLoan = await _loanService.Get(id);

            if (existingLoan is null)
            {
                return NotFound();
            }

            await _loanService.Update(NewLoan);

            return Ok();
        }
    }
}
