using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace ScraperProject.Services;

public class WebScraper
{
    private readonly ChromeDriver _driver;
    private readonly IConfiguration _configuration;

    public WebScraper(IConfiguration configuration)
    {
        var options = new ChromeOptions();
        options.AddArguments(new List<string>() { "headless", "disable-gpu" });
        options.AcceptInsecureCertificates = true;
        _driver = new ChromeDriver(options);

        _configuration = configuration;
    }

    public void CloseBrowser() => _driver.Quit();

    public decimal ScrapLaptopPriceFromSwiatLaptopow(string laptopName)
    {
        var url = _configuration.GetValue<string>("Website:swiatLaptopowUrl");

        _driver.Navigate().GoToUrl(url);

        var searchInput = _driver.FindElement(By.CssSelector(".js-search-input"));
        var jsExecutor = (IJavaScriptExecutor)_driver;
        jsExecutor.ExecuteScript($"arguments[0].value='{laptopName}';", searchInput);

        var searchButton = _driver.FindElement(By.CssSelector("button.search-form__btn"));
        ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", searchButton);

        var article = _driver.FindElement(By.TagName("article"));
        var price = article.FindElement(By.CssSelector("span.price")).Text
            .Replace(" ", "")
            .Replace("zł", "");

        var priceAsNumber = Convert.ToDecimal(price);

        return priceAsNumber;
    }

    public decimal ScrapLaptopPriceFromMorele(string laptopName)
    {
        var url = _configuration.GetValue<string>("Website:moreleUrl");

        _driver.Navigate().GoToUrl(url);

        var searchInput = _driver.FindElement(By.CssSelector("input[name='q']"));
        ((IJavaScriptExecutor)_driver).ExecuteScript($"arguments[0].value='{laptopName}';", searchInput);

        var searchButton = _driver.FindElement(By.CssSelector("button.h-quick-search-submit"));
        Thread.Sleep(1000);
        ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", searchButton);

        var laptopInfo = _driver.FindElement(By.XPath("//*[@id=\"category\"]/div[2]/div/div[6]/div[1]/div"));
        var price = laptopInfo.FindElement(By.ClassName("price-new")).Text
            .Replace(" ", "")
            .Replace("zł", "");

        var priceAsNumber = Convert.ToDecimal(price);

        return priceAsNumber;
    }
}
