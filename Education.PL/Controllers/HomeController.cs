using Education.BL.Services.Abstractions;
using Education.PL.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Education.PL.Controllers;

public class HomeController : Controller
{
    readonly INewsService _newsService;

    public HomeController(INewsService newsService)
    {
        _newsService = newsService;
    }

    public async Task<IActionResult> Index()
    {
        HomeVM VM = new()
        {
            News = await _newsService.GetAllViewItemsAsync(),
        };

        return View(VM);
    }
}
