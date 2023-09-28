using AutoMapper;
using net7_api.Context;
using net7_api.Models;
using net7_api.Services;

namespace net7_api
{
    public class DataImporter
    {
        private ApiDbContext _dbContext;
        private readonly ExternalApiService _externalApiService;

        public DataImporter(ApiDbContext dbContext, ExternalApiService externalApiService)
        {
            _dbContext = dbContext;
            _externalApiService = externalApiService;
        }

        public async Task ImportAsync()
        {
            var data = await _externalApiService.GetDataFromExternalApiAsync();

            var mapper = InitAutoMapper();

            foreach (var item in data)
            {
                _dbContext.Add(mapper.Map<PollutionPointModel>(item));
            }

            _dbContext.SaveChanges();
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
