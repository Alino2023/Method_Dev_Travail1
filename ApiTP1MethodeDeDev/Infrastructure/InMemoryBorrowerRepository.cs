using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Borrower;
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

        //public int Add(Borrower borrower)
        //{
        //    throw new NotImplementedException();
        //}

        //public List<Borrower> GetAll()
        //{
        //    return _appDbContext.Borrowers.Select(b => new Borrower(b.Sin, b.FirstName, b.LastName, b.Phone, b.Email, b.Address)).ToList();

        //}

        //public Borrower GetBySin(string sin)
        //{
        //    throw new NotImplementedException();
        //}

        public void Update(Borrower borrower)
        {
            throw new NotImplementedException();
        }

        Task<int> IBorrowerRepository.Add(Borrower borrower)
        {
            throw new NotImplementedException();
        }

        Task<List<Borrower>> IBorrowerRepository.GetAll()
        {
            return _appDbContext.Borrowers.Select(b => BorrowerToEntity(b))
                .ToListAsync();
            throw new NotImplementedException();
        }

        Task<Borrower> IBorrowerRepository.GetBySin(string sin)
        {
            throw new NotImplementedException();
        }

        private static BorrowerEntity BorrowerToEntity(Borrower borrower) =>
           new BorrowerEntity
           {
               Sin = borrower.Sin,
               FirstName = borrower.FirstName,
               LastName = borrower.LastName,
               Phone = borrower.Phone,
               Email = borrower.Email,
               Address = borrower.Address
           };
    }
}
