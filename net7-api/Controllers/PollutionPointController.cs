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
        private readonly ApiDbContext _context;
        private readonly DataImporter _dataImporter;

        public PollutionPointController(ApiDbContext apiDbContext, DataImporter dataImporter)
        {
            _context = apiDbContext;
            _dataImporter = dataImporter;
        }

        [HttpGet(Name = "GetPollutionPoints")]
        public async Task<ActionResult<IEnumerable<PollutionPointGroup>>> Get()
        {
            await _dataImporter.ImportAsync();
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