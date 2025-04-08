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

        [TestInitialize]
        public void Setup()
        {
            _loanRepositoryMock = new Mock<ILoanRepository>();

            // Création d'un emprunteur valide
            _validBorrower = new Borrower
            {
                Sin = "123456789",
                FirstName = "John",
                LastName = "Doe",
                Phone = "1234567890",
                Email = "johndoe@example.com",
                Address = "123 Main St",
                BankruptyDate = DateTime.Now.AddYears(-7), // Pas de faillite récente
                Equifax_Result = 700,
                NumberOfLatePayments = new List<LatePaymentBorrower>(), // Aucun retard de paiement
                MonthlyIncome = 5000,
                OtherBankLoans = new List<OtherBankLoan>(), // Pas de prêts externes
                EmploymentHistory = new List<Job>
        {
            new Job
            {
                InstitutionName = "Tech Corp",
                StartingDate = DateTime.Now.AddYears(-3),
                EndingDate = DateTime.Now,
                MentualSalary = 5000
            }
        }
            };

            // Le DebtRatio sera automatiquement calculé ici
            Console.WriteLine($"Debt Ratio: {_validBorrower.DebtRatio}"); // Affiche le ratio d'endettement
        }


        [TestMethod]
        public void Given_ValidLoanRequest_When_Validated_Then_ShouldBeValid()
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
                TheBorrower = _validBorrower
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(loanRequest);
            bool isValid = Validator.TryValidateObject(loanRequest, validationContext, validationResults, true);

            // Assert
            Assert.IsTrue(isValid, "La demande de prêt valide devrait être considérée comme valide.");
        }

        [TestMethod]
        public void Given_LoanRequestWithNegativeAmount_When_Validated_Then_ShouldBeInvalid()
        {
            // Arrange
            var loanRequest = new LoanRequest
            {
                Amount = -500, // Montant invalide
                InterestRate = 5,
                DurationInMonths = 12,
                Status = StatusLoan.InProgress,
                StartDate = DateTime.Now,
                RemainingAmount = -500,
                TheBorrower = _validBorrower
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(loanRequest);
            bool isValid = Validator.TryValidateObject(loanRequest, validationContext, validationResults, true);

            // Assert
            Assert.IsFalse(isValid, "La validation devrait échouer pour un montant négatif.");
            Assert.IsTrue(validationResults.Exists(r => r.ErrorMessage.Contains("greater than zero")),
                          "Le message d'erreur attendu pour un montant négatif est absent.");
        }

        [TestMethod]
        public void Given_LoanRequestWithInvalidInterestRate_When_Validated_Then_ShouldBeInvalid()
        {
            // Arrange
            var loanRequest = new LoanRequest
            {
                Amount = 1000,
                InterestRate = 150, // Taux d'intérêt invalide
                DurationInMonths = 12,
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
            Assert.IsFalse(isValid, "La validation devrait échouer pour un taux d'intérêt supérieur à 100%.");
            Assert.IsTrue(validationResults.Exists(r => r.ErrorMessage.Contains("between 0 and 100%")),
                          "Le message d'erreur attendu pour un taux d'intérêt incorrect est absent.");
        }
    }
}
