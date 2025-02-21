using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiTP1MethodeDeDev.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeartBeatController : ControllerBase
    {
        // GET: api/<HeartBeatController>
        [HttpGet]
        public IActionResult GetApiHeartbeat()
        {
            return Ok();
        }

        
    }
}
