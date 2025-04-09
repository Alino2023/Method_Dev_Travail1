using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Loans;
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

            // Validation des dates
            loan.ValidateLoanDates(loan.StartDate, loan.EndDate, loan.DurationInMonths);

            var loanEntity = new LoanEntity
            {
                IdLoan = loan.IdLoan,
                Amount = loan.Amount,
                InterestRate = loan.InterestRate,
                DurationInMonths = loan.DurationInMonths,
                Status = loan.Status,
                StartDate = loan.StartDate,
                EndDate = loan.StartDate.AddMonths(loan.DurationInMonths),
                RemainingAmount = loan.RemainingAmount,
                BorrowerSin = loan.BorrowerSin
            };

            _context.Loans.Add(loanEntity);
            _context.SaveChanges();

            return loanEntity.IdLoan.ToString();
        }

        public async Task<string> AddLoanAsync(Loan loan)
        {
            if (loan == null)
                throw new ArgumentNullException(nameof(loan));

            loan.ValidateLoanDates(loan.StartDate, loan.EndDate, loan.DurationInMonths);

            var loanEntity = new LoanEntity
            {
                IdLoan = loan.IdLoan,
                Amount = loan.Amount,
                InterestRate = loan.InterestRate,
                DurationInMonths = loan.DurationInMonths,
                Status = loan.Status,
                StartDate = loan.StartDate,
                EndDate = loan.StartDate.AddMonths(loan.DurationInMonths),
                RemainingAmount = loan.RemainingAmount,
                BorrowerSin = loan.BorrowerSin
            };

            _context.Loans.Add(loanEntity);
            await _context.SaveChangesAsync();

            return loanEntity.IdLoan.ToString();
        }

        public List<Loan> GetAll()
        {
            return _context.Loans
                .AsNoTracking()
                .Select(l => new Loan
                {
                    IdLoan = l.IdLoan,
                    Amount = l.Amount,
                    InterestRate = l.InterestRate,
                    DurationInMonths = l.DurationInMonths,
                    Status = l.Status,
                    StartDate = l.StartDate,
                    EndDate = l.EndDate,
                    RemainingAmount = l.RemainingAmount,
                    BorrowerSin = l.BorrowerSin,
                    MonthlyPayment = CalculateMonthlyPayment(l.Amount, l.InterestRate, l.DurationInMonths)
                })
                .ToList();
        }

        public Loan GetByIdLoan(int idLoan)
        {
            var l = _context.Loans
                .AsNoTracking()
                .FirstOrDefault(x => x.IdLoan == idLoan);

            if (l == null)
                throw new KeyNotFoundException($"Loan with ID {idLoan} not found.");

            return new Loan
            {
                IdLoan = l.IdLoan,
                Amount = l.Amount,
                InterestRate = l.InterestRate,
                DurationInMonths = l.DurationInMonths,
                Status = l.Status,
                StartDate = l.StartDate,
                EndDate = l.EndDate,
                RemainingAmount = l.RemainingAmount,
                BorrowerSin = l.BorrowerSin,
                MonthlyPayment = CalculateMonthlyPayment(l.Amount, l.InterestRate, l.DurationInMonths)
            };
        }

        private decimal CalculateMonthlyPayment(decimal amount, decimal rate, int duration)
        {
            if (rate == 0)
                return Math.Round(amount / duration, 2);

            var monthlyRate = rate / 12 / 100;
            var denominator = (decimal)(Math.Pow(1 + (double)monthlyRate, duration) - 1);
            if (denominator == 0) return amount;

            var monthlyPayment = amount * monthlyRate * (decimal)Math.Pow(1 + (double)monthlyRate, duration) / denominator;
            return Math.Round(monthlyPayment, 2);
        }
    }
}
