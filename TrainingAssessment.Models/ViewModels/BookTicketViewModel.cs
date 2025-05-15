using System.ComponentModel.DataAnnotations;

namespace TrainingAssessment.Models.ViewModels;

public class BookTicketViewModel
{
    public int? Id { get; set; }
    [Required(ErrorMessage = "User name should be specify")]
    public int UserId { get; set; }
    public int ConcertId { get; set; }
    public int TotalTicketBuy { get; set; }
    public decimal DiscountPrice { get; set; }
    public decimal TotalPrice { get; set; }
    public string Venue { get; set; } = null!;
    public DateTime ConcertTime { get; set; }
    public decimal TicketPrice { get; set; }
    public int TotalSeats { get; set; }
}
