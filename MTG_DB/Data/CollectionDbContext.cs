using Microsoft.EntityFrameworkCore;
using MTG_DB.Models;

namespace MTG_DB.Data;

public class CollectionDbContext : DbContext
{
    public CollectionDbContext(DbContextOptions<CollectionDbContext> options) : base(options) { }

    public DbSet<MtgCollection> Collections => Set<MtgCollection>();
    public DbSet<MtgCollectionCard> CollectionCards => Set<MtgCollectionCard>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MtgCollection>(e =>
        {
            e.HasKey(c => c.Id);
            e.Property(c => c.Name).IsRequired();
            e.Property(c => c.Icon).HasDefaultValue("📦");
            e.Property(c => c.Type).HasDefaultValue("Bulk");
        });

        modelBuilder.Entity<MtgCollectionCard>(e =>
        {
            e.HasKey(c => c.Id);
            e.HasOne(c => c.Collection)
             .WithMany(col => col.Cards)
             .HasForeignKey(c => c.CollectionId)
             .OnDelete(DeleteBehavior.Cascade);
            e.Property(c => c.Condition).HasDefaultValue("NM");
            e.Property(c => c.Quantity).HasDefaultValue(1);
            e.Property(c => c.IsMainboard).HasDefaultValue(true);
        });
    }
}
