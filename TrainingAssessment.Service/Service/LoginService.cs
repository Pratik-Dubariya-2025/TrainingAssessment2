using System.Security.Claims;
using System.Threading.Tasks;
using BCrypt.Net;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using TrainingAssessment.Models.Models;
using TrainingAssessment.Models.ViewModels;
using TrainingAssessment.Repository.Repository.IRepository;
using TrainingAssessment.Service.Service.IService;

namespace TrainingAssessment.Service.Service;

public class LoginService : ILoginService
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IHttpContextAccessor httpContextAccessor;
    public LoginService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
    {
        this.unitOfWork = unitOfWork;
        this.httpContextAccessor = httpContextAccessor;
    }

    public async Task<(bool, string)> Login(LoginViewModel loginViewModel)
    {
        User? user = unitOfWork.UserRepository.GetFirstOrDefault(s => s.Email == loginViewModel.Email, s => s.Role);
        if (user == null)
        {
            return (false, "User not found");
        }

        if (!VerifyPassword(loginViewModel.Password, user.Password))
        {
            return (false, "Incorrect Password");
        }

        List<Claim> claims = new()
        {
            new(ClaimTypes.Name, user.Username),
            new(ClaimTypes.Email, user.Email),
            new("Id", user.Id.ToString()),
            new(ClaimTypes.Role, user.RoleId.ToString())
        };
        bool IsSessionSet = await SetSession(claims, loginViewModel.RememberMe);
        if (!IsSessionSet)
        {
            return (false, "Error while setting session");
        }
        return (true, $"Welcome back, {user.Username}");
    }

    public (bool, string) SignUp(UserViewModel userView)
    {
        User? user = unitOfWork.UserRepository
            .GetFirstOrDefault(u => u.Email.ToLower() == userView.Email.ToLower());
        
        if (user != null)
        {
            return (false, "Email already exist");
        }

        user = unitOfWork.UserRepository
            .GetFirstOrDefault(u => u.Username == userView.Username);
        
        if (user != null)
        {
            return (false, "Username already exist");
        }

        User newUser = new()
        {
            FirstName = userView.FirstName,
            LastName = userView.LastName,
            Username = userView.Username,
            Email = userView.Email.ToLower(),
            RoleId = 2,
            Password = HashPassword(userView.Password)
        };

        unitOfWork.UserRepository.Add(newUser);
        unitOfWork.Save();

        return (true, "Signup successful, Signup to buy tickets");
    }

    public int GetUserId()
    {
        int UserId =  int.Parse(httpContextAccessor?.HttpContext?.User?.FindFirst("Id")?.Value ?? "0");
        return UserId;
    }

    public bool VerifyPassword(string? password, string hashPassword)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashPassword);
    }

    public string HashPassword(string? password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public async Task<bool> SetSession(List<Claim> claims, bool RememberMe)
    {
        try
        {
            HttpContext? httpContext = httpContextAccessor.HttpContext;
            if (claims == null || httpContext == null)
            {
                return false;
            }
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, new AuthenticationProperties
            {
                IsPersistent = RememberMe,
                ExpiresUtc = RememberMe ? DateTime.UtcNow.AddDays(30) : DateTime.UtcNow.AddHours(1)
            });
            return true;
        }
        catch (Exception)
        {
            return false;
            throw;
        }
    }
}
