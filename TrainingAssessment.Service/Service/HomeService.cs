using System.Security.Claims;
using System.Threading.Tasks;
using BCrypt.Net;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using TrainingAssessment.Models.Models;
using TrainingAssessment.Models.ViewModels;
using TrainingAssessment.Repository.Repository.IRepository;
using TrainingAssessment.Service.Service.IService;

namespace TrainingAssessment.Service.Service;

public class HomeService : IHomeService
{
    private readonly IUnitOfWork unitOfWork;
    private readonly ILoginService loginService;
    public HomeService(IUnitOfWork unitOfWork, ILoginService loginService)
    {
        this.unitOfWork = unitOfWork;
        this.loginService = loginService;
    }
    public (bool, string) Add(ConcertViewModel concert)
    {
        try
        {
            if (concert.ConcertTime.Date <= DateTime.Now.Date)
            {
                return (false, "Concert date must be from tommorrow");
            }
            Concert newConcert = new()
            {
                Title = concert.Title,
                ArtistName = concert.ArtistName,
                Venue = concert.Venue,
                ConcertTime = concert.ConcertTime,
                TicketPrice = concert.TicketPrice,
                TotalSeats = concert.TotalSeats,
                AvailableSeats = concert.TotalSeats,
                Description = concert.Description
            };

            unitOfWork.ConcertRepository.Add(newConcert);
            bool isSaved = unitOfWork.Save();

            if(!isSaved) { return (false, "Error while saving concert"); }
            return (true, "Concert is added");
        }
        catch (Exception ex)
        {
            return (false, ex.Message);
            throw;
        }
    }
    public (bool, string) Edit(ConcertViewModel concert)
    {
        try
        {
            Concert? existConcert = unitOfWork.ConcertRepository.GetFirstOrDefault(c => c.Id == concert.Id && !c.IsDeleted && c.ConcertTime <= DateTime.Now);

            if (existConcert == null)
            {
                return (false, "Concert not found");
            }

            if (concert.ConcertTime.Date <= DateTime.Now.Date)
            {
                return (false, "Concert time cannot be less then current time");
            }
            if (concert.TotalSeats <= (existConcert.TotalSeats - existConcert.AvailableSeats))
            {
                return (false, "Total seats cannot be less or equal to booked seats");
            }
            existConcert.Title = concert.Title;
            existConcert.ArtistName = concert.ArtistName;
            existConcert.Venue = concert.Venue;
            existConcert.ConcertTime = concert.ConcertTime;
            existConcert.TicketPrice = concert.TicketPrice;
            existConcert.TotalSeats = concert.TotalSeats;
            existConcert.AvailableSeats = concert.TotalSeats - existConcert.AvailableSeats;
            existConcert.Description = concert.Description;

            bool isSaved = unitOfWork.Save();

            if(!isSaved) { return (false, "Error while updating concert"); }
            return (true, "Concert is added");
        }
        catch (Exception ex)
        {
            return (false, ex.Message);
            throw;
        }
    }

    public (bool, string) BuyConcertTicket(BookTicketViewModel bookTicketViewModel)
    {
        try
        {
            int UserId = loginService.GetUserId();
            if (UserId == 0)
            {
                return (false, "Unauthorized User");
            }
            decimal totalTicketPrice = 0;
            Concert? concert = unitOfWork.ConcertRepository
                .GetFirstOrDefault(c => c.Id == bookTicketViewModel.ConcertId && !c.IsDeleted && c.ConcertTime.Date > DateTime.Now.Date);
            
            if (concert == null)
            {
                return (false, "Concert not found");
            }

            if (bookTicketViewModel.TotalTicketBuy > concert.AvailableSeats)
            {
                return (false, "Total tickets are more then available tickets");
            }
            totalTicketPrice = bookTicketViewModel.TotalTicketBuy > 5 
                ? bookTicketViewModel.TotalTicketBuy * concert.TicketPrice * (decimal)0.80
                : bookTicketViewModel.TotalTicketBuy * concert.TicketPrice;

            decimal discountPrice = bookTicketViewModel.TotalTicketBuy > 5 
                ? bookTicketViewModel.TotalTicketBuy * concert.TicketPrice * (decimal)0.20
                : 0;
            
            BookTicket bookTicket = new()
            {
                ConcertId = concert.Id,
                UserId = UserId,
                TotalTicketBuy = bookTicketViewModel.TotalTicketBuy,
                TotalPrice = totalTicketPrice,
                DiscountPrice = discountPrice
            };

            concert.AvailableSeats = concert.TotalSeats - bookTicket.TotalTicketBuy;
            unitOfWork.BookTicketRepository.Add(bookTicket);
            unitOfWork.Save();
            return (true, "Ticket bought successfully");
        }
        catch (Exception ex)
        {
            return (false, ex.Message);
        }
    }

