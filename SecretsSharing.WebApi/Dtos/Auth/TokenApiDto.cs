namespace WebApi.Dtos.Auth;

/// <summary>
/// Model with access and refresh tokens
/// </summary>
public class TokenApiDto
{
    /// <summary>
    /// User access token
    /// </summary>
    public string AccessToken { get; set; }

    /// <summary>
    /// User refresh token
    /// </summary>
    public string RefreshToken { get; set; }
}