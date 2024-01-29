using ScraperProject.ViewModels;

namespace ScraperProject.Services.Abstractions;

public interface ILaptopService
{
    /// <summary>
    /// Delete laptop from database
    /// </summary>
    Task DeleteLaptop(int id);

    /// <summary>
    /// Add new laptop
    /// </summary>
    Task AddLaptop(NewLaptopViewModel vm);
}
