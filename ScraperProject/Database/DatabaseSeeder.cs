using Microsoft.EntityFrameworkCore;
using ScraperProject.Database.Entities;

namespace ScraperProject.Database;

internal static class DatabaseSeeder
{
    public static void Seed(DatabaseContext databaseContext)
    {
        databaseContext.Stores.AddRange(new List<Store>()
        {
            new Store { Id = Store.SwiatLaptopowStoreId, Name = Store.SwiatLaptopow },
            new Store { Id = Store.MoreleStoreId, Name = Store.Morele }
        });

        databaseContext.Laptops.AddRange(new List<Laptop>()
        {
            new Laptop { Id = 1, Name = "LAPTOP HP PAVILION 15-EG0041NA / 31Z13EA / INTEL", Price = 1000, StoreId = Store.SwiatLaptopowStoreId },
            new Laptop { Id = 2, Name = "LAPTOP HP PAVILION 15-EH0023NW / 365P4EA / AMD", Price = 0, StoreId = Store.SwiatLaptopowStoreId },
            new Laptop { Id = 3, Name = "LAPTOP HP 15S-EQ2104NW / 4H379EA / AMD RYZEN 3 / 8GB", Price = 0, StoreId = Store.SwiatLaptopowStoreId },
            new Laptop { Id = 4, Name = "LAPTOP HP PAVILION 15-EH1154NW / 4H3T9EA / AMD", Price = 0, StoreId = Store.SwiatLaptopowStoreId },
            new Laptop { Id = 5, Name = "Laptop HP Laptop HP VICTUS 15-FB0008CA - Ryzen 5-5600H | 8GB | SSD 512GB | 15.6\"FHD 144Hz | GeForce GTX1650 4096MB pamięci własnej | Windows 11", Price = 0, StoreId = Store.MoreleStoreId },
            new Laptop { Id = 6, Name = "Laptop HP HP Victus Gaming 16 144Hz Ryzen 5 7640HS 16GB DDR5 512GB SSD RTX 4050 6GB", Price = 0, StoreId = Store.MoreleStoreId }
        });

        databaseContext.SaveChanges();
    }
}
