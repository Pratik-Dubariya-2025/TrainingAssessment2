using TrainingAssessment.Models.Data;
using TrainingAssessment.Models.Models;
using TrainingAssessment.Repository.Repository.IRepository;

namespace TrainingAssessment.Repository.Repository;

public class BookTicketRepository : Repository<BookTicket>, IBookTicketRepository
{
    public BookTicketRepository(TrainingAssessmentDbContext dbContext) : base(dbContext)
    {}
}
