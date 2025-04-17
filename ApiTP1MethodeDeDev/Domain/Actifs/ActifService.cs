using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Actifs
{
    public class ActifService : IActifRepository
    {
        private readonly IActifRepository _repository;

        public ActifService(IActifRepository actifRepository)
        {
            _repository = actifRepository;
        }

        public int Add(Actif actif)
        {
            return _repository.Add(actif);
        }

        public Actif GetById(int id)
        {
            return _repository.GetById(id);
        }
    }
}
