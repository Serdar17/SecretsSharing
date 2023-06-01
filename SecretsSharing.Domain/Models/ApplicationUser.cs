using Domain.Primitives;

namespace Domain.Models;

public class ApplicationUser : BaseEntity<Guid>
{
    public string Email { get; set; }

    public string PasswordHash { get; set; }
    
    public string? RefreshToken { get; set; }
    
    public DateTime RefreshTokenExpiryTime { get; set; }

    public List<File> Files { get; set; } = new();
}