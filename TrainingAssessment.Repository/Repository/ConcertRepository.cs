using TrainingAssessment.Models.Data;
using TrainingAssessment.Models.Models;
using TrainingAssessment.Models.ViewModels;
using TrainingAssessment.Repository.Repository.IRepository;

namespace TrainingAssessment.Repository.Repository;

public class ConcertRepository : Repository<Concert>, IConcertRepository
{
    public ConcertRepository(TrainingAssessmentDbContext dbContext) : base(dbContext)
    {}

    public List<ConcertViewModel> GetFilteredConcerts(DateTime? filterDate, string? search)
    {
        IQueryable<Concert> query = DbSet.Where(b => !b.IsDeleted);

        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(c => c.ArtistName.ToLower().Contains(search.ToLower()) || 
                c.Venue.ToLower().Contains(search.ToLower()));
        }

        if (filterDate != null && filterDate?.Date > DateTime.Now.Date)
        {
            filterDate ??= DateTime.MinValue.Date;
            Console.WriteLine(filterDate);
            Console.WriteLine(query.Select(c => c.ConcertTime).FirstOrDefault().Date);
            query = query.Where(c => filterDate < c.ConcertTime.Date);
        }

        List<ConcertViewModel>? concerts = query
            .Select(c => new ConcertViewModel{
                    Id = c.Id,
                    Title = c.Title,
                    ConcertTime = c.ConcertTime,
                    TicketPrice = c.TicketPrice,
                    TotalSeats = c.TotalSeats,
                    ArtistName = c.ArtistName,
                    AvailableSeats = c.AvailableSeats,
                    Venue = c.Venue,
                    Description = c.Description
                }).ToList() ?? new();

        return concerts;
    }
}
