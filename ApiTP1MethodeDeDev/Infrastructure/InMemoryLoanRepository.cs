using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Loans;
using Domain.Borrowers;
using Microsoft.EntityFrameworkCore;

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
                IdLoan = loan.IdLoan,
                Amount = loan.Amount,
                InterestRate = loan.InterestRate,
                DurationInMonths = loan.DurationInMonths,
                Status = loan.Status,
                StartDate = loan.StartDate,
                RemainingAmount = loan.RemainingAmount,
                BorrowerSin = loan.TheBorrower.Sin
            };

            _context.Loans.Add(loanEntity);
            _context.SaveChanges();

            return loanEntity.IdLoan.ToString();
        }

        public List<Loan> GetAll()
        {
            return _context.Loans
                .Include(l => l.TheBorrower)
                .Select(l => new Loan
                {
                    IdLoan = l.IdLoan,
                    Amount = l.Amount,
                    InterestRate = l.InterestRate,
                    DurationInMonths = l.DurationInMonths,
                    Status = l.Status,
                    StartDate = l.StartDate,
                    RemainingAmount = l.RemainingAmount,
                    TheBorrower = new Borrower(
                        l.TheBorrower.Sin,
                        l.TheBorrower.FirstName,
                        l.TheBorrower.LastName,
                        l.TheBorrower.Phone,
                        l.TheBorrower.Email,
                        l.TheBorrower.Address,
                        l.TheBorrower.Equifax_Result,
                        l.TheBorrower.BankruptyDate,
                        l.TheBorrower.OtherBankLoans,
                        l.TheBorrower.NumberOfLatePayments,
                        l.TheBorrower.EmploymentHistory
                    )
                }).ToList();
        }

        public Loan GetByIdLoan(int idLoan)
        {
            var loanEntity = _context.Loans
                .Include(l => l.TheBorrower)
                .ThenInclude(b => b.OtherBankLoans)
                .Include(l => l.TheBorrower.NumberOfLatePayments)
                .Include(l => l.TheBorrower.EmploymentHistory)
                .FirstOrDefault(l => l.IdLoan == idLoan);

            if (loanEntity == null)
                throw new KeyNotFoundException($"Loan with ID {idLoan} not found.");

            return new Loan
            {
                IdLoan = loanEntity.IdLoan,
                Amount = loanEntity.Amount,
                InterestRate = loanEntity.InterestRate,
                DurationInMonths = loanEntity.DurationInMonths,
                Status = loanEntity.Status,
                StartDate = loanEntity.StartDate,
                RemainingAmount = loanEntity.RemainingAmount,
                TheBorrower = new Borrower(
                    loanEntity.TheBorrower.Sin,
                    loanEntity.TheBorrower.FirstName,
                    loanEntity.TheBorrower.LastName,
                    loanEntity.TheBorrower.Phone,
                    loanEntity.TheBorrower.Email,
                    loanEntity.TheBorrower.Address,
                    loanEntity.TheBorrower.Equifax_Result,
                    loanEntity.TheBorrower.BankruptyDate,
                    loanEntity.TheBorrower.OtherBankLoans,
                    loanEntity.TheBorrower.NumberOfLatePayments,
                    loanEntity.TheBorrower.EmploymentHistory
                )
            };
        }

        public void Update(Loan loan)
        {
            var loanEntity = _context.Loans.FirstOrDefault(l => l.IdLoan == loan.IdLoan);

            if (loanEntity == null)
                throw new KeyNotFoundException($"Loan with ID {loan.IdLoan} not found.");

            loanEntity.Amount = loan.Amount;
            loanEntity.InterestRate = loan.InterestRate;
            loanEntity.DurationInMonths = loan.DurationInMonths;
            loanEntity.Status = loan.Status;
            loanEntity.StartDate = loan.StartDate;
            loanEntity.RemainingAmount = loan.RemainingAmount;
            loanEntity.BorrowerSin = loan.TheBorrower.Sin;

            _context.SaveChanges();
        }

        public Task<Borrower?> GetBorrowerBySin(string sin)
        {
            var borrower = _context.Borrowers
                .Include(b => b.OtherBankLoans)
                .Include(b => b.NumberOfLatePayments)
                .Include(b => b.EmploymentHistory)
                .FirstOrDefault(b => b.Sin == sin);

            return Task.FromResult<Borrower?>(borrower == null ? null : new Borrower(
                borrower.Sin,
                borrower.FirstName,
                borrower.LastName,
                borrower.Phone,
                borrower.Email,
                borrower.Address,
                borrower.Equifax_Result,
                borrower.BankruptyDate,
                borrower.OtherBankLoans,
                borrower.NumberOfLatePayments,
                borrower.EmploymentHistory
            ));
        }

        public object AddLoanAsync(Loan loan)
        {
            return Task.Run(() => Create(loan));
        }
    }
}
