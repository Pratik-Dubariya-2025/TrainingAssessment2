using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainingAssessment.Models.Models;

public class Concert
{
    
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string ArtistName { get; set; } = null!;
    public string Venue { get; set; } = null!;
    public DateTime ConcertTime { get; set; }
    public decimal TicketPrice { get; set; }
    public int TotalSeats { get; set; }
    public int AvailableSeats { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public virtual ICollection<BookTicket> BookTickets { get; set; } = new List<BookTicket>();
}