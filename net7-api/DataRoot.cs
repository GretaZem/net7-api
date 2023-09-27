using Newtonsoft.Json;

namespace net7_api
{
    public class DataRoot
    {
        [JsonProperty("_data")]
        public List<PollutionPoint>? Data { get; set; }
    }
}
