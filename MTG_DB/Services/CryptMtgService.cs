using System.Globalization;
using System.Text.Json.Serialization;
using System.Net.Http.Json;
using Microsoft.Extensions.Caching.Memory;
using MTG_DB.Models;

namespace MtgInventoryApp.Services;

public class CryptMtgService
{
    private readonly HttpClient _http;
    private readonly IMemoryCache _cache;
    private static readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(15);

    public CryptMtgService(HttpClient http, IMemoryCache cache)
    {
        _http = http;
        _cache = cache;
    }

    public async Task<CryptMtgPrice?> GetPriceAsync(string cardName, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(cardName)) return null;

        var cacheKey = $"crypt:{cardName.ToLowerInvariant()}";
        if (_cache.TryGetValue(cacheKey, out CryptMtgPrice? cached))
            return cached;

        try
        {
            // Step 1: find product handle via Shopify predictive search
            var suggestUrl = $"search/suggest.json?q={Uri.EscapeDataString(cardName)}&resources[type]=product&resources[limit]=5";
            var suggestResp = await _http.GetAsync(suggestUrl, ct);
            if (!suggestResp.IsSuccessStatusCode) return null;

            var suggestJson = await suggestResp.Content.ReadFromJsonAsync<ShopifySuggestResponse>(cancellationToken: ct);
            var products = suggestJson?.Resources?.Results?.Products;
            if (products == null || products.Count == 0) return null;

            // Prefer exact title match, fall back to first result
            var match = products.FirstOrDefault(p =>
                p.Title?.Equals(cardName, StringComparison.OrdinalIgnoreCase) == true)
                ?? products[0];

            var handle = match.Handle;
            if (string.IsNullOrEmpty(handle)) return null;

            var productUrl = $"https://cryptmtg.com/products/{handle}";

            // Step 2: fetch full product JSON for variant prices
            var productResp = await _http.GetAsync($"products/{handle}.json", ct);
            if (!productResp.IsSuccessStatusCode)
            {
                var minimal = new CryptMtgPrice { ProductUrl = productUrl };
                _cache.Set(cacheKey, minimal, CacheDuration);
                return minimal;
            }

            var productJson = await productResp.Content.ReadFromJsonAsync<ShopifyProductResponse>(cancellationToken: ct);
            var variants = productJson?.Product?.Variants;

            decimal? regularNm = null;
            decimal? foilNm = null;
            bool hasFrench = false;

            if (variants != null)
            {
                foreach (var v in variants)
                {
                    var title = v.Title ?? "";
                    bool isNm   = title.Contains("NM", StringComparison.OrdinalIgnoreCase);
                    bool isFoil = title.Contains("foil", StringComparison.OrdinalIgnoreCase);
                    bool isFr   = title.Contains("french", StringComparison.OrdinalIgnoreCase)
                               || title.Contains(" FR ", StringComparison.OrdinalIgnoreCase)
                               || title.EndsWith(" FR", StringComparison.OrdinalIgnoreCase);

                    if (isFr) hasFrench = true;

                    if (isNm && decimal.TryParse(v.Price, NumberStyles.Any, CultureInfo.InvariantCulture, out var raw))
                    {
                        // Prices in CAD cents per spec: 510 = $5.10
                        var dollars = raw / 100m;
                        if (isFoil)
                            foilNm ??= dollars;
                        else
                            regularNm ??= dollars;
                    }
                }
            }

            var result = new CryptMtgPrice
            {
                RegularNM        = regularNm,
                FoilNM           = foilNm,
                ProductUrl       = productUrl,
                AvailableInFrench = hasFrench
            };

            _cache.Set(cacheKey, result, CacheDuration);
            return result;
        }
        catch (Exception)
        {
            return null;
        }
    }

    // ── Private Shopify JSON DTOs ──────────────────────────────────────────────

    private sealed class ShopifySuggestResponse
    {
        [JsonPropertyName("resources")]
        public ShopifyResources? Resources { get; init; }
    }

    private sealed class ShopifyResources
    {
        [JsonPropertyName("results")]
        public ShopifyResults? Results { get; init; }
    }

    private sealed class ShopifyResults
    {
        [JsonPropertyName("products")]
        public List<ShopifySuggestProduct>? Products { get; init; }
    }

    private sealed class ShopifySuggestProduct
    {
        [JsonPropertyName("handle")]
        public string? Handle { get; init; }

        [JsonPropertyName("title")]
        public string? Title { get; init; }
    }

    private sealed class ShopifyProductResponse
    {
        [JsonPropertyName("product")]
        public ShopifyProduct? Product { get; init; }
    }

    private sealed class ShopifyProduct
    {
        [JsonPropertyName("variants")]
        public List<ShopifyVariant>? Variants { get; init; }
    }

    private sealed class ShopifyVariant
    {
        [JsonPropertyName("title")]
        public string? Title { get; init; }

        [JsonPropertyName("price")]
        public string? Price { get; init; }

        [JsonPropertyName("available")]
        public bool Available { get; init; }
    }
}
