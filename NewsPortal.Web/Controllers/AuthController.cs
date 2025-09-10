using Microsoft.AspNetCore.Mvc;
using NewsPortal.Application.Services;
using NewsPortal.Domain.Entities;
using NewsPortal.Web.ViewModels;

namespace NewsPortal.Web.Controllers;

public class AuthController:Controller
{
    protected readonly AuthService _authService;
    public AuthController(AuthService authService)
    {
        _authService = authService;
    }
    [HttpGet]
    public IActionResult AdminLogin()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AdminLogin(AdminLoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user= await _authService.Login(model.Email, model.Password);
            if (user != null && (user.Role==UserRole.Admin || user.Role==UserRole.Editor))
            {
                HttpContext.Session.SetInt32("UserId", user.Id);
                HttpContext.Session.SetString("UserName", user.Username);
                HttpContext.Session.SetString("UserEmail", user.Email);
                HttpContext.Session.SetString("UserFullName", user.FullName);
                HttpContext.Session.SetString("UserRole", user.Role.ToString());
                return RedirectToAction("Index", "Dashboard");
                
            }
            ModelState.AddModelError("", "Invalid email or password");
        }
        return View();
    }
    
    [HttpPost]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
    }
}