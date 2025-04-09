using Microsoft.VisualStudio.TestTools.UnitTesting;
using ApiTP1MethodeDeDev.Dtos.Loan;
using Domain.Borrowers;
using System;
using ApiTP1MethodeDeDev.Dtos;
using Domain.Loans;
using System.ComponentModel.DataAnnotations;

namespace Tests.LoanRequestTest
{
    [TestClass]
    public class LoanRequestTest
    {
        // Test 1: Given a valid LoanRequest, When validated, Then it should pass without errors
        [TestMethod]
        public void GivenValidLoanRequest_WhenValidated_ThenItShouldPass()
        {
            // Given
            var borrower = new BorrowerResquest
            {
                Sin = "123-456-789",
                FirstName = "John",
                LastName = "Doe",
                Phone = "123-456-7890",
                Email = "john.doe@example.com",
                Address = "123 Main St",
            };

            var loanRequest = new LoanRequest
            {
                Amount = 10000m,
                InterestRate = 5.5m,
                DurationInMonths = 24,
                Status = StatusLoan.Approved, // Assuming StatusLoan is an enum
                StartDate = DateTime.Now.AddMonths(1),
                RemainingAmount = 10000m,
                TheBorrower = borrower,
                IdLoan = 1
            };

            // When
            var validationResults = ValidateLoanRequest(loanRequest);

            // Then
            Assert.AreEqual(0, validationResults.Count, "There should be no validation errors.");
        }

        // Test 2: Given a loan request with an invalid amount (0), When validated, Then it should fail
        [TestMethod]
        public void GivenLoanRequestWithInvalidAmount_WhenValidated_ThenItShouldFail()
        {
            // Given
            var borrower = new BorrowerResquest
            {
                Sin = "123-456-789",
                FirstName = "John",
                LastName = "Doe",
                Phone = "123-456-7890",
                Email = "john.doe@example.com",
                Address = "123 Main St",
            };

            var loanRequest = new LoanRequest
            {
                Amount = 0m,  // Invalid amount
                InterestRate = 5.5m,
                DurationInMonths = 24,
                Status = StatusLoan.Approved,
                StartDate = DateTime.Now.AddMonths(1),
                RemainingAmount = 10000m,
                TheBorrower = borrower,
                IdLoan = 1
            };

            // When
            var validationResults = ValidateLoanRequest(loanRequest);

            // Then
            Assert.IsTrue(validationResults.Count > 0, "Validation should fail for amount 0.");
            Assert.AreEqual("Amount must be greater than zero.", validationResults[0].ErrorMessage);
        }

        // Test 3: Given a loan request with a negative interest rate, When validated, Then it should fail
        [TestMethod]
        public void GivenLoanRequestWithNegativeInterestRate_WhenValidated_ThenItShouldFail()
        {
            // Given
            var borrower = new BorrowerResquest
            {
                Sin = "123-456-789",
                FirstName = "John",
                LastName = "Doe",
                Phone = "123-456-7890",
                Email = "john.doe@example.com",
                Address = "123 Main St",
            };

            var loanRequest = new LoanRequest
            {
                Amount = 10000m,
                InterestRate = -5.5m,  // Invalid negative interest rate
                DurationInMonths = 24,
                Status = StatusLoan.Approved,
                StartDate = DateTime.Now.AddMonths(1),
                RemainingAmount = 10000m,
                TheBorrower = borrower,
                IdLoan = 1
            };

            // When
            var validationResults = ValidateLoanRequest(loanRequest);

            // Then
            Assert.IsTrue(validationResults.Count > 0, "Validation should fail for negative interest rate.");
            Assert.AreEqual("Interest rate must be between 0 and 100%.", validationResults[0].ErrorMessage);
        }

        // Test 4: Given a loan request with an invalid duration (less than 1 month), When validated, Then it should fail
        [TestMethod]
        public void GivenLoanRequestWithInvalidDuration_WhenValidated_ThenItShouldFail()
        {
            // Given
            var borrower = new BorrowerResquest
            {
                Sin = "123-456-789",
                FirstName = "John",
                LastName = "Doe",
                Phone = "123-456-7890",
                Email = "john.doe@example.com",
                Address = "123 Main St",
            };

            var loanRequest = new LoanRequest
            {
                Amount = 10000m,
                InterestRate = 5.5m,
                DurationInMonths = 0,  // Invalid duration
                Status = StatusLoan.Approved,
                StartDate = DateTime.Now.AddMonths(1),
                RemainingAmount = 10000m,
                TheBorrower = borrower,
                IdLoan = 1
            };

            // When
            var validationResults = ValidateLoanRequest(loanRequest);

            // Then
            Assert.IsTrue(validationResults.Count > 0, "Validation should fail for duration less than 1.");
            Assert.AreEqual("Duration must be at least 1 month.", validationResults[0].ErrorMessage);
        }

        // Helper method to validate LoanRequest
        private static List<ValidationResult> ValidateLoanRequest(LoanRequest loanRequest)
        {
            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(loanRequest, null, null);
            Validator.TryValidateObject(loanRequest, context, validationResults, true);
            return validationResults;
        }
    }
}
