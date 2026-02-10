using System.Text.Json.Serialization;
using MTG_DB.Models;

public class CardFace
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("mana_cost")]
    public string ManaCost { get; set; } = string.Empty;

    [JsonPropertyName("type_line")]
    public string TypeLine { get; set; } = string.Empty;

    [JsonPropertyName("oracle_text")]
    public string OracleText { get; set; } = string.Empty;

    [JsonPropertyName("power")]
    public string Power { get; set; } = string.Empty;

    [JsonPropertyName("toughness")]
    public string Toughness { get; set; } = string.Empty;

    [JsonPropertyName("loyalty")]
    public string Loyalty { get; set; } = string.Empty;

    [JsonPropertyName("artist")]
    public string Artist { get; set; } = string.Empty;

    [JsonPropertyName("image_uris")]
    public ScryfallImageUris ImageUris { get; set; } = new();
}