using TrainingAssessment.Models.ViewModels;

namespace TrainingAssessment.Service.Service.IService;

public interface ILoginService
{
    int GetUserId();
    Task<(bool, string)> Login(LoginViewModel loginViewModel);
    (bool, string) SignUp(UserViewModel userView);
    bool VerifyPassword(string? password, string hashPassword);
    string HashPassword(string? password);
}
