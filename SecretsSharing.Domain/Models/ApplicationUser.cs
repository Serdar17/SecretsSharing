using Domain.Primitives;

namespace Domain.Models;

public class ApplicationUser : BaseEntity<Guid>
{
    public string Email { get; set; }

    public string PasswordHash { get; set; }
    
    public string? RefreshToken { get; set; }
    
    public DateTime RefreshTokenExpiryTime { get; set; }

    public List<FileDetails> Files { get; set; } = new();
}