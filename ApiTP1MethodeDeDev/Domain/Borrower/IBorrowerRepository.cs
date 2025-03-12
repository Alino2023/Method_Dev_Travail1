using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Borrower
{
    public interface IBorrowerRepository
    {
        Borrower GetBySin(string sin);
        int Add(Borrower borrower);
        void Update(Borrower borrower);

    }
}
