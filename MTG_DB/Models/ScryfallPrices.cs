using System.Text.Json.Serialization;

namespace MTG_DB.Models
{
    public class ScryfallPrices
    {
        [JsonPropertyName("usd")]
        public string? Usd { get; set; }

        [JsonPropertyName("usd_foil")]
        public string? UsdFoil { get; set; }

        [JsonPropertyName("eur")]
        public string? Eur { get; set; }
    }

}
