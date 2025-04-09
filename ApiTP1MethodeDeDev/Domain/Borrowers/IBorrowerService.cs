using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Borrowers
{
    public interface IBorrowerService
    {
        public IList<Borrower> GetAll();
        Borrower GetBySin(string sin);
        string Add(Borrower borrower);
        void Update(Borrower borrower);

<<<<<<< HEAD
        //decimal CalculateDebtRatio(Borrower borrower);
=======
>>>>>>> parent of f589cb5 (delete of the existing code to searsh for the one in develop)
    }
}
