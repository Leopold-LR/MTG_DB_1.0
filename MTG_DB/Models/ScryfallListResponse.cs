using System.Text.Json.Serialization;

namespace MtgInventoryApp.Models;

public class ScryfallListResponse
{
    [JsonPropertyName("data")]
    public ScryfallCard[] Data { get; set; } = Array.Empty<ScryfallCard>();

    [JsonPropertyName("has_more")]
    public bool HasMore { get; set; }

    [JsonPropertyName("next_page")]
    public string? NextPage { get; set; }

    [JsonPropertyName("total_cards")]
    public int TotalCards { get; set; }
}
