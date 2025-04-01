using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Domain.Bank;
using Domain.Emploi;
using Domain.LatePayment;

namespace ApiTP1MethodeDeDev.Dtos
{
    public class BorrowerReponse
    {
        [Required]
        [MinLength(9)]
        [MaxLength(11)]
        [Description("User's Social insurance Number")]
        //[Example("123-456-789")]
        public string Sin { get; set; }

        [Required]
        [MaxLength(255)]
        [Description("User's FirstName")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(255)]
        [Description("User's LastName")]
        public string LastName { get; set; }

        [Required]
        [MaxLength(11)]
        [Description("User's Phone")]
        public string Phone { get; set; }

        [Required]
        [MaxLength(255)]
        [Description("User's Email")]
        public string Email { get; set; }

        [Required]
        [MaxLength(255)]
        [Description("User's Address")]
        public string Address { get; set; }

        [Description("User's bankrupty date if exists ")]
        public DateTime BankruptyDate { get; set; }

        [Required]
        [Description("Credit Score")]
        public int Equifax_Result { get; set; }

        [Required]
        [Description("Number Of borrower's Late Payements")]
        public List<LatePaymentBorrower> NumberOfLatePayments { get; set; }

        [Required]
        [Description("List of monthly payments from other banks")]
        public List<OtherBankLoan> OtherBankLoans { get; set; }

        [Required]
        [Description("List of borrower's job")]
        public List<Job> EmploymentHistory { get; set; } = new List<Job>();

    }
}
