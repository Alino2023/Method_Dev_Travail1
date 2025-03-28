using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Bank
{
    public class OtherBankLoan
    {

        [Required]
        [Description("Loan mensuality")]
        public decimal Mensuality { get; set; }

        [Required]
        [Description("Loan Remaining Balance")]
        public decimal RemainingBalance {  get; set; }
        [Required]
        [Description("Reason for the loan ")]
        public string Reason { get; set; }
        public decimal MonthlyPayment { get; set; }
    }
}
