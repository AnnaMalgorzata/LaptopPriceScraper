using ScraperProject.ViewModels;

namespace ScraperProject.Services.Abstractions;

public interface IStoreService
{
    /// <summary>
    /// Get all stores with connected laptops as <see cref="StoresViewModel"/>
    /// </summary>
    Task<StoresViewModel> GetStoresWithLaptops();
}
