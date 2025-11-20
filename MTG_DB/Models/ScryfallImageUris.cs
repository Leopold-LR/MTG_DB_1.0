using System.Text.Json.Serialization;

namespace MTG_DB.Models
{
    public class ScryfallImageUris
    {
        [JsonPropertyName("small")]
        public string? Small { get; set; }

        [JsonPropertyName("normal")]
        public string? Normal { get; set; }

        [JsonPropertyName("large")]
        public string? Large { get; set; }

        [JsonPropertyName("png")]
        public string? Png { get; set; }

        [JsonPropertyName("art_crop")]
        public string? ArtCrop { get; set; }
    }

}
