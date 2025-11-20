using System.Text.Json.Serialization;
using MTG_DB.Models;

public class ScryfallCardFace
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("image_uris")]
    public ScryfallImageUris ImageUris { get; set; } = new();
}