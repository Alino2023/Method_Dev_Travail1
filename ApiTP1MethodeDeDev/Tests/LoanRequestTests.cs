using ApiTP1MethodeDeDev.Dtos.Loan;
using Domain.Bank;
using Domain.Borrowers;
using Domain.Emploi;
using Domain.LatePayment;
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
                TheBorrower = new Borrower
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
                    DebtRatio = 0.2m,
                    OtherBankLoans = new List<OtherBankLoan>(), // Pas de prêts externes
                    EmploymentHistory = new List<Job>
        {
            new Job
            {
                //Title = "Software Engineer",
                InstitutionName = "Tech Corp",
                StartingDate = DateTime.Now.AddYears(-3),
                EndingDate = DateTime.Now,
                MentualSalary = 5000
            }
        }
                }
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
                TheBorrower = new Borrower
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
                    DebtRatio = 0.2m,
                    OtherBankLoans = new List<OtherBankLoan>(), // Pas de prêts externes
                    EmploymentHistory = new List<Job>
        {
            new Job
            {
                //Title = "Software Engineer",
                InstitutionName = "Tech Corp",
                StartingDate = DateTime.Now.AddYears(-3),
                EndingDate = DateTime.Now,
                MentualSalary = 5000
            }
        }
                }
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
                TheBorrower = new Borrower
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
                    DebtRatio = 0.2m,
                    OtherBankLoans = new List<OtherBankLoan>(), // Pas de prêts externes
                    EmploymentHistory = new List<Job>
        {
            new Job
            {
                //Title = "Software Engineer",
                InstitutionName = "Tech Corp",
                StartingDate = DateTime.Now.AddYears(-3),
                EndingDate = DateTime.Now,
                MentualSalary = 5000
            }
        }
                }
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
                TheBorrower = new Borrower
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
                    DebtRatio = 0.2m,
                    OtherBankLoans = new List<OtherBankLoan>(), // Pas de prêts externes
                    EmploymentHistory = new List<Job>
        {
            new Job
            {
                //Title = "Software Engineer",
                InstitutionName = "Tech Corp",
                StartingDate = DateTime.Now.AddYears(-3),
                EndingDate = DateTime.Now,
                MentualSalary = 5000
            }
        }
                }
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
                TheBorrower = new Borrower
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
                    DebtRatio = 0.2m,
                    OtherBankLoans = new List<OtherBankLoan>(), // Pas de prêts externes
                    EmploymentHistory = new List<Job>
        {
            new Job
            {
                //Title = "Software Engineer",
                InstitutionName = "Tech Corp",
                StartingDate = DateTime.Now.AddYears(-3),
                EndingDate = DateTime.Now,
                MentualSalary = 5000
            }
        }
                }
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
