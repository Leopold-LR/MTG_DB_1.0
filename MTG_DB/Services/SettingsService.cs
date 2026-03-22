using System.Globalization;
using Microsoft.JSInterop;

namespace MtgInventoryApp.Services;

public class SettingsService
{
    private const string CurrencyKey = "settings:priceCurrency";
    private const string RateKey     = "settings:usdToCadRate";

    public string  PriceCurrency { get; set; } = "USD";
    public decimal UsdToCadRate  { get; set; } = 1.38m;

    public bool IsCAD => PriceCurrency == "CAD";

    public async Task LoadAsync(IJSRuntime js)
    {
        var currency = await js.InvokeAsync<string?>("localStorage.getItem", CurrencyKey);
        if (!string.IsNullOrEmpty(currency))
            PriceCurrency = currency;

        var rate = await js.InvokeAsync<string?>("localStorage.getItem", RateKey);
        if (decimal.TryParse(rate, NumberStyles.Any, CultureInfo.InvariantCulture, out var r) && r > 0)
            UsdToCadRate = r;
    }

    public async Task SaveAsync(IJSRuntime js)
    {
        await js.InvokeVoidAsync("localStorage.setItem", CurrencyKey, PriceCurrency);
        await js.InvokeVoidAsync("localStorage.setItem", RateKey, UsdToCadRate.ToString(CultureInfo.InvariantCulture));
    }

    /// <summary>
    /// Formats a Scryfall USD price string according to the current currency preference.
    /// Returns "—" when the value is null or empty.
    /// </summary>
    public string FormatUsdPrice(string? usdVal)
    {
        if (string.IsNullOrWhiteSpace(usdVal)) return "—";

        if (!decimal.TryParse(usdVal, NumberStyles.Any, CultureInfo.InvariantCulture, out var val))
            return $"${usdVal}";

        return IsCAD
            ? $"CA${val * UsdToCadRate:F2}"
            : $"${val:F2}";
    }

    /// <summary>
    /// Short label for the active currency symbol used in UI ("$" or "CA$").
    /// </summary>
    public string CurrencySymbol => IsCAD ? "CA$" : "$";
}
