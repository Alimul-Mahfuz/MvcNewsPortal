using Microsoft.AspNetCore.Mvc;
using NewsPortal.Web.Filters;

namespace NewsPortal.Web.Controllers;
[SessionAuthorized]
public class DashboardController:Controller
{
    [HttpGet]
    public IActionResult Index() => View();
}