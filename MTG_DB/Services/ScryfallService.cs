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

    public async Task<ScryfallListResponse> SearchAsync(string query, int page = 1, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(query)) return new ScryfallListResponse();

        var endpoint = $"/cards/search?q={Uri.EscapeDataString(query.Trim())}&page={page}";

        HttpResponseMessage response;
        try
        {
            response = await _http.GetAsync(endpoint, ct);
        }
        catch (HttpRequestException)
        {
            return new ScryfallListResponse();
        }

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<ScryfallListResponse>(cancellationToken: ct)
                   ?? new ScryfallListResponse();
        }

        return new ScryfallListResponse();
    }

    public async Task<ScryfallCard?> GetCardByIdAsync(string id, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(id)) return null;

        try
        {
            var response = await _http.GetAsync($"cards/{id}", ct);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ScryfallCard>(cancellationToken: ct);
            }
        }
        catch (HttpRequestException)
        {

        }

        return null;
    }
}