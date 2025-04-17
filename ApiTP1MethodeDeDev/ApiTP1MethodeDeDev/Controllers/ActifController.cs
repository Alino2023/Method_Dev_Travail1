using ApiTP1MethodeDeDev.Dtos;
using Domain.Actifs;
using Microsoft.AspNetCore.Mvc;

namespace ApiTP1MethodeDeDev.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ActifController : ControllerBase
    {
        private readonly IActifService _actifService;

        public ActifController(IActifService actifService)
        {
            _actifService = actifService;
        }

        [HttpGet("{id}")]
        public ActionResult<Actif> Get(int id)
        {
            var actif = _actifService.GetById(id);
            if (actif == null)
                return NotFound();

            return actif;
        }

        [HttpPost]
        public ActionResult<int> Post( ActifRequest actifRequest)
        {
            if (actifRequest == null)
                return BadRequest("Invalid borrower data");

            Actif actif = new Actif
            (
                 actifRequest.id,
                 actifRequest.Valeur,
                 actifRequest.Description,
                 actifRequest.BorroweId
            );

            int ActifId = _actifService.Add(actif);
            return CreatedAtAction(nameof(Get), new { Id = ActifId }, ActifId);
        }
    }
}
