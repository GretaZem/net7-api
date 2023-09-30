using AutoMapper;
using net7_api.Context;
using net7_api.Models;
using net7_api.Services;

namespace net7_api
{
    public class DataImporter
    {
        private readonly ILogger<DataImporter> _logger;
        private ApiDbContext _dbContext;
        private readonly ExternalApiService _externalApiService;

        public DataImporter(ILogger<DataImporter> logger, ApiDbContext dbContext, ExternalApiService externalApiService)
        {
            _logger = logger;
            _dbContext = dbContext;
            _externalApiService = externalApiService;
        }

        public async Task ImportAsync()
        {
            var data = await _externalApiService.GetDataFromExternalApiAsync();
            var mapper = InitAutoMapper();

            try
            {
                foreach (var item in data)
                {
                    _dbContext.Add(mapper.Map<PollutionPointModel>(item));
                }

                _dbContext.SaveChanges();
            }
            catch (ArgumentException) 
            {
                _logger.LogInformation("Item already exists in database");
            }
        }

        private Mapper InitAutoMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<PollutionPoint, PollutionPointModel>();
            });

            var mapper = new Mapper(config);
            return mapper;
        }
    }
}
