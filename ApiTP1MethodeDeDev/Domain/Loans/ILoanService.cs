﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Borrowers;

namespace Domain.Loans
{
    public interface ILoanService
    {
        public IList<Loan> GetAll();
        Loan GetByIdLoan(int idLoan);
        string Create(Loan loan);
    }
}
