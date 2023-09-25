using Newtonsoft.Json;

namespace net7_api
{
    public class PollutionPoint
    {
        [JsonProperty("_type")]
        public string? Type { get; set; }

        [JsonProperty("_id")]
        public string? Id { get; set; }

        [JsonProperty("_revision")]
        public string? Revision { get; set; }

        [JsonProperty("objecto_nr")]
        public int ObjectNumber { get; set; }

        [JsonProperty("ptz_adresas")]
        public string? Address { get; set; }

        [JsonProperty("ptz_anketos_nr")]
        public string? ApplicationNumber { get; set; }

        [JsonProperty("ptz_anketos_data")]
        public DateTime ApplicationDate { get; set; }

        [JsonProperty("ptz_objekto_bukle")]
        public string? ObjectCondition { get; set; }

        [JsonProperty("ptz_objekto_tipas")]
        public string? ObjectType { get; set; }

        [JsonProperty("ptz_pavojingumas_aplinkai")]
        public string? EnvironmentalDanger { get; set; }

        [JsonProperty("koord")]
        public string? Coordinates { get; set; }
    }
}