using Microsoft.EntityFrameworkCore;
using ScraperProject.Database;
using ScraperProject.Services.Abstractions;
using ScraperProject.ViewModels;

namespace ScraperProject.Services;

internal class LaptopService : ILaptopService
{
    private readonly DatabaseContext _databaseContext;

    public LaptopService(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task DeleteLaptop(int id)
    {
        var laptop = _databaseContext.Laptops.Single(x => x.Id == id);

        _databaseContext.Laptops.Remove(laptop);

        await _databaseContext.SaveChangesAsync();
    }

    public async Task AddLaptop(NewLaptopViewModel vm)
    {
        var store = _databaseContext.Stores.SingleOrDefault(x => x.Name == vm.StoreName);

        _databaseContext.Laptops.Add(new Database.Entities.Laptop
        {
            Name = vm.Name,
            StoreId = store.Id
        });

        await _databaseContext.SaveChangesAsync();
    }
}
