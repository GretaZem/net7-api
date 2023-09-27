using Microsoft.AspNetCore.Mvc;
using net7_api.Services;

namespace net7_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PollutionPointController : ControllerBase
    {
        private readonly ILogger<PollutionPointController> _logger;
        private readonly ExternalApiService _externalApiService;

        public PollutionPointController(ILogger<PollutionPointController> logger, ExternalApiService externalApiService)
        {
            _logger = logger;
            _externalApiService = externalApiService;
        }

        [HttpGet(Name = "GetPollutionPoints")]
        public async Task<ActionResult<IEnumerable<PollutionPoint>>> Get()
        {
            var data = await _externalApiService.GetDataFromExternalApiAsync();

            return data.Take(2).ToList(); // returning 2, since Swagger can't handle whole list
        }
    }
}