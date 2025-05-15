using Microsoft.AspNetCore.Mvc;

namespace TrainingAssessment.Web.Controllers;

public class AccountController : Controller
{
    public IActionResult AccessDenied()
    {
        return View();
    }
}
