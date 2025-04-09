using Domain.Borrowers;
using Domain.Loans;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ApiTP1MethodeDeDev.Dtos.Loan
{
    public class LoanResponse
    {
        public int IdLoan { get; set; }
        public decimal Amount { get; set; }
        public decimal InterestRate { get; set; }
        public int DurationInMonths { get; set; }
        public StatusLoan Status { get; set; }
        public DateTime StartDate { get; set; }

        private DateTime _endDate;
        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                if (StartDate == default || DurationInMonths <= 0)
                    throw new ArgumentException("Invalid loan duration or start date");
                _endDate = StartDate.AddMonths(DurationInMonths);
            }
        }

        public decimal RemainingAmount { get; set; }
        public string BorrowerSin { get; set; }
    }

}
