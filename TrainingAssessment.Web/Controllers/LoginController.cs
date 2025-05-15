using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrainingAssessment.Models.ViewModels;
using TrainingAssessment.Service.Service.IService;

namespace TrainingAssessment.Web.Controllers;

public class LoginController : Controller
{

    private readonly ILoginService loginService;

    public LoginController(ILoginService loginService)
    {
        this.loginService = loginService;
    }

    [HttpGet]
    public IActionResult Index(string? ReturnUrl)
    {
        ViewData["ReturnUrl"] = ReturnUrl;
        if (User.Identity != null && User.Identity.IsAuthenticated)
        {
            TempData["error"] = $"{User.Identity.Name} is already login";
            return RedirectToAction("AccessDenied", "Account");
        }
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(string? returnurl, LoginViewModel loginViewModel)
    {
        if (!ModelState.IsValid)
        {
            TempData["error"] = "All field are required";
            return View(loginViewModel);
        }
        (bool success, string message) = await loginService.Login(loginViewModel);
        TempData[success ? "success" : "error"] = message;
        if (!success)
        {
            return View(loginViewModel);
        }
        if (!string.IsNullOrEmpty(returnurl) && Url.IsLocalUrl(returnurl))
        {
            return Redirect(returnurl);
        }
        return RedirectToAction("Index","Home");
    }

    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        HttpContext.Session.Clear();
        TempData["success"] = "Logout successful";
        return RedirectToAction("Index", "Login");
    }

}
