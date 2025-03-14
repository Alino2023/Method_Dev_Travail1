using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Borrower
{
    public interface IBorrowerRepository
    {
        Task<Borrower> GetBySin(string sin);
        Task<int> Add(Borrower borrower);
        void Update(Borrower borrower);
        Task<List<Borrower>> GetAll();

    }
}
