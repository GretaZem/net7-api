using Newtonsoft.Json;

namespace net7_api.Services
{
    public class ExternalApiService
    {
        private readonly ILogger<ExternalApiService> _logger;
        private readonly HttpClient _httpClient;

        public ExternalApiService(ILogger<ExternalApiService> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        public async Task<List<PollutionPoint>> GetDataFromExternalApiAsync()
        {
            _logger.LogInformation("Fetching data from external API");
            var response = await _httpClient.GetAsync("https://get.data.gov.lt/datasets/gov/lgt/potencialus_tarsos_zidiniai/TarsosZidinys/:format/json");

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex) 
            {
                _logger.LogError($"Failed to get external data: {ex.Message}");
            }

            var dataRoot = JsonConvert.DeserializeObject<DataRoot>(await response.Content.ReadAsStringAsync());

            return dataRoot.Data;
        }
    }
}
