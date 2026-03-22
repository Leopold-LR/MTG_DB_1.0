using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MTG_DB.Models;

public class MtgCollectionCard
{
    [Key]
    public int Id { get; set; }

    public int CollectionId { get; set; }

    [ForeignKey(nameof(CollectionId))]
    public MtgCollection? Collection { get; set; }

    /// <summary>Scryfall card ID</summary>
    public string CardScryfallId { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;
    public string SetCode { get; set; } = string.Empty;
    public string SetName { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;

    /// <summary>NM / LP / MP / HP / DMG</summary>
    public string Condition { get; set; } = "NM";

    public bool IsFoil { get; set; }
    public int Quantity { get; set; } = 1;

    /// <summary>USD price string from Scryfall at time of adding</summary>
    public decimal? PriceUsd { get; set; }

    /// <summary>Converted mana cost (for deck mana curve)</summary>
    public int Cmc { get; set; }

    /// <summary>True = mainboard, false = sideboard (Deck type only)</summary>
    public bool IsMainboard { get; set; } = true;

    public DateTime AddedAt { get; set; } = DateTime.UtcNow;
}
