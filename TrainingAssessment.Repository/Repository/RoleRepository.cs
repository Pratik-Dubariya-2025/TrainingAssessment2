using TrainingAssessment.Models.Data;
using TrainingAssessment.Models.Models;
using TrainingAssessment.Repository.Repository.IRepository;

namespace TrainingAssessment.Repository.Repository;

public class RoleRepository : Repository<Role>, IRoleRepository
{
    public RoleRepository(TrainingAssessmentDbContext dbContext) : base(dbContext)
    {}
}
