using System.ComponentModel.DataAnnotations;

namespace net7_api.Models
{
    public class PollutionPointModel
    {
        public string? Type { get; set; }

        [Key]
        public string? Id { get; set; }

        public string? Revision { get; set; }

        public int ObjectNumber { get; set; }

        public string? Address { get; set; }

        public string? ApplicationNumber { get; set; }

        public DateTime ApplicationDate { get; set; }

        public string? ObjectCondition { get; set; }

        public string? ObjectType { get; set; }

        public string? EnvironmentalDanger { get; set; }

        public string? Coordinates { get; set; }
    }
}
