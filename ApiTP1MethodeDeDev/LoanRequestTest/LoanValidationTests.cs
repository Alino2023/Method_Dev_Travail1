using ApiTP1MethodeDeDev.Dtos.Loan;
using Domain.Borrowers;
using Domain.Loans;
using System.ComponentModel.DataAnnotations;

namespace ApiTP1MethodeDeDev.Tests
{
    public class LoanRequestValidationTests
    {
        private Borrower _validBorrower;

        [TestInitialize]
        public void Setup()
        {
            _validBorrower = new Borrower
            {
                Sin = "123456789",
                FirstName = "John",
                LastName = "Doe",
                Phone = "1234567890",
                Email = "johndoe@example.com",
                Address = "123 Main St",
                Equifax_Result = 700,
                MonthlyIncome = 5000,
                DebtRatio = 0.2m
            };
        }

        [TestMethod]
        public void Given_LoanWithoutBorrower_When_Validated_Then_ShouldBeInvalid()
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
                TheBorrower = null // Emprunteur manquant
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(loanRequest);
            bool isValid = Validator.TryValidateObject(loanRequest, validationContext, validationResults, true);

            // Assert
            Assert.IsFalse(isValid, "Un prêt sans emprunteur devrait être invalide.");
            Assert.IsTrue(validationResults.Exists(r => r.ErrorMessage.Contains("Borrower is required")),
                          "Le message d'erreur attendu pour un emprunteur manquant est absent.");
        }

        [TestMethod]
        public void Given_LoanWithZeroDuration_When_Validated_Then_ShouldBeInvalid()
        {
            // Arrange
            var loanRequest = new LoanRequest
            {
                Amount = 1000,
                InterestRate = 5,
                DurationInMonths = 0, // Durée invalide
                Status = StatusLoan.InProgress,
                StartDate = DateTime.Now,
                RemainingAmount = 1000,
                TheBorrower = _validBorrower
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(loanRequest);
            bool isValid = Validator.TryValidateObject(loanRequest, validationContext, validationResults, true);

            // Assert
            Assert.IsFalse(isValid, "Un prêt avec une durée de 0 mois devrait être invalide.");
            Assert.IsTrue(validationResults.Exists(r => r.ErrorMessage.Contains("Duration must be greater than zero")),
                          "Le message d'erreur attendu pour une durée de 0 mois est absent.");
        }

        [TestMethod]
        public void Given_LoanWithStartDateInPast_When_Validated_Then_ShouldBeInvalid()
        {
            // Arrange
            var loanRequest = new LoanRequest
            {
                Amount = 1000,
                InterestRate = 5,
                DurationInMonths = 12,
                Status = StatusLoan.InProgress,
                StartDate = DateTime.Now.AddDays(-10), // Date invalide dans le passé
                RemainingAmount = 1000,
                TheBorrower = _validBorrower
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(loanRequest);
            bool isValid = Validator.TryValidateObject(loanRequest, validationContext, validationResults, true);

            // Assert
            Assert.IsFalse(isValid, "Un prêt avec une date de début dans le passé devrait être invalide.");
            Assert.IsTrue(validationResults.Exists(r => r.ErrorMessage.Contains("Start date cannot be in the past")),
                          "Le message d'erreur attendu pour une date de début incorrecte est absent.");
        }

        [TestMethod]
        public void Given_LoanWithRemainingAmountGreaterThanAmount_When_Validated_Then_ShouldBeInvalid()
        {
            // Arrange
            var loanRequest = new LoanRequest
            {
                Amount = 1000,
                InterestRate = 5,
                DurationInMonths = 12,
                Status = StatusLoan.InProgress,
                StartDate = DateTime.Now,
                RemainingAmount = 1200, // Montant restant invalide
                TheBorrower = _validBorrower
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(loanRequest);
            bool isValid = Validator.TryValidateObject(loanRequest, validationContext, validationResults, true);

            // Assert
            Assert.IsFalse(isValid, "Le montant restant ne devrait pas être supérieur au montant total du prêt.");
            Assert.IsTrue(validationResults.Exists(r => r.ErrorMessage.Contains("Remaining amount cannot exceed total amount")),
                          "Le message d'erreur attendu pour un montant restant incorrect est absent.");
        }

    }
}
