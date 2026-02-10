using System.Text.Json.Serialization;

namespace MTG_DB.Models
{
    public class ScryfallImageUris
    {
        [JsonPropertyName("small")]
        public string Small { get; set; } = string.Empty;

        [JsonPropertyName("normal")]
        public string Normal { get; set; } = string.Empty;

        [JsonPropertyName("large")]
        public string Large { get; set; } = string.Empty;

        [JsonPropertyName("png")]
        public string Png { get; set; } = string.Empty;
    }

}
