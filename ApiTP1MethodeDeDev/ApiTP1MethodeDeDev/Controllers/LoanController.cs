using Domain.Loans;
using Microsoft.AspNetCore.Mvc;

namespace ApiTP1MethodeDeDev.Controllers
{
    [ApiController]
    [Route("api/loans")]
    public class LoanController : ControllerBase

    { private readonly ILoanService _loanService;

        public LoanController(ILoanService loanService)
        {
            _loanService = loanService;
        }

        

    }
}
