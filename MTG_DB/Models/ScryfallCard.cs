using MTG_DB.Models;
using System.Text.Json.Serialization;

public class ScryfallCard
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("set")]
    public string Set { get; set; } = string.Empty;

    [JsonPropertyName("set_name")]
    public string SetName { get; set; } = string.Empty;

    [JsonPropertyName("rarity")]
    public string Rarity { get; set; } = string.Empty;

    [JsonPropertyName("layout")]
    public string Layout { get; set; } = string.Empty;

    [JsonPropertyName("card_faces")]
    public List<CardFace> CardFaces { get; set; } = new();

    // Properties for regular cards
    [JsonPropertyName("mana_cost")]
    public string ManaCost { get; set; } = string.Empty;

    [JsonPropertyName("type_line")]
    public string TypeLine { get; set; } = string.Empty;

    [JsonPropertyName("oracle_text")]
    public string OracleText { get; set; } = string.Empty;

    [JsonPropertyName("collector_number")]
    public string CollectorNumber { get; set; } = string.Empty;

    [JsonPropertyName("power")]
    public string Power { get; set; } = string.Empty;

    [JsonPropertyName("toughness")]
    public string Toughness { get; set; } = string.Empty;

    [JsonPropertyName("loyalty")]
    public string Loyalty { get; set; } = string.Empty;

    [JsonPropertyName("cmc")]
    public decimal ConvertedManaCost { get; set; }

    [JsonPropertyName("artist")]
    public string Artist { get; set; } = string.Empty;

    [JsonPropertyName("border_color")]
    public string BorderColor { get; set; } = string.Empty;

    [JsonPropertyName("frame")]
    public string Frame { get; set; } = string.Empty;

    [JsonPropertyName("lang")]
    public string Lang { get; set; } = string.Empty;

    [JsonPropertyName("image_uris")]
    public ScryfallImageUris ImageUris { get; set; } = new();

    [JsonPropertyName("prices")]
    public ScryfallPrices Prices { get; set; } = new();
}
