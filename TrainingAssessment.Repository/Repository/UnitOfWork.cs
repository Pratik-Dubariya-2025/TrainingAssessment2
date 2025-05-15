using TrainingAssessment.Models.Data;
using TrainingAssessment.Repository.Repository.IRepository;

namespace TrainingAssessment.Repository.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly TrainingAssessmentDbContext dbContext;
    private IRoleRepository? roleRepository;
    private IUserRepository? userRepository;

    public UnitOfWork(TrainingAssessmentDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public IRoleRepository RoleRepository { get { return roleRepository ??= new RoleRepository(dbContext); } }
    public IUserRepository UserRepository { get { return userRepository ??= new UserRepository(dbContext); } }
    public bool Save()
    {
        try
        {
            dbContext.SaveChanges();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
