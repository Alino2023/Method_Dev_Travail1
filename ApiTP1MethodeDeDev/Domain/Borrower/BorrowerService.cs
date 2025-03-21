using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Borrower
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
    }
}
