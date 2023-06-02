using System.Security.Claims;

namespace WebApi.Services.Interfaces;

/// <summary>
/// interface that represents abstraction over interaction with jwt tokens
/// </summary>
public interface ITokenService
{
    /// <summary>
    /// method for generating a user access token
    /// </summary>
    /// <param name="claims">list of user claims</param>
    /// <returns>new generated token as string</returns>
    string GenerateToken(List<Claim> claims);

    /// <summary>
    /// method for generating a user refresh token
    /// </summary>
    /// <returns>new generated refresh token as string</returns>
    string GenerateRefreshToken();
    
    /// <summary>
    /// method for getting principal from expired token
    /// </summary>
    /// <param name="token">user access token</param>
    /// <returns>Claims principal object</returns>
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}