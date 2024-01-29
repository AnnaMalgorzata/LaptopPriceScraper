using Microsoft.EntityFrameworkCore;

namespace ScraperProject.Database.Entities;

public class Store
{
    public int Id { get; init; }
    public string Name { get; init; }
    public ICollection<Laptop> Laptops { get; init; }

    internal static void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Store>(builder =>
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Name).IsUnique();
        });
    }

    public const string SwiatLaptopow = "swiat-laptopow.pl";
    public const string Morele = "morele.net";

    public const int SwiatLaptopowStoreId = 1;
    public const int MoreleStoreId = 2;
}
