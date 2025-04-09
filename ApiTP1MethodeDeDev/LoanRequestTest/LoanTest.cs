using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Domain.Loans;
using Domain.Borrowers;

namespace ApiTP1MethodeDeDev.Test
{
    [TestClass]
    public class LoanTest
    {
        private Loan loan;
        private Borrower borrower;

        [TestInitialize]
        public void Setup()
        {
            borrower = new Borrower
            {
                Sin = "123-456-789",
                FirstName = "John",
                LastName = "Doe",
                Phone = "123-456-7890",
                Email = "john.doe@example.com",
                Address = "123 Main St",
                MonthlyIncome = 5000m,
                Equifax_Result = 700,
                BankruptyDate = DateTime.MinValue
            };

            loan = new Loan
            {
                Amount = 10000m,
                InterestRate = 5.5m,
                DurationInMonths = 24,
                StartDate = DateTime.Now.AddMonths(1), 
                EndDate = DateTime.Now.AddMonths(25), 
                RemainingAmount = 10000m,
                TheBorrower = borrower,
                MonthlyPayment = 500m
            };
        }

        // Test 1: Given a valid loan, When ValidateLoanDates is called, Then it should pass without exception
        [TestMethod]
        public void GivenValidLoan_WhenValidateLoanDates_ThenItShouldPass()
        {
            // Given
            var validLoan = new Loan
            {
                StartDate = DateTime.Now.AddMonths(1),  
                EndDate = DateTime.Now.AddMonths(25),  
                DurationInMonths = 24
            };

            // When
            validLoan.ValidateLoanDates(validLoan.StartDate, validLoan.EndDate, validLoan.DurationInMonths);

            // Then
            Assert.IsTrue(true);
        }

        // Test 2: Given a loan with a start date in the future, When ValidateLoanDates is called, Then it should throw an ArgumentException
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GivenLoanWithStartDateInFuture_WhenValidateLoanDates_ThenItShouldThrowArgumentException()
        {
            // Given
            var invalidLoan = new Loan
            {
                StartDate = DateTime.Now.AddMonths(2),  
                EndDate = DateTime.Now.AddMonths(26),   
                DurationInMonths = 24
            };

            // When
            invalidLoan.ValidateLoanDates(invalidLoan.StartDate, invalidLoan.EndDate, invalidLoan.DurationInMonths);

            // Then
            // Expected exception: ArgumentException
        }

        // Test 3: Given a loan with an invalid duration that doesn't match the start and end dates, When ValidateLoanDates is called, Then it should throw an ArgumentException
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GivenLoanWithInvalidDuration_WhenValidateLoanDates_ThenItShouldThrowArgumentException()
        {
            // Given
            var invalidLoan = new Loan
            {
                StartDate = DateTime.Now.AddMonths(1),
                EndDate = DateTime.Now.AddMonths(25), 
                DurationInMonths = 12 
            };

            // When
            invalidLoan.ValidateLoanDates(invalidLoan.StartDate, invalidLoan.EndDate, invalidLoan.DurationInMonths);

            // Then
            // Expected exception: ArgumentException
        }

        // Test 4: Given a loan where the end date is not after the start date, When ValidateLoanDates is called, Then it should throw an ArgumentException
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GivenLoanWithEndDateBeforeStartDate_WhenValidateLoanDates_ThenItShouldThrowArgumentException()
        {
            // Given
            var invalidLoan = new Loan
            {
                StartDate = DateTime.Now.AddMonths(2),
                EndDate = DateTime.Now.AddMonths(1), 
                DurationInMonths = 24
            };

            // When
            invalidLoan.ValidateLoanDates(invalidLoan.StartDate, invalidLoan.EndDate, invalidLoan.DurationInMonths);

            // Then
            // Expected exception: ArgumentException
        }
    }
}
