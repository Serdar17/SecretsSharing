namespace WebApi.Dtos.Auth;

public class AuthResponse
{
    public AuthResponse(Guid userId, string email, string accessToken, string refreshToken)
    {
        UserId = userId;
        Email = email;
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }
    public Guid UserId { get; set; }

    public string Email { get; set; }

    public string AccessToken { get; set; }

    public string RefreshToken { get; set; }
}