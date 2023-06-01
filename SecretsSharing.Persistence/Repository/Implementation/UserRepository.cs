using Domain.Models;
using Microsoft.EntityFrameworkCore;
using SecretsSharing.Persistence.Interfaces;
using SecretsSharing.Persistence.Repository.Interfaces;

namespace SecretsSharing.Persistence.Repository.Implementation;

public class UserRepository : IUserRepository
{
    private readonly IApplicationDbContext _dbContext;

    public UserRepository(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(ApplicationUser applicationUser, CancellationToken cancellationToken)
    {
        await _dbContext.Users.AddAsync(applicationUser, cancellationToken);
        
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<ApplicationUser?> FindByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Users
            .Where(x => x.Email == email)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task UpdateAsync(ApplicationUser user, CancellationToken cancellationToken = default)
    {
        _dbContext.Users.Update(user);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}