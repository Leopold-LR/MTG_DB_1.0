using Microsoft.EntityFrameworkCore;
using MTG_DB.Data;
using MTG_DB.Models;

namespace MtgInventoryApp.Services;

public class CollectionService
{
    private readonly CollectionDbContext _db;

    public CollectionService(CollectionDbContext db) => _db = db;

    // ── Collections ────────────────────────────────────────────────────────────

    public async Task<List<MtgCollection>> GetAllCollectionsAsync() =>
        await _db.Collections
            .Include(c => c.Cards)
            .OrderByDescending(c => c.CreatedAt)
            .ToListAsync();

    public async Task<MtgCollection?> GetCollectionAsync(int id) =>
        await _db.Collections
            .Include(c => c.Cards)
            .FirstOrDefaultAsync(c => c.Id == id);

    public async Task<MtgCollection> CreateCollectionAsync(string name, string type, string icon)
    {
        var col = new MtgCollection
        {
            Name      = name.Trim(),
            Type      = type,
            Icon      = icon,
            CreatedAt = DateTime.UtcNow,
        };
        _db.Collections.Add(col);
        await _db.SaveChangesAsync();
        return col;
    }

    public async Task DeleteCollectionAsync(int id)
    {
        var col = await _db.Collections.FindAsync(id);
        if (col != null)
        {
            _db.Collections.Remove(col);
            await _db.SaveChangesAsync();
        }
    }

    // ── Cards ──────────────────────────────────────────────────────────────────

    public async Task AddCardAsync(int collectionId, MtgCollectionCard card)
    {
        // If an identical card (same Scryfall ID, foil, condition, board) already exists, just increment quantity
        var existing = await _db.CollectionCards.FirstOrDefaultAsync(c =>
            c.CollectionId    == collectionId &&
            c.CardScryfallId  == card.CardScryfallId &&
            c.IsFoil          == card.IsFoil &&
            c.Condition       == card.Condition &&
            c.IsMainboard     == card.IsMainboard);

        if (existing != null)
        {
            existing.Quantity += card.Quantity;
        }
        else
        {
            card.CollectionId = collectionId;
            card.AddedAt      = DateTime.UtcNow;
            _db.CollectionCards.Add(card);
        }

        await _db.SaveChangesAsync();
    }

    public async Task UpdateQuantityAsync(int cardId, int delta)
    {
        var card = await _db.CollectionCards.FindAsync(cardId);
        if (card == null) return;

        card.Quantity += delta;
        if (card.Quantity <= 0)
            _db.CollectionCards.Remove(card);

        await _db.SaveChangesAsync();
    }

    public async Task RemoveCardAsync(int cardId)
    {
        var card = await _db.CollectionCards.FindAsync(cardId);
        if (card != null)
        {
            _db.CollectionCards.Remove(card);
            await _db.SaveChangesAsync();
        }
    }
}
