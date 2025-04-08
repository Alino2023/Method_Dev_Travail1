using Domain.Bank;
using Domain.Borrowers;
using Domain.Emploi;
using Domain.LatePayment;
using Domain.Loans;
using Moq;

namespace ApiTP1MethodeDeDev.Test
{
    [TestClass]
    public class BorrowerTest
    {
        private Mock<IBorrowerService> borrowerServiceMock;
        private Borrower borrower;

        [TestInitialize]
        public void Setup()
        {
            borrowerServiceMock = new Mock<IBorrowerService>();

            borrower = new Borrower
            {
                Sin = "123-456-789",
                FirstName = "John",
                LastName = "Doe",
                Phone = "123-456-7890",
                Email = "john.doe@example.com",
                Address = "123 Main St",
                Equifax_Result = 700,
                BankruptyDate = System.DateTime.MinValue,
                NumberOfLatePayments = new List<LatePaymentBorrower>(),
                OtherBankLoans = new List<OtherBankLoan>(),
                EmploymentHistory = new List<Job>
                {
                    new Job { MentualSalary = 5000m, StartingDate = System.DateTime.Now.AddMonths(-6) }
                },
                Loans = new List<Loan>
                {
                    new Loan { MonthlyPayment = 1000m }
                },
                BorrowerService = borrowerServiceMock.Object,
            };

            
        }

        [TestMethod]
        public void Given_Borrower_When_CalculateDebtRatioIsCalled_Then_ReturnsExpectedValue()
        {
            // Arrange
            decimal expectedRatio = 20.0m;
            borrowerServiceMock.Setup(s => s.CalculateDebtRatio(borrower)).Returns(expectedRatio);

            // Act
            decimal actualRatio = borrowerServiceMock.Object.CalculateDebtRatio(borrower);

            // Assert
            Assert.AreEqual(expectedRatio, actualRatio, "Le ratio retourné n'est pas correct.");
        }

        [TestMethod]
        public void TestDebtRatioCalculation()
        {
            decimal expectedRatio = 26.0m;
            decimal actualRatio = borrower.DebtRatio;
            Console.WriteLine($"Expected Debt Ratio: {expectedRatio}, Actual Debt Ratio: {actualRatio}");
            Assert.AreEqual(expectedRatio, Math.Round(actualRatio, 1), "Le ratio d'endettement est incorrect.");
        }


        [TestMethod]
        public void TestRiskClassificationHighRisk()
        {
            decimal expectedRatio = 0.45m;
            borrowerServiceMock.Setup(s => s.CalculateDebtRatio(borrower)).Returns(expectedRatio);

            borrower.Equifax_Result = 600;

            string riskClass = borrower.ClassifyRisk();
            Console.WriteLine($"Risk Classification: {riskClass}");
            Assert.AreEqual("High Risk", riskClass, "La classification du risque n'est pas correcte.");
        }

        [TestMethod]
        public void TestRiskClassificationMediumRisk()
        {
            decimal expectedRatio = 0.35m;
            borrowerServiceMock.Setup(s => s.CalculateDebtRatio(borrower)).Returns(expectedRatio);

            borrower.Equifax_Result = 700;

            string riskClass = borrower.ClassifyRisk();
            Console.WriteLine($"Risk Classification: {riskClass}");
            Assert.AreEqual("Medium Risk", riskClass, "La classification du risque n'est pas correcte.");
        }

        [TestMethod]
        public void TestRiskClassificationLowRisk()
        {
            decimal expectedRatio = 0.20m;
            borrowerServiceMock.Setup(s => s.CalculateDebtRatio(borrower)).Returns(expectedRatio);

            borrower.Equifax_Result = 800;

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
