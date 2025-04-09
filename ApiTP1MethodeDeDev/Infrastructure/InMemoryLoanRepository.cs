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
            return _context.Loans.ToList().Select(l => new Loan(l.IdLoan, l.Amount, l.InterestRate, l.DurationInMonths, l.Status, l.StartDate, l.EndDate, l.RemainingAmount, l.BorrowerSin, null)).ToList();
        }

        public Task<Borrower?> GetBorrowerBySin(string sin)
        {
            throw new NotImplementedException();
        }

        //public Task<Borrower?> GetBorrowerBySin(string sin)
        //{
        //    return _context.Borrowers.ToList().Select(b => new Borrower(b.Sin, b.FirstName, b.LastName, b.Phone, b.Email, b.Address, b.Had_Bankrupty_In_Last_Six_Years, b.BankruptyDate, b.Equifax_Result, b.NumberOfLatePayments, b.MonthlyIncome, b.DebtRatio, b.OtherBankLoans, b.EmploymentHistory)).ToList();
        //}

        public Loan GetByIdLoan(int idLoan)
        {
            return _context.Loans.Select
                (l => new Loan(l.IdLoan, l.Amount, l.InterestRate, l.DurationInMonths, l.Status, l.StartDate, l.EndDate, l.RemainingAmount, l.BorrowerSin, null)).FirstOrDefault(l => l.IdLoan == idLoan);
        }

        public void Update(Loan loan)
        {
            var existingLoan = _context.Loans.FirstOrDefault(l => l.IdLoan == loan.IdLoan);
            if (existingLoan != null)
            {
                _context.Remove(existingLoan);
                _context.Add(loan);
            }
            else
            {
                throw new Exception($"Loan with ID {loan.IdLoan} not found.");
            }
        }
    }
}
