using Newtonsoft.Json;

namespace net7_api
{
    public class PollutionPointGroup
    {
        [JsonProperty("ptz_objekto_tipas")]
        public string PollutionPointType { get; set; }

        [JsonProperty("grupes_dydis")]
        public int GroupSize { get; set; }

        public List<PollutionPoint> PollutionPoints { get; set; }
    }
}
