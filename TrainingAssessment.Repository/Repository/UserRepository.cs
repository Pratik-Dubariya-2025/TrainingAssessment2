using TrainingAssessment.Models.Data;
using TrainingAssessment.Models.Models;
using TrainingAssessment.Repository.Repository.IRepository;

namespace TrainingAssessment.Repository.Repository;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(TrainingAssessmentDbContext dbContext) : base(dbContext)
    {}
}
