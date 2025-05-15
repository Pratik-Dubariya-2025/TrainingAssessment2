using TrainingAssessment.Models.ViewModels;

namespace TrainingAssessment.Service.Service.IService;

public interface IHomeService
{
    (bool, string) Add(ConcertViewModel concert);
    (bool, string) Edit(ConcertViewModel concert);
    (bool, string) BuyConcertTicket(BookTicketViewModel bookTicketViewModel);
    ConcertViewModel? GetConcertViewModelById(int Id);
    BookTicketViewModel? GetBookTicketViewModelByConcertId(int concertId);
    List<ConcertViewModel> GetAllConcerts(DateTime? filterDate, string? search);
    List<BookTicketViewModel> GetAllBooking(string filter);
}
