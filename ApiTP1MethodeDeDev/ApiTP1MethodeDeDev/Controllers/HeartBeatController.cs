using Domain.Borrower;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiTP1MethodeDeDev.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeartBeatController : ControllerBase
    {
        private readonly AppDbContext _context;

        public HeartBeatController(AppDbContext context)
        {
            _context = context;
        }
        // GET: api/<HeartBeatController>
        [HttpGet]
        public IActionResult GetApiHeartbeat()
        {
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BorrowerEntity>>> GetAllBorrowers()
        {
            return await _context.Borrowers
                .Select(x => BorrowerToEntity(x))
                .ToListAsync();
        }



        [HttpGet("{sin}")]
        public async Task<ActionResult<Borrower>> GetBorrower(string sin)
        {
            var borrower = await _context.Borrowers.FindAsync(sin);

            if (borrower == null)
            {
                return NotFound();
            }

            return borrower;
        }

        [HttpPut("{sin}")]
        public async Task<IActionResult> UpdateBorrower(string sin, Borrower borrower)
        {
            if (sin != borrower.Sin)
            {
                return BadRequest();
            }

            _context.Entry(borrower).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BorrowerExists(sin))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        private bool BorrowerExists(string sin)
        {
            return _context.Borrowers.Any(b => b.Sin == sin);
        }

        private static BorrowerEntity BorrowerToEntity(Borrower borrower) =>
           new BorrowerEntity
           {
               Sin = borrower.Sin,
               FirstName = borrower.FirstName,
               LastName = borrower.LastName,
               Phone = borrower.Phone,
               Email = borrower.Email,
               Address = borrower.Address,
           };


    }
}
