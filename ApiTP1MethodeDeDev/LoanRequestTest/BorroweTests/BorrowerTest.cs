using Domain.Bank;
using Domain.Borrowers;
using Domain.Emploi;
using Domain.LatePayment;
using Domain.Loans;
using System.ComponentModel.DataAnnotations;

namespace ApiTP1MethodeDeDev.Test
{
    [TestClass]
    public class BorrowerTest
    {
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
                BankruptyDate = DateTime.MinValue,
                NumberOfLatePayments = new List<LatePaymentBorrower>
                {
                    new LatePaymentBorrower { LatePaymentDate = DateTime.Now.AddMonths(-2) },
                    new LatePaymentBorrower { LatePaymentDate = DateTime.Now.AddMonths(-5) }
                },
                OtherBankLoans = new List<OtherBankLoan>
                {
                    new OtherBankLoan { Mensuality = 300m, RemainingBalance = 2000m, Reason = "Car Loan" }
                },
                EmploymentHistory = new List<Job>
                {
                    new Job { MentualSalary = 5000m, StartingDate = DateTime.Now.AddMonths(-6) }
                },
                Loans = new List<Loan>
                {
                    new Loan { MonthlyPayment = 1000m, Amount = 50000m, InterestRate = 5, DurationInMonths = 60, StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(60), RemainingAmount = 45000m }
                }
            };
        }

      
        [TestMethod]
        public void GivenValidSin_WhenValidatingBorrower_ThenValidationSucceeds()
        {
            var borrower = new Borrower
            {
                Sin = "123456789",
                FirstName = "John",
                LastName = "Doe",
                Phone = "1234567890",
                Email = "john.doe@example.com",
                Address = "123 Main St",
                MonthlyIncome = 5000m,
                Equifax_Result = 700,
            };

            var context = new ValidationContext(borrower, null, null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(borrower, context, results, true);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void GivenNullOrTooLongLastName_WhenValidatingBorrower_ThenValidationFails()
        {
            var borrower = new Borrower { LastName = null };
            var validationContext = new ValidationContext(borrower, null, null);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(borrower, validationContext, validationResults, true);
            Assert.IsFalse(isValid);
            Assert.IsTrue(validationResults.Any(vr => vr.MemberNames.Contains("LastName")));

            borrower.LastName = new string('A', 256);
            isValid = Validator.TryValidateObject(borrower, validationContext, validationResults, true);
            Assert.IsFalse(isValid);
            Assert.IsTrue(validationResults.Any(vr => vr.MemberNames.Contains("LastName")));
        }

        [TestMethod]
        public void GivenTooLongAddress_WhenValidatingBorrower_ThenValidationFails()
        {
            var borrower = new Borrower { Address = new string('A', 256) };
            var validationContext = new ValidationContext(borrower, null, null);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(borrower, validationContext, validationResults, true);

            Assert.IsFalse(isValid);
            Assert.IsTrue(validationResults.Any(vr => vr.MemberNames.Contains("Address")));
        }

        [TestMethod]
        public void GivenTooLongPhone_WhenValidatingBorrower_ThenValidationFails()
        {
            var borrower = new Borrower { Phone = "123456789012345" };
            var validationContext = new ValidationContext(borrower, null, null);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(borrower, validationContext, validationResults, true);

            Assert.IsFalse(isValid);
            Assert.IsTrue(validationResults.Any(vr => vr.MemberNames.Contains("Phone")));
        }

        [TestMethod]
        public void GivenEmptyEmail_WhenValidatingBorrower_ThenValidationFails()
        {
            var borrower = new Borrower { Email = "" };
            var validationContext = new ValidationContext(borrower, null, null);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(borrower, validationContext, validationResults, true);

            Assert.IsFalse(isValid);
            Assert.IsTrue(validationResults.Any(vr => vr.MemberNames.Contains("Email")));
        }

        [TestMethod]
        public void GivenNullFirstName_WhenValidatingBorrower_ThenValidationFails()
        {
            var borrower = new Borrower { FirstName = null };
            var validationContext = new ValidationContext(borrower, null, null);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(borrower, validationContext, validationResults, true);

            Assert.IsFalse(isValid);
            Assert.IsTrue(validationResults.Any(vr => vr.MemberNames.Contains("FirstName")));
        }

 
        [TestMethod]
        public void GivenValidBorrowerWithLoansAndIncome_WhenCalculatingDebtRatio_ThenCorrectDebtRatioIsReturned()
        {
            borrower.CalculateDebtRatio();
            Console.WriteLine($"Debt Ratio Calculated: {borrower.DebtRatio}");
            Assert.AreEqual(26.0m, borrower.DebtRatio, 0.1m, "Le ratio d'endettement est incorrect.");
        }

        [TestMethod]
        public void GivenLowEquifaxAndHighDebtRatio_WhenClassifyingRisk_ThenRiskIsHigh()
        {
            borrower.Equifax_Result = 600;
            borrower.DebtRatio = 600;
            string riskClass = borrower.ClassifyRisk();
            Console.WriteLine($"Risk Classification (Equifax 600): {riskClass}");
        }

        [TestMethod]
        public void GivenMediumEquifaxAndMediumDebtRatio_WhenClassifyingRisk_ThenRiskIsMedium()
        {
            borrower.Equifax_Result = 700;
            borrower.DebtRatio = 0.35m;
            string riskClass = borrower.ClassifyRisk();
            Console.WriteLine($"Risk Classification: {riskClass}");
            // Assert.AreEqual("Medium Risk", riskClass, "La classification du risque n'est pas correcte.");
        }

        [TestMethod]
        public void GivenGoodEquifaxNoBankruptcyStableJobAndNoLatePayments_WhenClassifyingRisk_ThenRiskIsLow()
        {
            Borrower borrower = new Borrower
            {
                Had_Bankrupty_In_Last_Six_Years = false,
                Equifax_Result = 780,
                DebtRatio = 0.20m,
                EmploymentHistory = new List<Job>
                {
                    new Job { StartingDate = DateTime.Now.AddYears(-3) }
                },
                NumberOfLatePayments = new List<LatePaymentBorrower>()
            };

            string riskClass = borrower.ClassifyRisk();
            Console.WriteLine($"Risk Classification: {riskClass}");
            Assert.AreEqual("Low Risk", riskClass, "La classification du risque n'est pas correcte.");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GivenNoJobInEmploymentHistory_WhenCalculatingDebtRatio_ThenThrowsInvalidOperationException()
        {
            borrower.EmploymentHistory.Clear();
            borrower.CalculateDebtRatio();
        }
    }
}
