using System.Text.Json.Serialization;

namespace MotChecking.Models
{
    public class VehicleInfo
    {
        [JsonPropertyName("make")]
        public string? Make { get; set; }

        [JsonPropertyName("model")]
        public string? Model { get; set; }

        [JsonPropertyName("primaryColour")]
        public string? Colour { get; set; }

        [JsonPropertyName("motTests")]
        public List<MotInfo>? MotInfos { get; set; }
    }
}
