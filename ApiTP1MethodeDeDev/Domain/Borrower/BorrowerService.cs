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

        public Borrower GetBySin(string sin)
        {
            return _borrowerRepository.GetBySin(sin);
        }

        IList<Borrower> IBorrowerService.GetAll()
        {
            return _borrowerRepository.GetAll();
        }
    }
}
