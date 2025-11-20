using MtgInventoryApp.Models;
using System.Net.Http;
using System.Net.Http.Json;

namespace MtgInventoryApp.Services;

public class ScryfallService
{
    private readonly HttpClient _http;

    public ScryfallService(HttpClient http)
    {
        _http = http;
    }

    public async Task<ScryfallCard[]> SearchAsync(string query, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(query)) return Array.Empty<ScryfallCard>();

        var endpoint = $"/cards/search?q={Uri.EscapeDataString(query.Trim())}";

        HttpResponseMessage response;
        try
        {
            response = await _http.GetAsync(endpoint, ct);
        }
        catch (HttpRequestException)
        {
            return Array.Empty<ScryfallCard>();
        }

        if (response.IsSuccessStatusCode)
        {
            var list = await response.Content.ReadFromJsonAsync<ScryfallListResponse>(cancellationToken: ct);
            return list?.Data ?? Array.Empty<ScryfallCard>();
        }

        return Array.Empty<ScryfallCard>();
    }
}