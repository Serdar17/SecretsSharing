using Domain.Primitives;

namespace Domain.Models;

/// <summary>
/// User data access model
/// </summary>
public class ApplicationUser : BaseEntity<Guid>
{
    /// <summary>
    /// User's email
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// User hash password
    /// </summary>
    public string PasswordHash { get; set; }
    
    /// <summary>
    /// User refresh token
    /// </summary>
    public string? RefreshToken { get; set; }
    
    /// <summary>
    /// User refresh token expiry time
    /// </summary>
    public DateTime RefreshTokenExpiryTime { get; set; }

    /// <summary>
    /// List of user files
    /// </summary>
    public List<FileDetails> Files { get; set; } = new();
}