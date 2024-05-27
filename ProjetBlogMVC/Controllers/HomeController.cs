using Microsoft.AspNetCore.Mvc;
using ViewModels;
using System.Diagnostics;
using IServices;

namespace ProjetBlogMVC.Controllers
{
    public class HomeController(
        ILogger<HomeController> logger,
        IArticleService articleService
        ) : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View(await articleService.GetAllAsync());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
