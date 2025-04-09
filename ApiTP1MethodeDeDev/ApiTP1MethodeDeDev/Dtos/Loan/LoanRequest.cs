using Domain.Borrowers;
using Domain.Loans;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ApiTP1MethodeDeDev.Dtos.Loan
{
    public class LoanRequest
    {
        [Required(ErrorMessage = "Amount must be greater than zero.")]
        [Range(1, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public decimal Amount { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "Interest rate must be between 0 and 100%.")]
        public decimal InterestRate { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Duration must be at least 1 month.")]
        public int DurationInMonths { get; set; }

        [Required]
        public StatusLoan Status { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public decimal RemainingAmount { get; set; }

        [Required(ErrorMessage = "A borrower must be assigned to the loan.")]
        public string BorrowerSin { get; set; }
        public int IdLoan { get; set; }
    }

}
