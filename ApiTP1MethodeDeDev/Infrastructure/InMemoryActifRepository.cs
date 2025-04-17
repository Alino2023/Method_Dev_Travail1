using Domain.Actifs;
using Domain.Borrowers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class inMemoryActifRepository : IActifRepository
    {
        private readonly AppDbContext _appDbContext;

        public inMemoryActifRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public int Add(Actif actif)
        {
            if (actif == null)
            {
                throw new ArgumentNullException(nameof(actif));
            }
            var actifEntity = new ActifEntity
            {
                Id = actif.Id,
                Valeur=actif.Valeur,
                Description = actif.Description,
                BorroweId = actif.BorroweId

            };

            _appDbContext.LesActifs.Add(actifEntity);
            _appDbContext.SaveChanges();

            return actifEntity.Id;
        }

        public Actif GetById(int id)
        {
            ActifEntity actifEntity = _appDbContext.LesActifs .First(b => b.Id == id);

            if (actifEntity == null)
            {
                throw new KeyNotFoundException($"actif with Id {id} not found.");
            }

            return new Actif(actifEntity.Id, actifEntity.Valeur,actifEntity.Description, actifEntity.BorroweId);
        }
    }   
}
