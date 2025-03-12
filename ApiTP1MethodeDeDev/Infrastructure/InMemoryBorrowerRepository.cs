using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Borrower;

namespace Infrastructure
{
    public class InMemoryBorrowerRepository : IBorrowerRepository
    {
        public int Add(Borrower borrower)
        {
            throw new NotImplementedException();
        }

        public List<Borrower> GetAll()
        {
            throw new NotImplementedException();
        }

        public Borrower GetBySin(string sin)
        {
            throw new NotImplementedException();
        }

        public void Update(Borrower borrower)
        {
            throw new NotImplementedException();
        }
    }
}
