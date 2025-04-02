using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Borrowers;

namespace Domain.Loans
{
    public interface ILoanRepository
    {
        List<Loan> GetAll();
        Loan GetByIdLoan(int idLoan);
        string Create(Loan loan);
        void Update(Loan loan);
        Task<Borrower?> GetBorrowerBySin(string sin);
       

    }
}
