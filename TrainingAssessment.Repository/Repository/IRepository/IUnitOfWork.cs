namespace TrainingAssessment.Repository.Repository.IRepository;

public interface IUnitOfWork
{
    IRoleRepository RoleRepository { get; }
    IUserRepository UserRepository { get; }
    IConcertRepository ConcertRepository { get; }
    IBookTicketRepository BookTicketRepository { get; }
    bool Save();
}
