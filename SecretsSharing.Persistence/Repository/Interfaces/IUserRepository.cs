using Domain.Models;

namespace SecretsSharing.Persistence.Repository.Interfaces;

public interface IUserRepository
{
    Task AddAsync(ApplicationUser applicationUser, CancellationToken cancellationToken = default);
    
    Task<ApplicationUser?> FindByEmailAsync(string email, CancellationToken cancellationToken = default);
    
    Task UpdateAsync(ApplicationUser user, CancellationToken cancellationToken = default);
}