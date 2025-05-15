using System.ComponentModel.DataAnnotations;

namespace TrainingAssessment.Models.ViewModels;

public class ConcertViewModel
{
    public int? Id { get; set; }
    [Required]
    public string Title { get; set; } = null!;
    [Required(ErrorMessage = "Artist name should be specify")]
    public string ArtistName { get; set; } = null!;
    [Required(ErrorMessage = "Venue cannot be empty")]
    public string Venue { get; set; } = null!;
    [Required(ErrorMessage = "Concert time cannot be empty")]
    public DateTime ConcertTime { get; set; }
    [Required(ErrorMessage = "Ticket price cannot be empty")]
    public decimal TicketPrice { get; set; }
    [Required(ErrorMessage = "Total seats cannot be empty")]
    public int TotalSeats { get; set; }
    public int? AvailableSeats { get; set; }
}
