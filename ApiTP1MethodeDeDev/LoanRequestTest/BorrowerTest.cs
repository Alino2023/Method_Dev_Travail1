using Domain.Bank;
using Domain.Borrowers;
using Domain.Emploi;
using Domain.LatePayment;
using Domain.Loans;

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
        public void TestDebtRatioCalculation()
        {
            borrower.CalculateDebtRatio();
            Console.WriteLine($"Debt Ratio Calculated: {borrower.DebtRatio}");
            Assert.AreEqual(26.0m, borrower.DebtRatio, 0.1m, "Le ratio d'endettement est incorrect.");
        }

        [TestMethod]
        public void TestRiskClassificationHighRisk()
        {
            borrower.Equifax_Result = 600;
            borrower.DebtRatio = 0.45m;
            string riskClass = borrower.ClassifyRisk();
            Console.WriteLine($"Risk Classification: {riskClass}");
            Assert.AreEqual("High Risk", riskClass, "La classification du risque n'est pas correcte.");
        }

        [TestMethod]
        public void TestRiskClassificationMediumRisk()
        {
            borrower.Equifax_Result = 700;
            borrower.DebtRatio = 0.35m;
            string riskClass = borrower.ClassifyRisk();
            Console.WriteLine($"Risk Classification: {riskClass}");
            Assert.AreEqual("Medium Risk", riskClass, "La classification du risque n'est pas correcte.");
        }

        [TestMethod]
        public void TestRiskClassificationLowRisk()
        {
            borrower.Equifax_Result = 800;
            borrower.DebtRatio = 0.20m;
            string riskClass = borrower.ClassifyRisk();
            Console.WriteLine($"Risk Classification: {riskClass}");
            Assert.AreEqual("Low Risk", riskClass, "La classification du risque n'est pas correcte.");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestDebtRatioCalculationWithNoJob()
        {
            borrower.EmploymentHistory.Clear();
            borrower.CalculateDebtRatio();
        }
    }
}
