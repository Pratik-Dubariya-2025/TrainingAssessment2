namespace TrainingAssessment.Repository.Repository.IRepository;

public interface IUnitOfWork
{
    IRoleRepository RoleRepository { get; }
    IUserRepository UserRepository { get; }
    bool Save();
}
