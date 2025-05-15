using TrainingAssessment.Models.Data;
using TrainingAssessment.Repository.Repository.IRepository;

namespace TrainingAssessment.Repository.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly TrainingAssessmentDbContext dbContext;
    private IRoleRepository? roleRepository;
    private IUserRepository? userRepository;
    private IConcertRepository? concertRepository;
    private IBookTicketRepository? bookTicketRepository;

    public UnitOfWork(TrainingAssessmentDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public IRoleRepository RoleRepository { get { return roleRepository ??= new RoleRepository(dbContext); } }
    public IUserRepository UserRepository { get { return userRepository ??= new UserRepository(dbContext); } }
    public IConcertRepository ConcertRepository { get { return concertRepository ??= new ConcertRepository(dbContext); } }
    public IBookTicketRepository BookTicketRepository { get { return bookTicketRepository ??= new BookTicketRepository(dbContext); } }
    public bool Save()
    {
        try
        {
            dbContext.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            throw ex;
            return false;
        }
    }
}
