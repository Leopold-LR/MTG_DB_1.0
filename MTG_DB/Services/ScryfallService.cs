using MtgInventoryApp.Models;
using MTG_DB.Models;
using System.Net.Http;
using System.Net.Http.Json;

namespace MtgInventoryApp.Services;

// Set types considered "playable" for the set filter dropdown
file static class PlayableSetTypes
{
    public static readonly HashSet<string> All = [
        "expansion", "core", "masters", "draft_innovation",
        "commander", "starter", "planechase", "archenemy",
        "memorabilia", "funny"
    ];
}

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

    public async Task<List<ScryfallSet>> GetSetsAsync(CancellationToken ct = default)
    {
        try
        {
            var response = await _http.GetAsync("/sets", ct);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ScryfallSetListResponse>(cancellationToken: ct);
                return result?.Data
                    .Where(s => PlayableSetTypes.All.Contains(s.SetType))
                    .OrderByDescending(s => s.ReleasedAt)
                    .ToList() ?? [];
            }
        }
        catch (HttpRequestException) { }

        return [];
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
        catch (HttpRequestException) { }

        return null;
    }

    public async Task<List<ScryfallRuling>> GetRulingsAsync(string cardId, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(cardId)) return [];

        try
        {
            var response = await _http.GetAsync($"cards/{cardId}/rulings", ct);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ScryfallRulingListResponse>(cancellationToken: ct);
                return result?.Data ?? [];
            }
        }
        catch (HttpRequestException) { }

        return [];
    }

    public async Task<List<ScryfallCard>> GetPrintsAsync(string cardName, string excludeId, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(cardName)) return [];

        try
        {
            var q = Uri.EscapeDataString($"!\"{cardName}\"");
            var response = await _http.GetAsync($"cards/search?q={q}&unique=prints&order=released", ct);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ScryfallListResponse>(cancellationToken: ct);
                return result?.Data?.Where(c => c.Id != excludeId).ToList() ?? [];
            }
        }
        catch (HttpRequestException) { }

        return [];
    }
}