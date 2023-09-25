using Microsoft.AspNetCore.Mvc;

namespace net7_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PolutionPointController : ControllerBase
    {
        private readonly ILogger<PolutionPointController> _logger;

        public PolutionPointController(ILogger<PolutionPointController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetPolutionPoints")]
        public async Task<ActionResult<IEnumerable<PolutionPoint>>> Get()
        {
            return Ok(new List<PolutionPoint>());
        }
    }
}