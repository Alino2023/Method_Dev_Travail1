﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Borrowers;

namespace Domain.Loans
{
    public class LoanService : ILoanService
    {
        private readonly ILoanRepository _loanRepository;

        public LoanService(ILoanRepository loanRepository)
        {
            _loanRepository = loanRepository;
        }
       
        public string Create(Loan loan)
        {
            if (loan == null)
            {
                return "The loan cannot be void";
            }

            if (loan.Amount <= 0)
            {
                return "The loan amount must be positive.";
            }

            if (loan.DurationInMonths <= 0)
            {
                return "The term of the loan must be greater than 0.";
            }

            loan.RemainingAmount = loan.Amount;

            return _loanRepository.Create(loan);
                        
        }
        public IList<Loan> GetAll()
        {
            return _loanRepository.GetAll();
        }

        public async Task<Borrower?> GetBorrowerBySin(string sin)
        {
            if (string.IsNullOrWhiteSpace(sin))
            {
                throw new ArgumentException("SIN cannot be null or empty.", nameof(sin));
            }

            return await _loanRepository.GetBorrowerBySin(sin);
        }

        public Loan GetByIdLoan(int idLoan)
        {
            var loan = _loanRepository.GetByIdLoan(idLoan); 
            if (loan == null)
            {
                return null!;
            }

            return loan;
        }

        public void Update(Loan loan)
        {
            if (loan == null)
            {
                throw new ArgumentNullException(nameof(loan), "The loan cannot be void.");
            }
            _loanRepository.Update(loan);
        }
    }
}
