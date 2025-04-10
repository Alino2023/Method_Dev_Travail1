﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Domain.Bank;
using Domain.Emploi;
using Domain.Loans;
using Domain.LatePayment;
using System.Reflection.Metadata.Ecma335;

namespace Domain.Borrowers
{
    public class Borrower
    {
        [Key]
        [Required]
        [MinLength(9)]
        [MaxLength(9)]
        [Description("User's Social insurance Number")]
        //[Example("123456789")]
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
        public bool Had_Bankrupty_In_Last_Six_Years { get; set; }

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
        public decimal DebtRatio{ get; set; }
        //public Decimal DebtRatio { get => CalculateDebtRatio(); }


        [Required]
        [Description("List of monthly payments from other banks")]
        public List<OtherBankLoan> OtherBankLoans { get; set; }

        [Required]
        [Description("List of borrower's job")]
        public List<Job> EmploymentHistory { get; set; } = new List<Job>();


        public List<Loan> Loans { get; set; } = new List<Loan>();
        public List<decimal> ActiveLoanPayments { get; set; } = new List<decimal>();

        //public IBorrowerService BorrowerService { get; set; }


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
        public void VerifyBankrupty()
        {
            if (BankruptyDate != default && (DateTime.Now - BankruptyDate).TotalDays <= 6 * 365)
            {
                Had_Bankrupty_In_Last_Six_Years = true;
            }
            else
            {
                Had_Bankrupty_In_Last_Six_Years = false;
            }
        }
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
            //BorrowerService = borrowerService;
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

             DebtRatio= ((totalLoanPayments / jobActuel.MentualSalary) * 100);
        }

        public string ClassifyRisk()
        {
            int jobsInLastTwoYears = EmploymentHistory.Count(job => job.StartingDate >= DateTime.Now.AddYears(-2));
            Job currentJob = EmploymentHistory.OrderByDescending(job => job.StartingDate).FirstOrDefault();
            bool currentJobIsLessThan12Months = ((currentJob != null) && ((DateTime.Now - currentJob.StartingDate).TotalDays < 365));

            if (IsHighRisk(jobsInLastTwoYears, currentJob, currentJobIsLessThan12Months))
                return "High Risk";

            if (IsMediumRisk(jobsInLastTwoYears, currentJob, currentJobIsLessThan12Months))
                return "Medium Risk";

            if (IsLowRisk(jobsInLastTwoYears, currentJob, currentJobIsLessThan12Months))
                return "Low Risk";

            return "Error in the classification of risk";
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
            if (!Had_Bankrupty_In_Last_Six_Years && Equifax_Result >= 650 && Equifax_Result < 750 && DebtRatio >= 0.25m && DebtRatio < 0.4m && NumberOfLatePayments.Where(n => n.LatePaymentDate >= DateTime.Now.AddMonths(-6)).Count() <= 1
                && (jobsInLastTwoYears >= 3 || currentJobIsLessThan12Months))
            {
                return true ;
            }
            return false;
        }
        private bool IsLowRisk(int jobsInLastTwoYears, Job currentJob, bool currentJobIsLessThan12Months)
        {
            if (!Had_Bankrupty_In_Last_Six_Years && Equifax_Result > 750 &&
                NumberOfLatePayments.Where(n => n.LatePaymentDate >= DateTime.Now.AddMonths(-6)).Count() == 0 && DebtRatio < 0.25m &&
                jobsInLastTwoYears <= 2 && !currentJobIsLessThan12Months)
            {
                return true;
            }
            return false;
        }

    }
}
