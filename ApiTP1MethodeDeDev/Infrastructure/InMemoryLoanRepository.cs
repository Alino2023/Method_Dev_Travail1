using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Borrowers;
using Domain.Loans;

namespace Infrastructure
{
    public class InMemoryLoanRepository : ILoanRepository
    {
        private readonly AppDbContext _context;

        public InMemoryLoanRepository(AppDbContext context)
        {
            _context = context;
        }

        public object AddLoanAsync(Loan loan)
        {
            throw new NotImplementedException();
        }

        public string Create(Loan loan)
        {
            if (loan == null)
                throw new ArgumentNullException(nameof(loan));

            var loanEntity = new LoanEntity
            {
                Amount = loan.Amount,
                InterestRate = loan.InterestRate,
                DurationInMonths = loan.DurationInMonths,
                Status = loan.Status,
                StartDate = loan.StartDate,
                EndDate = loan.EndDate,
                RemainingAmount = loan.RemainingAmount,
                BorrowerSin = loan.TheBorrower.Sin
            };

            _context.Loans.Add(loanEntity);
            _context.SaveChanges();

            return loanEntity.IdLoan.ToString();
        }

        public List<Loan> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Borrower?> GetBorrowerBySin(string sin)
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
