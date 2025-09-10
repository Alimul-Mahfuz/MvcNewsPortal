using Microsoft.AspNetCore.Mvc;

namespace NewsPortal.Web.Controllers;

public class DashboardController:Controller
{
    [HttpGet]
    public IActionResult Index() => View();
}