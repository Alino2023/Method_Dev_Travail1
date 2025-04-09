using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Borrowers;

namespace Domain.Loans
{
    public class Loan
    {
        [Key]
        [Required]
        [MinLength(1)]
        [Description("It is an unique integer number of a loan ")]
        public int IdLoan { get; set; }

        [Required]
        [Description("The whole amount of the loan")]
        public decimal Amount { get; set; }

        [Required]
        [Description("The rate of interests")]
        public decimal InterestRate { get; set; }

        [Required]
        [Description("Specify the duration of the paiements in months")]
        public int DurationInMonths { get; set; }

        [Required]
        [Description("The state of the loan. By default it is Pending")]
        public StatusLoan Status { get; set; } = StatusLoan.Pending;

        [Required]
        [Description("The date when paiements will start")]
        public DateTime StartDate { get; set; } = DateTime.Now;

        [Required]
        [Description("The end date of paiements")]
        public DateTime EndDate { get; set; }

        [Required]
        [Description("The exact and precise amount that the borrower undertakes to repay to the bank as part of his loan")]
        public decimal RemainingAmount { get; set; }

        [Required]
        public string BorrowerSin { get; set; }

        [Required]
        [Description("The monthly payment amount for the loan")]
        public decimal MonthlyPayment { get; set; }


        public List<decimal> Loans { get; set; } = new List<decimal>();


        public Loan(int idLoan, decimal amount, decimal interestRate, int durationInMonths, StatusLoan status, DateTime startDate, DateTime endDate, decimal remainingAmount, string borrowerSin, string borrowersin)
        {
            ValidateLoanDates(startDate, endDate, durationInMonths);

            IdLoan = idLoan;
            Amount = amount;
            InterestRate = interestRate;
            DurationInMonths = durationInMonths;
            Status = status;
            StartDate = startDate;
            EndDate = StartDate.AddMonths(DurationInMonths);
            RemainingAmount = remainingAmount;
            BorrowerSin = borrowersin;
        }

        public void ValidateLoanDates(DateTime startDate, DateTime endDate, int durationInMonths)
        {
            if (startDate > DateTime.Now)
            {
                throw new ArgumentException("The loan start date cannot be in the future.");
            }

            if (endDate <= startDate)
            {
                throw new ArgumentException("The loan end date must be after the start date.");
            }

            if (endDate != startDate.AddMonths(durationInMonths))
            {
                throw new ArgumentException("The loan duration does not match the start and end dates.");
            }
        }
        public Loan()
        {

        }
    }
}
