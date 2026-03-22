namespace MTG_DB.Models;

/// <summary>
/// Holds price data returned by CryptMtgService for a single card at La Crypte.
/// </summary>
public class CryptMtgPriceResult
{
    /// <summary>Near-Mint regular price in CAD, or null if not available.</summary>
    public string? RegularNmCad { get; set; }

    /// <summary>Near-Mint foil price in CAD, or null if not available.</summary>
    public string? FoilNmCad { get; set; }

    /// <summary>Direct product URL on cryptmtg.com.</summary>
    public string? ProductUrl { get; set; }

    /// <summary>True when at least one price is listed.</summary>
    public bool IsListed => !string.IsNullOrWhiteSpace(RegularNmCad) || !string.IsNullOrWhiteSpace(FoilNmCad);
}
