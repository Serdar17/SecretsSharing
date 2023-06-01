using System.Security.Claims;

namespace WebApi.Services.Interfaces;

public interface ITokenService
{
    string GenerateToken(List<Claim> claims);

    string GenerateRefreshToken();
    
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}