using Microsoft.EntityFrameworkCore;

namespace ScraperProject.Database.Entities;

public class Laptop
{
    public int Id { get; init; }
    public string Name { get; init; }
    public decimal? Price { get; set; }
    public int StoreId {  get; init; }

    internal static void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Laptop>(builder =>
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasIndex(x => new { x.Name, x.StoreId }).IsUnique();

            builder.HasOne<Store>()
                .WithMany(x => x.Laptops)
                .HasForeignKey(x => x.StoreId);
        });
    }
}
