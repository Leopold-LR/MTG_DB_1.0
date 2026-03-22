using System.Text.Json.Serialization;

namespace MtgInventoryApp.Models;

public class ScryfallSet
{
    [JsonPropertyName("code")]
    public string Code { get; set; } = "";

    [JsonPropertyName("name")]
    public string Name { get; set; } = "";

    [JsonPropertyName("released_at")]
    public string? ReleasedAt { get; set; }

    [JsonPropertyName("set_type")]
    public string SetType { get; set; } = "";
}

public class ScryfallSetListResponse
{
    [JsonPropertyName("data")]
    public List<ScryfallSet> Data { get; set; } = [];
}
