using MTG_DB.Models;
using System.Text.Json.Serialization;

public class ScryfallCard
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = "";

    [JsonPropertyName("name")]
    public string Name { get; set; } = "";

    [JsonPropertyName("set")]
    public string Set { get; set; } = "";

    [JsonPropertyName("set_name")]
    public string SetName { get; set; } = "";

    [JsonPropertyName("rarity")]
    public string Rarity { get; set; } = "";

    [JsonPropertyName("image_uris")]
    public ScryfallImageUris? ImageUris { get; set; }

    [JsonPropertyName("OracleText")]
    public string OracleText { get; set; } = "";

    [JsonPropertyName("layout")]
    public string Layout { get; set; } = string.Empty;

    [JsonPropertyName("CollectorNumber")]
    public string CollectorNumber { get; set; } = string.Empty;

    [JsonPropertyName("TypeLine")]
    public string TypeLine { get; set; } = string.Empty;

    [JsonPropertyName("ManaCost")]
    public string ManaCost { get; set; } = string.Empty;

    [JsonPropertyName("card_faces")]
    public List<ScryfallCardFace> CardFaces { get; set; } = [];

    [JsonPropertyName("prices")]
    public ScryfallPrices? Prices { get; set; }
}
