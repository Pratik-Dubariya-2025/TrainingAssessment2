using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TrainingAssessment.Models.ViewModels;

public class ConcertViewModel
{
    public int? Id { get; set; } = 0;
    [Required]
    public string Title { get; set; } = null!;
    [Required(ErrorMessage = "Artist name should be specify")]
    public string ArtistName { get; set; } = null!;
    [Required(ErrorMessage = "Venue cannot be empty")]
    public string Venue { get; set; } = null!;
    [Required(ErrorMessage = "Description cannot be empty")]
    public string Description { get; set; } = null!;
    [Required(ErrorMessage = "Concert time cannot be empty")]
    [DataType(DataType.DateTime)]
    [MyDateValidation(ErrorMessage = "Concert time must from any from tommorrow")]
    public DateTime ConcertTime { get; set; } = DateTime.MinValue;
    [Required(ErrorMessage = "Ticket price cannot be empty")]
    [Range(typeof(decimal), "0.01", "100000", ErrorMessage = "Ticket must be between 0 and 100000.")]
    public decimal TicketPrice { get; set; }
    [Required(ErrorMessage = "Total seats cannot be empty")]
    [Range(1, int.MaxValue, ErrorMessage = "Total seats must have valid value.")]
    public int TotalSeats { get; set; }
    public int? AvailableSeats { get; set; } = 0;
}

public class MyDateValidation: ValidationAttribute
{
    public override bool IsValid(object? value)
    { 
        DateTime dateEntered = value == null ? DateTime.MinValue : (DateTime)value;
        if (dateEntered <= DateTime.Today) {
            return false;
        }
        return true;
    }
}