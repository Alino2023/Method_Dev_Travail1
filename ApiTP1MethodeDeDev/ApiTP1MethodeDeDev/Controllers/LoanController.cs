using Microsoft.AspNetCore.Mvc;

namespace ApiTP1MethodeDeDev.Controllers
{
    [ApiController]
    [Route("api/loans")]
    public class LoanController : ControllerBase

    { private readonly LoanRepository _loanRepository;

        public LoanController(LoanRepository loanRepository)
        {
            _loanRepository = loanRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateLoan([FromBody] Loan loan)
        {
            if (loan == null)
                return BadRequest("Les données du prêt sont invalides.");

            await _loanRepository.AddLoanAsync(loan);
            return Ok(loan);
        }

        [HttpGet]
        public async Task<IActionResult> GetLoans()
        {
            var loans = await _loanRepository.GetLoansAsync();
            return Ok(loans);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLoanById(int id)
        {
            var loan = await _loanRepository.GetLoanByIdAsync(id);
            if (loan == null) return NotFound();
            return Ok(loan);
        }

    }
}
