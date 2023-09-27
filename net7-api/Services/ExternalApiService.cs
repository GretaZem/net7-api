using Newtonsoft.Json;

namespace net7_api.Services
{
    public class ExternalApiService
    {
        private readonly HttpClient _httpClient;

        public ExternalApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<PollutionPoint>> GetDataFromExternalApiAsync()
        {
            var response = await _httpClient.GetAsync("https://get.data.gov.lt/datasets/gov/lgt/potencialus_tarsos_zidiniai/TarsosZidinys/:format/json");
            response.EnsureSuccessStatusCode();

            var dataRoot = JsonConvert.DeserializeObject<DataRoot>(await response.Content.ReadAsStringAsync());

            return dataRoot.Data;
        }
    }
}
