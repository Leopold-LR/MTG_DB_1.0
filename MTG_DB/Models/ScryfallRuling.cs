using System.Text.Json.Serialization;

namespace MTG_DB.Models;

public class ScryfallRuling
{
    [JsonPropertyName("oracle_id")]
    public string? OracleId { get; set; }

    [JsonPropertyName("source")]
    public string? Source { get; set; }

    [JsonPropertyName("published_at")]
    public string? PublishedAt { get; set; }

    [JsonPropertyName("comment")]
    public string? Comment { get; set; }
}

public class ScryfallRulingListResponse
{
    [JsonPropertyName("data")]
    public List<ScryfallRuling> Data { get; set; } = [];
}
