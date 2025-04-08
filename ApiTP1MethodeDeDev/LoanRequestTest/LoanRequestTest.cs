using Domain.Borrowers;
using Domain.Loans;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain.LatePayment;
using Domain.Emploi;
using Domain.Bank;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using ApiTP1MethodeDeDev.Dtos.Loan;

namespace ApiTP1MethodeDeDev.Tests
{
    [TestClass]
    public class LoanRequestTest
    {
        private Mock<ILoanRepository> _loanRepositoryMock;
        private Borrower _validBorrower;

    }
}
