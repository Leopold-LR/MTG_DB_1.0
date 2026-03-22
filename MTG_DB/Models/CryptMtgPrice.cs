namespace MTG_DB.Models;

public class CryptMtgPrice
{
    public decimal? RegularNM { get; init; }
    public decimal? FoilNM { get; init; }
    public string? ProductUrl { get; init; }
    public bool AvailableInFrench { get; init; }
}
