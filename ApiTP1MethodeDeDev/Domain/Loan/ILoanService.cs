using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Loan
{
    public interface ILoanService
    {
        public IList<Loan> GetAll();
        Loan GetById(int idLoan);
        string Create(Loan loan);
        void Update(Loan loan);
    }
}
