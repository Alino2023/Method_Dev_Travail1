using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Borrower
{
    public interface IBorrowerService
    {
        public IList<Borrower> GetAll();
        Borrower GetBySin(string sin);
        string Add(Borrower borrower);
        void Update(Borrower borrower);

    }
}
