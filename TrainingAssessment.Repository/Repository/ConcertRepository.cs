using TrainingAssessment.Models.Data;
using TrainingAssessment.Models.Models;
using TrainingAssessment.Repository.Repository.IRepository;

namespace TrainingAssessment.Repository.Repository;

public class ConcertRepository : Repository<Concert>, IConcertRepository
{
    public ConcertRepository(TrainingAssessmentDbContext dbContext) : base(dbContext)
    {}
}
