using TrainingAssessment.Models.ViewModels;

namespace TrainingAssessment.Helper.JwtService;

public interface ITokenService
{
    string BuildToken(string Username,string RoleId , bool rememberMe = false);
    object IsTokenValid(string value);
}
