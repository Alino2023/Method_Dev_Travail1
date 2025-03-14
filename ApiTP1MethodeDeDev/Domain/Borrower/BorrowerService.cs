using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Borrower
{
    public class BorrowerService : IBorrowerService
    {

        Task<IList<Borrower>> IBorrowerService.GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
