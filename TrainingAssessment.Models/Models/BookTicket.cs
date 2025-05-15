using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainingAssessment.Models.Models;

public class BookTicket
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [ForeignKey("User")]
    public int UserId { get; set; }
    [ForeignKey("Concert")]
    public int ConcertId { get; set; }
    public int TotalTicketBuy { get; set; }
    public decimal DiscountPrice { get; set; }
    public decimal TotalPrice { get; set; }
    public string Venue { get; set; } = null!;
    public DateTime ConcertTime { get; set; }
    public decimal TicketPrice { get; set; }
    public int TotalSeats { get; set; }
    public bool IsDeleted { get; set; } = false;
    public DateTime CreatedAt { get; set; }
    public virtual User User  { get; set; } = null!;
    public virtual Concert Concert  { get; set; } = null!;
}
