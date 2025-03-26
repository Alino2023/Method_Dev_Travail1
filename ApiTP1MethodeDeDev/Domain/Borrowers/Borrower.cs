using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Bank;
using Domain.Emploi;

namespace Domain.Borrowers
{
    public class Borrower
    {
        [Key]
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

        [Required]
        [Description("User's bankrupty times in the last 6years")]
        public bool Had_Bankrupty_In_Last_Six_Years { get; set; }

        [Description("User's bankrupty date if exists ")]
        DateTime BankruptyDate { get; set; }

        [Required]
        [Description("Credit Score")]
        public int Equifax_Result { get; set; }

        [Required]
        [Description("Number Of borrower's Late Payements")]
        public int NumberOfLatePayements {  get; set; }


        [Required]
        [Description("Debt Ratio")]
        public Decimal DebtRatio {  get; set; }


        [Required]
        [Description("List of monthly payments from other banks")]
        public List<OtherBank> OtherBankLoans { get; set; }
        public List<Job> EmploymentHistory { get; set; } = new List<Job>();
        public List<Loan> Loans { get; set; } = new List<Loan>();


        public Borrower()
        {
        }

        public Borrower(string sin, string firstName, string lastName, string phone, string email, string address)
        {
            Sin = sin;
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            Email = email;
            Address = address;
        
        }

        public decimal CalculateDebtRatio()
        {
            decimal totalLoanPayments = ActiveLoanPayments.Sum();
            return (MonthlyIncome > 0) ? (totalLoanPayments / MonthlyIncome) * 100 : 0;
        }

        public Borrower(string sin, string firstName, string lastName, string phone, string email, string address)
        {
            Sin = sin;
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            Email = email;
            Address = address;
        }
    }
}
