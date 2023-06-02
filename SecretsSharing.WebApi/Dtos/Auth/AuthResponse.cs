namespace WebApi.Dtos.Auth;

/// <summary>
/// Model about successful authorization
/// </summary>
public class AuthResponse
{
    public AuthResponse(Guid userId, string email, string accessToken, string refreshToken)
    {
        UserId = userId;
        Email = email;
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }
    
    /// <summary>
    /// Unique user id
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// User's email
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// User access token
    /// </summary>
    public string AccessToken { get; set; }

    /// <summary>
    /// User refresh token
    /// </summary>
    public string RefreshToken { get; set; }
}