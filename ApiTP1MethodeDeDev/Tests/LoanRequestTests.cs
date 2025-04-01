using ApiTP1MethodeDeDev.Dtos.Loan;
using Domain.Borrowers;
using Domain.Loans;
using FluentAssertions;
using System.ComponentModel.DataAnnotations;
using Xunit;


namespace ApiTP1MethodeDeDev.Tests
{
    public class LoanRequestTests
    {
        [Fact]
        public void LoanRequest_ShouldBeValid_WhenValidData()
        {
            // Arrange
            var loanRequest = new LoanRequest
            {
                Amount = 1000,
                InterestRate = 5,
                DurationInMonths = 12,
                Status = StatusLoan.InProgress,
                StartDate = DateTime.Now,
                RemainingAmount = 1000,
                TheBorrower = new Borrower { /* Remplir les informations nécessaires */ }
            };

            // Act
            var validationResults = new System.ComponentModel.DataAnnotations.ValidationContext(loanRequest);
            var validationResult = new System.Collections.Generic.List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(loanRequest, validationResults, validationResult, true);

            // Assert
            isValid.Should().BeTrue();
        }

        [Fact]
        public void LoanRequest_ShouldInvalidate_WhenAmountIsZeroOrNegative()
        {
            // Arrange
            var loanRequest = new LoanRequest
            {
                Amount = 0,
                InterestRate = 5,
                DurationInMonths = 12,
                Status = StatusLoan.InProgress,
                StartDate = DateTime.Now,
                RemainingAmount = 0,
                TheBorrower = new Borrower { /* Remplir les informations nécessaires */ }
            };

            // Act
            var validationResults = new System.ComponentModel.DataAnnotations.ValidationContext(loanRequest);
            var validationResult = new System.Collections.Generic.List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(loanRequest, validationResults, validationResult, true);

            // Assert
            isValid.Should().BeFalse();
            validationResult.Should().Contain(r => r.ErrorMessage == "Amount must be greater than zero.");
        }

        [Fact]
        public void LoanRequest_ShouldInvalidate_WhenInterestRateOutOfRange()
        {
            // Arrange
            var loanRequest = new LoanRequest
            {
                Amount = 1000,
                InterestRate = 200, // Interest rate out of range
                DurationInMonths = 12,
                Status = StatusLoan.InProgress,
                StartDate = DateTime.Now,
                RemainingAmount = 1000,
                TheBorrower = new Borrower { /* Remplir les informations nécessaires */ }
            };

            // Act
            var validationResults = new System.ComponentModel.DataAnnotations.ValidationContext(loanRequest);
            var validationResult = new System.Collections.Generic.List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(loanRequest, validationResults, validationResult, true);

            // Assert
            isValid.Should().BeFalse();
            validationResult.Should().Contain(r => r.ErrorMessage == "Interest rate must be between 0 and 100%.");
        }

        [Fact]
        public void LoanRequest_ShouldInvalidate_WhenDurationIsLessThanOne()
        {
            // Arrange
            var loanRequest = new LoanRequest
            {
                Amount = 1000,
                InterestRate = 5,
                DurationInMonths = 0, // Invalid duration
                Status = StatusLoan.InProgress,
                StartDate = DateTime.Now,
                RemainingAmount = 1000,
                TheBorrower = new Borrower { /* Remplir les informations nécessaires */ }
            };

            // Act
            var validationResults = new System.ComponentModel.DataAnnotations.ValidationContext(loanRequest);
            var validationResult = new System.Collections.Generic.List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(loanRequest, validationResults, validationResult, true);

            // Assert
            isValid.Should().BeFalse();
            validationResult.Should().Contain(r => r.ErrorMessage == "Duration must be at least 1 month.");
        }

        [Fact]
        public void LoanRequest_ShouldInvalidate_WhenBorrowerIsNull()
        {
            // Arrange
            var loanRequest = new LoanRequest
            {
                Amount = 1000,
                InterestRate = 5,
                DurationInMonths = 12,
                Status = StatusLoan.InProgress,
                StartDate = DateTime.Now,
                RemainingAmount = 1000,
                TheBorrower = null // Borrower is null
            };

            // Act
            var validationResults = new System.ComponentModel.DataAnnotations.ValidationContext(loanRequest);
            var validationResult = new System.Collections.Generic.List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(loanRequest, validationResults, validationResult, true);

            // Assert
            isValid.Should().BeFalse();
            validationResult.Should().Contain(r => r.ErrorMessage == "A borrower must be assigned to the loan.");
        }
    }
}
