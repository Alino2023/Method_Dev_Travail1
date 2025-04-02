using System.ComponentModel;

namespace ApiTP1MethodeDeDev.Dtos
{
    public class OtherBankLoanReponse
    {

        public int BankId { get; set; }

        public string BankName { get; set; }

        [Description("Loan mensuality")]
        public decimal Mensuality { get; set; }

        [Description("Loan Remaining Balance")]
        public decimal RemainingBalance { get; set; }

        [Description("Reason for the loan ")]
        public string Reason { get; set; }
    }
}
