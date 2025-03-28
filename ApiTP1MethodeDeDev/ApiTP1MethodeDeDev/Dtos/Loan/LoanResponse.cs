﻿using Domain.Borrowers;
using Domain.Loans;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ApiTP1MethodeDeDev.Dtos.Loan
{
    public class LoanResponse
    {
        [Key]
        [Required]
        [MinLength(1)]
        [Description("It is an unique integer number of a loan ")]
        public int IdLoan { get; set; }

        [Required]
        [Description("The whole amount of the loan")]
        public decimal Amount { get; set; }

        [Required]
        [Description("The rate of interests")]
        public decimal InterestRate { get; set; }

        [Required]
        [Description("Specify the duration of the paiements in months")]
        public int DurationInMonths { get; set; }

        [Required]
        [Description("The state of the loan. By default it is Pending")]
        public StatusLoan Status { get; set; } = StatusLoan.Pending;

        [Required]
        [Description("The date when paiements will start")]
        public DateTime StartDate { get; set; } = DateTime.Now;

        [Required]
        [Description("The end date of paiements")]
        public DateTime EndDate { get; set; }

        [Required]
        [Description("The exact and precise amount that the borrower undertakes to repay to the bank as part of his loan")]
        public decimal RemainingAmount { get; set; }

        [Required]
        public Borrower TheBorrower { get; set; }

        public List<decimal> Loans { get; set; } = new List<decimal>();

    }
}