    public ConcertViewModel? GetConcertViewModelById(int Id)
    {
        ConcertViewModel? concert = unitOfWork.ConcertRepository.GetFirstOrDefault(c => new ConcertViewModel{
            Id = c.Id,
            Title = c.Title,
            ConcertTime = c.ConcertTime,
            TicketPrice = c.TicketPrice,
            TotalSeats = c.TotalSeats,
            ArtistName = c.ArtistName,
            AvailableSeats = c.AvailableSeats,
            Venue = c.Venue,
            Description = c.Description
        }, c => !c.IsDeleted && c.Id == Id && c.ConcertTime.Date > DateTime.Now.Date);
        
        return concert;
    }

    public BookTicketViewModel? GetBookTicketViewModelByConcertId(int concertId)
    {
        int UserId = loginService.GetUserId();
        BookTicketViewModel? bookTicketViewModel = unitOfWork.ConcertRepository.GetFirstOrDefault(c => new BookTicketViewModel{
            ConcertId = c.Id,
            Title = c.Title,
            ConcertTime = c.ConcertTime,
            TicketPrice = c.TicketPrice,
            TotalSeats = c.TotalSeats,
            ArtistName = c.ArtistName,
            AvailableSeats = c.AvailableSeats,
            Description = c.Description,
            Venue = c.Venue,
            UserId = UserId,
            TotalTicketBuy = 1
        }, c => c.Id == concertId && !c.IsDeleted && c.ConcertTime.Date > DateTime.Now.Date);

        return bookTicketViewModel;
    }
    public List<ConcertViewModel> GetAllConcerts(DateTime? filterDate, string? search)
    {   
        return unitOfWork.ConcertRepository.GetFilteredConcerts(filterDate, search);
    }

    public List<BookTicketViewModel> GetAllBooking(string filter)
    {
        List<BookTicketViewModel> bookTickets = new();
        int UserId = loginService.GetUserId();
        if (UserId == 0)
        {
            return bookTickets;
        }

        bookTickets = filter switch
        {
            "past_bookings" => unitOfWork.BookTicketRepository
                .GetAll(b => new BookTicketViewModel
                {
                    Title = b.Concert.Title,
                    ConcertTime = b.Concert.ConcertTime,
                    TicketPrice = b.Concert.TicketPrice,
                    ArtistName = b.Concert.ArtistName,
                    AvailableSeats = b.Concert.AvailableSeats,
                    Description = b.Concert.Description,
                    Venue = b.Concert.Venue,
                    UserId = UserId,
                    TotalTicketBuy = b.TotalTicketBuy,
                    TotalPrice = b.TotalPrice,
                    DiscountPrice = b.DiscountPrice
                }, b => b.UserId == UserId && !b.IsDeleted && b.Concert.ConcertTime.Date <= DateTime.Now),
            "upcoming_bookings" => unitOfWork.BookTicketRepository
                .GetAll(b => new BookTicketViewModel
                {
                    Title = b.Concert.Title,
                    ConcertTime = b.Concert.ConcertTime,
                    TicketPrice = b.Concert.TicketPrice,
                    ArtistName = b.Concert.ArtistName,
                    AvailableSeats = b.Concert.AvailableSeats,
                    Description = b.Concert.Description,
                    Venue = b.Concert.Venue,
                    UserId = UserId,
                    TotalTicketBuy = b.TotalTicketBuy,
                    TotalPrice = b.TotalPrice,
                    DiscountPrice = b.DiscountPrice
                }, b => b.UserId == UserId && !b.IsDeleted && b.Concert.ConcertTime.Date > DateTime.Now),
            _ => unitOfWork.BookTicketRepository
                .GetAll(b => new BookTicketViewModel
                {
                    Title = b.Concert.Title,
                    ConcertTime = b.Concert.ConcertTime,
                    TicketPrice = b.Concert.TicketPrice,
                    ArtistName = b.Concert.ArtistName,
                    AvailableSeats = b.Concert.AvailableSeats,
                    Description = b.Concert.Description,
                    Venue = b.Concert.Venue,
                    UserId = UserId,
                    TotalTicketBuy = b.TotalTicketBuy,
                    TotalPrice = b.TotalPrice,
                    DiscountPrice = b.DiscountPrice
                }, b => b.UserId == UserId && !b.IsDeleted),
        };
        return bookTickets;
    }

}
