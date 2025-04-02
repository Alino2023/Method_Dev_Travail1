using System.Net;
using ApiTP1MethodeDeDev.Dtos;
using ApiTP1MethodeDeDev.Dtos.Loan;
using Domain.Bank;
using Domain.Borrowers;
using Domain.Loans;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

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
                        TheBorrower = new BorrowerReponse()
                        {
                            Sin = loan.TheBorrower.Sin,
                            FirstName = loan.TheBorrower.FirstName,
                            LastName = loan.TheBorrower.LastName,
                            Phone = loan.TheBorrower.Phone,
                            Email = loan.TheBorrower.Email,
                            Address = loan.TheBorrower.Address
                        }
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
                    TheBorrower = new BorrowerReponse()
                    {
                        Sin = l.TheBorrower.Sin,
                        FirstName = l.TheBorrower.FirstName,
                        LastName = l.TheBorrower.LastName,
                        Phone = l.TheBorrower.Phone,
                        Email = l.TheBorrower.Email,
                        Address = l.TheBorrower.Address
                    }
                });
        }

        [HttpPost]
        public ActionResult<string> Post([FromBody] LoanRequest loanRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (loanRequest.StartDate.Date != DateTime.Now.Date)
                return BadRequest("Start date must be today's date.");

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
                TheBorrower = new Borrower()//on doit remplacer ca par le borrower avec ses parametres 
            };

            string idLoan = _loanService.Create(loan);
            return CreatedAtAction(nameof(Get), new { idLoan = idLoan }, idLoan);
        }
    }
}
