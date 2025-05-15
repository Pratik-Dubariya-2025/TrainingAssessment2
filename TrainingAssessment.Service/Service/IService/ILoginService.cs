using TrainingAssessment.Models.ViewModels;

namespace TrainingAssessment.Service.Service.IService;

public interface ILoginService
{
    Task<(bool, string)> Login(LoginViewModel loginViewModel);
}
