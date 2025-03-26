using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Borrowers
{
    public interface IBorrowerRepository
    {
        Borrower GetBySin(string sin);
        string Add(Borrower borrower);
        void Update(Borrower borrower);
        List<Borrower> GetAll();
    }
}
