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

    }
}
