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
        public async Task<ActionResult<IEnumerable<PollutionPoint>>> Get()
        {
            await _dataImporter.ImportAsync();
            var data = _context.PollutionPoints.Take(2).ToList(); // returning 2, since Swagger can't handle whole list

            var response = new List<PollutionPoint>();
            var mapper = InitAutoMapper();
            foreach (var item in data)
            {
                response.Add(mapper.Map<PollutionPoint>(item));
            }

            return Ok(JsonConvert.SerializeObject(response));
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