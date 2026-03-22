using System.ComponentModel.DataAnnotations;

namespace MTG_DB.Models;

public class MtgCollection
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    public string Icon { get; set; } = "📦";

    /// <summary>Binder, Deck, or Bulk</summary>
    public string Type { get; set; } = "Bulk";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public List<MtgCollectionCard> Cards { get; set; } = new();
}
