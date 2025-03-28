using ApiTP1MethodeDeDev.Dtos;
using ApiTP1MethodeDeDev.Dtos.Loan;
using Domain.Borrowers;
using Domain.Loans;
using Microsoft.AspNetCore.Mvc;

namespace ApiTP1MethodeDeDev.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class LoansController : ControllerBase

    { 
        private readonly ILoanService _loanService;

        public LoansController(ILoanService loanService)
        {
            _loanService = loanService;
        }

        [HttpGet("{idLoan}")]
        public ActionResult<LoanResponse> Get(int idLoan)
        {
            try
            {
                Loan loan = _loanService.GetByIdLoan(idLoan);

                return Ok(
                    new LoanResponse
                    {
                        Amount = loan.Amount,
                        InterestRate = loan.InterestRate,
                        DurationInMonths = loan.DurationInMonths,
                        Status = loan.Status,
                        StartDate = loan.StartDate,
                        EndDate = loan.StartDate.AddMonths(loan.DurationInMonths),
                        RemainingAmount = loan.RemainingAmount,
                        TheBorrower = loan.TheBorrower
                    }
                );
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpGet]
        public IEnumerable<LoanResponse> Get()
        {
            return _loanService.GetAll().Select(l =>
                new LoanResponse
                {
                    IdLoan = l.IdLoan,
                    Amount = l.Amount,
                    InterestRate = l.InterestRate,
                    DurationInMonths = l.DurationInMonths,
                    Status = l.Status,
                    StartDate = l.StartDate,
                    EndDate = l.StartDate.AddMonths(l.DurationInMonths),
                    RemainingAmount = l.RemainingAmount,
                    TheBorrower = l.TheBorrower
                });
        }

        [HttpPost]
        public ActionResult<string> Post([FromBody] LoanRequest loanRequest)
        {
            if (loanRequest == null)
                return BadRequest("You have entered bad laon information");

            if (loanRequest.StartDate != DateTime.Now)
                return BadRequest("Start date must be the actual date.");

            var loan = new Loan
            {
                IdLoan = loanRequest.IdLoan,
                Amount = loanRequest.Amount,
                InterestRate = loanRequest.InterestRate,
                DurationInMonths = loanRequest.DurationInMonths,
                Status = loanRequest.Status,
                StartDate = loanRequest.StartDate,
                EndDate = loanRequest.StartDate.AddMonths(loanRequest.DurationInMonths),
                RemainingAmount = loanRequest.RemainingAmount,
                TheBorrower = loanRequest.TheBorrower
            };

            string idLoan = _loanService.Create(loan);
            return CreatedAtAction(nameof(Get), new { idLoan = idLoan }, idLoan);
        }


    }
}
