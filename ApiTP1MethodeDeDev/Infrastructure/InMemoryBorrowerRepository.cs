using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Borrowers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class InMemoryBorrowerRepository : IBorrowerRepository
    {
        private readonly AppDbContext _appDbContext;

        public InMemoryBorrowerRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public string Add(Borrower borrower)
        {
            if (borrower == null)
                throw new ArgumentNullException(nameof(borrower));

            var borrowerEntity = new BorrowerEntity
            {
                Sin = borrower.Sin,
                FirstName = borrower.FirstName,
                LastName = borrower.LastName,
                Phone = borrower.Phone,
                Email = borrower.Email,
                Address = borrower.Address,
                Equifax_Result = borrower.Equifax_Result,
                BankruptyDate = borrower.BankruptyDate,
                OtherBankLoans = borrower.OtherBankLoans,
                NumberOfLatePayments = borrower.NumberOfLatePayments,
                EmploymentHistory = borrower.EmploymentHistory
            };

            _appDbContext.Borrowers.Add(borrowerEntity);
            _appDbContext.SaveChanges();

            return borrowerEntity.Sin;
        }

        public List<Borrower> GetAll()
        {
            return _appDbContext.Borrowers.Select(b => new Borrower(b.Sin, b.FirstName, b.LastName, b.Phone, b.Email, b.Address,  b.Equifax_Result, b.BankruptyDate, b.OtherBankLoans, b.NumberOfLatePayments, b.EmploymentHistory)).ToList();

        }

        public Borrower GetBySin(string sin)
        {
            BorrowerEntity borrowerEntity = _appDbContext.Borrowers
                                            .Include(b => b.OtherBankLoans)
                                            .Include(b => b.NumberOfLatePayments)
                                            .Include(b => b.EmploymentHistory)
                                            .First(b => b.Sin == sin);

            if (borrowerEntity == null)
            {
                throw new KeyNotFoundException($"Borrower with SIN {sin} not found.");
            }

            return new Borrower(borrowerEntity.Sin, borrowerEntity.FirstName, borrowerEntity.LastName, borrowerEntity.Phone, borrowerEntity.Email, borrowerEntity.Address, borrowerEntity.Equifax_Result, borrowerEntity.BankruptyDate, borrowerEntity.OtherBankLoans, borrowerEntity.NumberOfLatePayments, borrowerEntity.EmploymentHistory);
        }

        public void Update(Borrower borrower)
        {
            if (borrower == null)
                throw new ArgumentNullException(nameof(borrower));

            var borrowerEntity = _appDbContext.Borrowers
                .Include(b => b.OtherBankLoans)
                .Include(b => b.NumberOfLatePayments)
                .Include(b => b.EmploymentHistory)
                .FirstOrDefault(b => b.Sin == borrower.Sin);

            if (borrowerEntity == null)
                throw new KeyNotFoundException($"Borrower with SIN {borrower.Sin} not found.");

            borrowerEntity.FirstName = borrower.FirstName;
            borrowerEntity.LastName = borrower.LastName;
            borrowerEntity.Phone = borrower.Phone;
            borrowerEntity.Email = borrower.Email;
            borrowerEntity.Address = borrower.Address;
            borrowerEntity.Equifax_Result = borrower.Equifax_Result;
            borrowerEntity.BankruptyDate = borrower.BankruptyDate;

            borrowerEntity.OtherBankLoans = borrower.OtherBankLoans;

            borrowerEntity.NumberOfLatePayments = borrower.NumberOfLatePayments;

            borrowerEntity.EmploymentHistory = borrower.EmploymentHistory;

            _appDbContext.SaveChanges();
        }




        //private static Borrower BorrowerToEntity(Borrower borrower) =>
        //          new Borrower
        //          {
        //              Sin = borrower.Sin,
        //              FirstName = borrower.FirstName,
        //              LastName = borrower.LastName,
        //              Phone = borrower.Phone,
        //              Email = borrower.Email,
        //              Address = borrower.Address
        //          };

    }
}
