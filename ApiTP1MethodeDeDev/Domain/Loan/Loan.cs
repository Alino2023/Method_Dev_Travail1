using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Borrower;

namespace Domain.Loan
{
    public class Loan
    {
        public int IdLoan { get; set; }
        public decimal Amount { get; set; }
        public decimal InterestRate { get; set; }
        public int DurationInMonths { get; set; }
        public StatusLoan Status { get; set; } = StatusLoan.Pending;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal RemainingAmount { get; set; }
        public string BorrowerSin {  get; set; }
        public BorrowerService TheBorrower { get; set; }

    }
}
