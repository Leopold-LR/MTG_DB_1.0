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

        [JsonPropertyName("usd_etched")]
        public string? UsdEtched { get; set; }

        [JsonPropertyName("tix")]
        public string? Tix { get; set; }

        [JsonPropertyName("eur_foil")]
        public string? EurFoil { get; set; }
    }

}
