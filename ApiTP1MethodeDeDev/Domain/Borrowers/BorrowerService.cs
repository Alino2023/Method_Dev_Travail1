using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Emploi;

namespace Domain.Borrowers
{
    public class BorrowerService : IBorrowerService
    {
        private readonly IBorrowerRepository _borrowerRepository;

        public BorrowerService(IBorrowerRepository borrowerRepository)
        {
            _borrowerRepository = borrowerRepository;
        }

        public string Add(Borrower borrower)
        {
            return _borrowerRepository.Add(borrower);
        }

        public Borrower GetBySin(string sin)
        {
            return _borrowerRepository.GetBySin(sin);
        }

        public void Update(Borrower borrower)
        {
            _borrowerRepository.Update(borrower);
        }

        IList<Borrower> IBorrowerService.GetAll()
        {
            return _borrowerRepository.GetAll();
        }

        //public decimal CalculateDebtRatio(Borrower borrower)
        //{
        //    Job jobActuel = borrower.EmploymentHistory.OrderByDescending(job => job.StartingDate).FirstOrDefault();

        //    if (jobActuel == null)
        //    {
        //        throw new InvalidOperationException("No employment found in the history to calculate the debt ratio.");
        //    }

        //    if (jobActuel.MentualSalary <= 0)
        //    {
        //        throw new InvalidOperationException("The current job's salary must be greater than zero to calculate the debt ratio.");
        //    }

        //    decimal totalLoanPayments = borrower.OtherBankLoans.Sum(loan => loan.Mensuality) + borrower.Loans.Sum(loan => loan.MonthlyPayment);

        //    return ((totalLoanPayments / jobActuel.MentualSalary) * 100);
        //}

    }
}
