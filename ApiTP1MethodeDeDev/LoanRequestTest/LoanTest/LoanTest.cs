using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Domain.Loans;
using Domain.Borrowers;

namespace Tests.LoanTest
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
<<<<<<< HEAD
        public  void GivenValidLoan_WhenValidateLoanDates_ThenItShouldPass()
=======
        public void GivenValidLoan_WhenValidateLoanDates_ThenItShouldPass()
>>>>>>> parent of f589cb5 (delete of the existing code to searsh for the one in develop)
        {
            // Given
            var amount = 10000m;
            var interestRate = 5.5m;
            var durationInMonths = 24;
            var startDate = DateTime.Now.AddMonths(1); // Start date in the future
            var borrower = new Borrower
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

            var loan = new Loan
            {
                Amount = amount,
                InterestRate = interestRate,
                DurationInMonths = durationInMonths,
                StartDate = startDate,
                EndDate = startDate.AddMonths(durationInMonths),
                RemainingAmount = amount,
                TheBorrower = borrower,
                MonthlyPayment = amount / durationInMonths
            };

            // When
            try
            {
                loan.ValidateLoanDates(loan.StartDate, loan.StartDate.AddMonths(loan.DurationInMonths), loan.DurationInMonths);
            }
            catch (ArgumentException ex)
            {
                Assert.Fail($"Validation failed with exception: {ex.Message}");
            }

            // Then
            // If no exception is thrown, the test passes.
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

        [TestMethod]
        public void GivenValidLoanDetails_WhenLoanIsCreated_ThenLoanPropertiesShouldBeCorrectlyAssigned()
        {
            // Given
            var amount = 10000m;
            var interestRate = 5.5m;
            var durationInMonths = 24;
            var startDate = DateTime.Now.AddMonths(1); // Start date in the future
            var borrower = new Borrower
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

            // When
            var loan = new Loan
            {
                Amount = amount,
                InterestRate = interestRate,
                DurationInMonths = durationInMonths,
                StartDate = startDate,
                EndDate = startDate.AddMonths(durationInMonths),
                RemainingAmount = amount,
                TheBorrower = borrower,
                MonthlyPayment = amount / durationInMonths
            };

            // Then
            Assert.AreEqual(amount, loan.Amount, "The loan amount is not correctly assigned.");
            Assert.AreEqual(interestRate, loan.InterestRate, "The interest rate is not correctly assigned.");
            Assert.AreEqual(durationInMonths, loan.DurationInMonths, "The loan duration is not correctly assigned.");
            Assert.AreEqual(startDate, loan.StartDate, "The loan start date is not correctly assigned.");
            Assert.AreEqual(startDate.AddMonths(durationInMonths), loan.EndDate, "The loan end date is not correctly assigned.");
            Assert.AreEqual(amount, loan.RemainingAmount, "The loan remaining amount is not correctly assigned.");
            Assert.AreEqual(borrower, loan.TheBorrower, "The borrower is not correctly assigned.");
            Assert.AreEqual(amount / durationInMonths, loan.MonthlyPayment, "The monthly payment is not correctly calculated.");
        }

    }
}
