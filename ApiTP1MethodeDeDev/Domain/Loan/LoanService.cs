using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Loan
{
    public class LoanService : ILoanService
    {
        private readonly ILoanRepository _loanRepository;

        public LoanService(ILoanRepository loanRepository)
        {
            _loanRepository = loanRepository;
        }
       
        public string Create(Loan loan)
        {
            throw new NotImplementedException();
        }
        public IList<Loan> GetAll()
        {
            throw new NotImplementedException();
        }

        public Loan GetByIdLoan(int idLoan)
        {
            throw new NotImplementedException();
        }

        public void Update(Loan loan)
        {
            throw new NotImplementedException();
        }
    }
}
