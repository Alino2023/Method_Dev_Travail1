using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Actifs
{
    public interface IActifService
    {
        int Add(Actif actif);
        Actif GetById(int id);


    }
}
