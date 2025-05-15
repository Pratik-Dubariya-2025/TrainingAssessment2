using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TrainingAssessment.Models.ViewModels;

namespace TrainingAssessment.Helper.JwtService;

public class TokenService : ITokenService
{
    private readonly IConfiguration _config;

    public TokenService(IConfiguration config)
    {
        _config = config;
    }

    private const double EXPIRY_DURATION = 30;
    public string BuildToken(string Username,string RoleId , bool rememberMe = false)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
#pragma warning disable CS8604 // Possible null reference argument.

        var securityKey = Encoding.ASCII.GetBytes(_config["Jwt:Key"]);
#pragma warning restore CS8604 // Possible null reference argument.


        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
                new[] {    
                    new Claim(ClaimTypes.Name, Username),
                    new Claim(ClaimTypes.Role, RoleId),
                    new Claim(ClaimTypes.NameIdentifier,
                    Guid.NewGuid().ToString())
                }
            ),
            Expires = rememberMe ? DateTime.UtcNow.AddDays(EXPIRY_DURATION) : DateTime.UtcNow.AddMinutes(EXPIRY_DURATION),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(securityKey),SecurityAlgorithms.HmacSha256Signature)
        };
        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public object IsTokenValid(string value)
    {
#pragma warning disable CS8604 // Possible null reference argument.

        byte[] mySecret = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
#pragma warning restore CS8604 // Possible null reference argument.

        SymmetricSecurityKey mySecurityKey = new(mySecret);
        JwtSecurityTokenHandler tokenHandler = new();

        tokenHandler.ValidateToken(value,new TokenValidationParameters {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(mySecret)
        }, out SecurityToken validatedToken);

        JwtSecurityToken jwtToken = (JwtSecurityToken)validatedToken;

        string username = jwtToken.Claims.First(u => u.Type == ClaimTypes.Name).Value;
        string roleId = jwtToken.Claims.First(u => u.Type == ClaimTypes.Role).Value;

        Object tokenData = new
        {
            roleId,
            username
        };

        return tokenData; 
    }
}
