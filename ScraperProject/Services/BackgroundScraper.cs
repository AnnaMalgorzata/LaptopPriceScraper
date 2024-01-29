using ScraperProject.Database;
using ScraperProject.Database.Entities;
using ScraperProject.Services.Abstractions;

namespace ScraperProject.Services;

public class BackgroundScraper : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public BackgroundScraper(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

        var frequencyInSeconds = configuration.GetValue<int>("Website:ScrapingFrequencyInSeconds");

        while (true)
        {
            try
            {
                await Task.WhenAll(
                    Task.Run(() => CheckAndUpdatePrices(Store.SwiatLaptopowStoreId)),
                    Task.Run(() => CheckAndUpdatePrices(Store.MoreleStoreId))
                    );

                await Task.Delay(frequencyInSeconds * 1000);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                await Task.Delay(10_000);
            }
        }
    }

    private void CheckAndUpdatePrices(int storeId)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var webScraper = scope.ServiceProvider.GetRequiredService<WebScraper>();
        var dbContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
        var mailService = scope.ServiceProvider.GetRequiredService<IMailService>();

        var laptops = dbContext.Laptops.Where(x => x.StoreId == storeId);

        foreach (var laptop in laptops)
        {
            var newPrice = ScrapPrice(laptop.Name);
            if (laptop.Price != newPrice)
            {
                var oldPrice = laptop.Price;
                laptop.Price = newPrice;
                if (oldPrice != 0)
                {
                    var storeName = storeId == Store.SwiatLaptopowStoreId
                        ? Store.SwiatLaptopow
                        : Store.Morele;

                    var emailBody = $"Price changed! \nLaptop: [{laptop.Name}] \nFrom store: {storeName}\nOld price: {oldPrice}\nNewPrice: {laptop.Price}";
                    Task.Run(async () => await mailService.SendMail(new Dtos.MailData
                    {
                        ReceiverEmail = "233356@student.uek.krakow.pl",
                        EmailSubject = "Scraper Detect Price change",
                        EmailBody = emailBody,
                    }));
                }
            }
        }

        webScraper.CloseBrowser();
        dbContext.SaveChanges();

        decimal ScrapPrice(string laptopName)
            => storeId == Store.SwiatLaptopowStoreId
            ? webScraper.ScrapLaptopPriceFromSwiatLaptopow(laptopName)
            : webScraper.ScrapLaptopPriceFromMorele(laptopName);
    }
}
