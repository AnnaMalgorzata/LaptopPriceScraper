using Microsoft.EntityFrameworkCore;
using ScraperProject.Database;
using ScraperProject.Services.Abstractions;
using ScraperProject.ViewModels;
using static ScraperProject.ViewModels.StoresViewModel;

namespace ScraperProject.Services;

internal class StoreService : IStoreService
{
    private readonly DatabaseContext _databaseContext;

    public StoreService(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<StoresViewModel> GetStoresWithLaptops()
    {
        var storesWithLaptops = await _databaseContext.Stores
            .Select(store => new StoreWithLaptops
            {
                Name = store.Name,
                Laptops = store.Laptops.Select(laptop => new Laptop
                {
                    Id = laptop.Id,
                    Name = laptop.Name,
                    Price = laptop.Price
                })
            })
            .AsNoTracking()
            .ToListAsync();

        return new StoresViewModel { StoresWithLaptops = storesWithLaptops };
    }
}
