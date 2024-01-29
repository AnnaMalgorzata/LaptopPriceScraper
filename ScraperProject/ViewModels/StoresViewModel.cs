namespace ScraperProject.ViewModels;

public class StoresViewModel
{
    public List<StoreWithLaptops> StoresWithLaptops { get; init; }

    public class StoreWithLaptops
    {
        public string Name { get; init; }
        public IEnumerable<Laptop> Laptops { get; init; }
    }

    public class Laptop
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public decimal? Price { get; init; }
    }
}
