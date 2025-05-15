using System.ComponentModel.DataAnnotations;

namespace TrainingAssessment.Models.ViewModels;

public class BookTicketViewModel
{
    public int? Id { get; set; }
    [Required(ErrorMessage = "User should be specify")]
    public int UserId { get; set; }
    [Required(ErrorMessage = "Concert should be specify")]
    public int ConcertId { get; set; }
    [Required(ErrorMessage = "Total ticket should be specify")]
    [Range(1,10,ErrorMessage = "Cannot buy more then 10 or less then 1")]
    public int TotalTicketBuy { get; set; }
    public decimal? DiscountPrice { get; set; } = 0;
    [Required(ErrorMessage = "Total price should be specify")]
    public decimal TotalPrice { get; set; }
    public decimal TicketPrice { get; set; }
    public string? Venue { get; set; }
    public string? Title { get; set; }
    public string? ArtistName { get; set; }
    public int? TotalSeats { get; set; }
    public int? AvailableSeats { get; set; }
    public string? Description { get; set; }
    public DateTime? ConcertTime { get; set; }
}
