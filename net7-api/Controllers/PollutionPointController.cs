using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using net7_api.Context;
using net7_api.Models;
using Newtonsoft.Json;

namespace net7_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PollutionPointController : ControllerBase
    {
        private readonly ILogger<PollutionPointController> _logger;
        private readonly ApiDbContext _context;
        private readonly DataImporter _dataImporter;

        public PollutionPointController(ILogger<PollutionPointController> logger, ApiDbContext apiDbContext, DataImporter dataImporter)
        {
            _logger = logger;
            _context = apiDbContext;
            _dataImporter = dataImporter;
        }

        [HttpGet(Name = "GetPollutionPoints")]
        public async Task<ActionResult<IEnumerable<PollutionPointGroup>>> Get()
        {
            _logger.LogInformation("Getting Pollution points.");
            var mapper = InitAutoMapper();

            var data = _context.PollutionPoints
                .GroupBy(x => x.ObjectType)
                .Select(g => new PollutionPointGroup
            {
                PollutionPointType = g.Key,
                GroupSize = g.Count(),
                PollutionPoints = g.Select(
                    x => mapper.Map<PollutionPoint>(x)
                    ).ToList()
            })
            .ToList();

            return Ok(JsonConvert.SerializeObject(data));
        }

        private Mapper InitAutoMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<PollutionPointModel, PollutionPoint>();
            });

            var mapper = new Mapper(config);
            return mapper;
        }
    }
}