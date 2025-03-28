using ApiTP1MethodeDeDev.Dtos;
using Domain.Loans;
using Microsoft.AspNetCore.Mvc;

namespace ApiTP1MethodeDeDev.Controllers
{
    [ApiController]
    [Route("api/loans")]
    public class LoanController : ControllerBase

    { 
        private readonly ILoanService _loanService;

        public LoanController(ILoanService loanService)
        {
            _loanService = loanService;
        }

        public IEnumerable<LoanReponse> Get()
        {
            return _borrowerService.GetAll().Select(b =>
                new BorrowerReponse
                {
                    FirstName = b.FirstName,
                    LastName = b.LastName,
                    Phone = b.Phone,
                    Email = b.Email,
                    Address = b.Address
                });
        }

    }
}
