using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Borrower
{
    public enum LoanStatus
    {
        EnCours,
        EnAttente,
        Paye,
        Ferme
    }
    public class Loan
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public decimal Mensuality { get; set; }

        [Required]
        public decimal InterestRate { get; set; }

        [Required]
        public int DurationInMonths { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }

        [Required]
        public DateTime StartingDateOfTheLoan { get; set; }

        [Required]
        public LoanStatus Status { get; set; } = LoanStatus.EnAttente;

        [Required]
        public string BorrowerId { get; set; }
        public Borrower Borrower { get; set; }

        public Loan()
        {
        }

        public Loan(decimal totalAmount, decimal interestRate, int durationInMonths, Borrower borrower)
        {
            TotalAmount = totalAmount;
            InterestRate = interestRate;
            DurationInMonths = durationInMonths;
            Borrower = borrower;
            BorrowerId = borrower.Sin;
            StartingDateOfTheLoan = DateTime.Now;
            Mensuality = CalculateMensuality();
            Status = LoanStatus.EnAttente;
        }

        public decimal CalculateMensuality()
        {
            if (InterestRate == 0)
            {
                return TotalAmount / DurationInMonths;
            }

            decimal monthlyRate = InterestRate / 12 / 100; // taux annuel en taux mensuel
            return TotalAmount * (monthlyRate / (1 - (decimal)Math.Pow(1 + (double)monthlyRate, -DurationInMonths)));
        }
    }
}
