using Microsoft.AspNetCore.Mvc;
using ScraperProject.Services.Abstractions;
using ScraperProject.ViewModels;
using System.Diagnostics;

namespace ScraperProject.Controllers;
public class HomeController : Controller
{
    private readonly IStoreService _storeService;
    private readonly ILaptopService _laptopService;

    public HomeController(IStoreService storeService, ILaptopService laptopService)
    {
        _storeService = storeService;
        _laptopService = laptopService;
    }

    public async Task<IActionResult> Index()
    {
        var vm = await _storeService.GetStoresWithLaptops();

        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> AddLaptop([FromBody] NewLaptopViewModel viewModel)
    {
        await _laptopService.AddLaptop(viewModel);
        return RedirectToAction("Index");
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteLaptop(int id)
    {
        await _laptopService.DeleteLaptop(id);
        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
