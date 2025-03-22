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
            if (loan == null)
            {
                return "Le prêt ne peut pas être nul";
            }

            // Verify if the loan amount is negative or zero
            if (loan.Amount <= 0)
            {
                return "Le montant du prêt doit être positif.";
            }

            //verify if the loan duration is negative or zero
            if (loan.DurationInMonths <= 0)
            {
                return "La durée du prêt doit être positive.";
            }

            // When the loan is created, the remaining amount is said to be the total amount
            loan.RemainingAmount = loan.Amount;

            return _loanRepository.Create(loan);
                        
        }
        public IList<Loan> GetAll()
        {
            return _loanRepository.GetAll();
        }

        public Loan GetByIdLoan(int idLoan)
        {
            var loan = _loanRepository.GetByIdLoan(idLoan); 
            if (loan == null)
            {
                return null!;
            }

            return loan;
        }

        public void Update(Loan loan)
        {
            if (loan == null)
            {
                throw new ArgumentNullException(nameof(loan), "Le prêt ne peut pas être nul.");
            }
            _loanRepository.Update(loan);
        }
    }
}
