using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NewsPortal.Application.Services;
using NewsPortal.Web.Models;

namespace NewsPortal.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ArticleService _articleService;

    public HomeController(ILogger<HomeController> logger,ArticleService articleService)
    {
        _logger = logger;
        _articleService = articleService;
    }

    public IActionResult Index()
    {
        var articles =  _articleService.GetArticlesByCategory().ToList();
        return View(articles);
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
