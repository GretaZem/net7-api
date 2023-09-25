using Microsoft.AspNetCore.Mvc;

namespace net7_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PollutionPointController : ControllerBase
    {
        private readonly ILogger<PollutionPointController> _logger;

        public PollutionPointController(ILogger<PollutionPointController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetPollutionPoints")]
        public async Task<ActionResult<IEnumerable<PollutionPoint>>> Get()
        {
            return Ok(new List<PollutionPoint>());
        }
    }
}