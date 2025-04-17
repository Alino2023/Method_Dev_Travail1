using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Borrowers;

namespace Domain.Actifs
{
    public interface IActifRepository
    {
        int Add(Actif actif);
        Actif GetById(int id);
    }
}
