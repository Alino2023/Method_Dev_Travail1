using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Borrower
{
    public interface IBorrowerService
    {
        public Task<IList<Borrower>> GetAll();

    }
}
