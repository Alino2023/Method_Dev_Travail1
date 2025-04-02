using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Domain.Bank;
using Domain.Emploi;
using Domain.Loans;
using Domain.LatePayment;

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
        
        [Description("User's bankrupty times in the last 6years")]
        public bool Had_Bankrupty_In_Last_Six_Years { get=> BankruptyDate != default && (DateTime.Now - BankruptyDate).TotalDays <= 6 * 365; }

        [Required]
        [Description("User's bankrupty date if exists ")]
        public DateTime BankruptyDate { get; set; }

        [Required]
        [Description("Credit Score")]
        public int Equifax_Result { get; set; }

        [Required]
        [Description("Number Of borrower's Late Payements")]
        public List<LatePaymentBorrower> NumberOfLatePayments {  get; set; }

        [Required]
        public decimal MonthlyIncome { get; set; }

        [Required]
        [Description("Debt Ratio")]
        public Decimal DebtRatio { get; set; }


        [Required]
        [Description("List of monthly payments from other banks")]
        public List<OtherBankLoan> OtherBankLoans { get; set; }

        [Required]
        [Description("List of borrower's job")]
        public List<Job> EmploymentHistory { get; set; } = new List<Job>();


        public List<Loan> Loans { get; set; } = new List<Loan>();
        public List<decimal> ActiveLoanPayments { get; set; } = new List<decimal>();


        public Borrower()
        {
        }

        //public Borrower(string sin, string firstName, string lastName, string phone, string email, string address)
        //{
        //    Sin = sin;
        //    FirstName = firstName;
        //    LastName = lastName;
        //    Phone = phone;
        //    Email = email;
        //    Address = address;
         
        //}

        public Borrower(string sin, string firstName, string lastName, string phone, string email, string address, int equifax_Result, DateTime bankruptyDate, List<OtherBankLoan> otherBankLoans, List<LatePaymentBorrower> numberOfLatePayments, List<Job> employmentHistory)
        {
            Sin = sin;
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            Email = email;
            Address = address;
            Equifax_Result = equifax_Result;
            BankruptyDate = bankruptyDate;
            OtherBankLoans = otherBankLoans;
            NumberOfLatePayments = numberOfLatePayments;
            EmploymentHistory = employmentHistory;    
        }

        public void CalculateDebtRatio()
        {
            Job jobActuel = EmploymentHistory.OrderByDescending(job => job.StartingDate).FirstOrDefault();

            if (jobActuel == null)
            {
                throw new InvalidOperationException("No employment found in the history to calculate the debt ratio.");
            }

            if (jobActuel.MentualSalary <= 0)
            {
                throw new InvalidOperationException("The current job's salary must be greater than zero to calculate the debt ratio.");
            }

            decimal totalLoanPayments = OtherBankLoans.Sum(loan => loan.Mensuality) + Loans.Sum(loan => loan.MonthlyPayment);

            DebtRatio = (totalLoanPayments / jobActuel.MentualSalary) * 100;
        }
        public string ClassifyRisk()
        {
            int jobsInLastTwoYears = EmploymentHistory.Count(job => job.StartingDate >= DateTime.Now.AddYears(-2));
            Job currentJob = EmploymentHistory.OrderByDescending(job => job.StartingDate).FirstOrDefault();
            bool currentJobIsLessThan12Months = ((currentJob != null) && ((DateTime.Now - currentJob.StartingDate).TotalDays < 365));

            return  IsHighRisk(jobsInLastTwoYears, currentJob, currentJobIsLessThan12Months) ? "High Risk" :
                    IsMediumRisk(jobsInLastTwoYears, currentJob, currentJobIsLessThan12Months) ? "Medium Risk" :
                    IsLowRisk(jobsInLastTwoYears, currentJob, currentJobIsLessThan12Months) ? "Low Risk" :
                    "Error in the classification of risk";
        }
        private bool IsHighRisk(int jobsInLastTwoYears, Job currentJob, bool currentJobIsLessThan12Months)
        {
            if (Had_Bankrupty_In_Last_Six_Years || Equifax_Result < 650 || DebtRatio >= 0.4m)
            {
                return true;
            }
            return false;
        }
        private bool IsMediumRisk(int jobsInLastTwoYears, Job currentJob, bool currentJobIsLessThan12Months)
        {
            if (!Had_Bankrupty_In_Last_Six_Years && Equifax_Result >= 650 && Equifax_Result < 750 && DebtRatio >= 0.25m && DebtRatio < 0.4m && NumberOfLatePayments.Count <= 1
                && (jobsInLastTwoYears >= 3 || currentJobIsLessThan12Months))
            {
                return true ;
            }
            return false;
        }
        private bool IsLowRisk(int jobsInLastTwoYears, Job currentJob, bool currentJobIsLessThan12Months)
        {
            if (!Had_Bankrupty_In_Last_Six_Years && Equifax_Result > 750 && NumberOfLatePayments.Count == 0 && DebtRatio < 0.25m && jobsInLastTwoYears <= 2 && !currentJobIsLessThan12Months)
            {
                return true;
            }
            return false;
        }

    }
}
