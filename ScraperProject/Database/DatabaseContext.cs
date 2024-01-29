using Microsoft.EntityFrameworkCore;
using ScraperProject.Database.Entities;

namespace ScraperProject.Database;

public class DatabaseContext : DbContext
{
    public DbSet<Store> Stores { get; set; }
    public DbSet<Laptop> Laptops { get; set; }

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        Laptop.OnModelCreating(modelBuilder);
        Store.OnModelCreating(modelBuilder);
    }
}
