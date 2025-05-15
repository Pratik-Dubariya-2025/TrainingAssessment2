using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrainingAssessment.Models.ViewModels;
using TrainingAssessment.Service.Service.IService;

namespace TrainingAssessment.Web.Controllers;

[Authorize]
public class HomeController : Controller
{

    private readonly IHomeService homeService;

    public HomeController(IHomeService homeService)
    {
        this.homeService = homeService;
    }

    public IActionResult Index()
    {
        return View();
    }

    [Authorize(Roles = "1")]
    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult ConcertDetails(int Id)
    {
        ConcertViewModel? concert = homeService.GetConcertViewModelById(Id);
        if (concert == null)
        {
            return RedirectToAction("Index", "Home");
        }
        return View(concert);
    }

    [Authorize(Roles = "1")]
    [HttpPost]
    public IActionResult AddEditConert(ConcertViewModel concert)
    {
        if (!ModelState.IsValid)
        {
            var allErrors = ModelState.Values.SelectMany(v => v.Errors);
            return Json(new { success = false, allErrors, message = "All fields are required" });
        }
        if (concert.Id == null || concert.Id <= 0)
        {
            (bool success, string message) = homeService.Add(concert);
            return Json(new { success, message });
        }
        (bool Success, string Message) = homeService.Edit(concert);
        return Json(new { Success, Message });
    }

    [Authorize(Roles = "2")]
    [HttpPost]
    public IActionResult BuyConcertTicket(BookTicketViewModel bookTicketView)
    {
        if (!ModelState.IsValid)
        {
            var allErrors = ModelState.Values.SelectMany(v => v.Errors);
            return Json(new { success = false, allErrors, message = "All fields are required" });
        }
        (bool success, string message) = homeService.BuyConcertTicket(bookTicketView);
        return Json(new { success, message });
    }

    [Authorize(Roles = "1")]
    public IActionResult OpenAddEditConcertModal(int Id)
    {
        ConcertViewModel? concert = new();
        if (Id <= 0)
        {
            return PartialView("_AddEditConcert", concert);
        }
        concert = homeService.GetConcertViewModelById(Id);
        return PartialView("_AddEditConcert", concert);
    }


    public IActionResult OpenConcertDetailModal(int Id)
    {
        if (Id <= 0)
        {
            return Json( new { success = false, messgae = "Id not found" } );
        }
        ConcertViewModel? concert = homeService.GetConcertViewModelById(Id);
        if (concert == null)
        {
            return Json( new { success = false, messgae = "Concert not found" } );
        }
        return PartialView("_ConcertDetails", concert);
    }

    [Authorize(Roles = "2")]
    public IActionResult OpenBuyTicketModal(int Id)
    {
        if (Id <= 0)
        {
            return Json( new { success = false, messgae = "Id not found" } );
        }
        BookTicketViewModel? bookTicketViewModel = homeService.GetBookTicketViewModelByConcertId(Id);
        return PartialView("_BuyTicket", bookTicketViewModel);
    }

    public IActionResult GetAllConcerts()
    {
        return PartialView("_ListAllConcert", homeService.GetAllConcerts());
    }
}
