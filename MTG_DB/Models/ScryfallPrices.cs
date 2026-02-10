using System.Text.Json.Serialization;

namespace MTG_DB.Models
{
    public class ScryfallPrices
    {
        [JsonPropertyName("usd")]
        public string Usd { get; set; } = string.Empty;

        [JsonPropertyName("usd_foil")]
        public string UsdFoil { get; set; } = string.Empty;

        [JsonPropertyName("usd_etched")]
        public string UsdEtched { get; set; } = string.Empty;

        [JsonPropertyName("eur")]
        public string Eur { get; set; } = string.Empty;

        [JsonPropertyName("eur_foil")]
        public string EurFoil { get; set; } = string.Empty;

        [JsonPropertyName("tix")]
        public string Tix { get; set; } = string.Empty;
    }

}
