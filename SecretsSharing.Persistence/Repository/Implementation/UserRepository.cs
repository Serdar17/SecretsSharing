using Domain.Models;
using Microsoft.EntityFrameworkCore;
using SecretsSharing.Persistence.Repository.Interfaces;

namespace SecretsSharing.Persistence.Repository.Implementation;

/// <summary>
/// a class that implements the IUserRepository interface
/// </summary>
public class UserRepository : IUserRepository
{
    private readonly IApplicationDbContext _dbContext;

    public UserRepository(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// method for adding a user to the database
    /// </summary>
    /// <param name="applicationUser">User object</param>
    /// <param name="cancellationToken">Cancellation token with default value</param>
    public async Task AddAsync(ApplicationUser applicationUser, CancellationToken cancellationToken)
    {
        await _dbContext.Users.AddAsync(applicationUser, cancellationToken);
        
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// method for searching for a user by email
    /// </summary>
    /// <param name="email">User's email</param>
    /// <param name="cancellationToken">Cancellation token with default value</param>
    /// <returns>Returns the found user object</returns>
    public async Task<ApplicationUser?> FindByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Users
            .Where(x => x.Email == email)
            .FirstOrDefaultAsync(cancellationToken);
    }

    /// <summary>
    /// method for searching for a user by unique user id
    /// </summary>
    /// <param name="id">Unique user id</param>
    /// <param name="cancellationToken">Cancellation token with default value</param>
    /// <returns>Returns the found user object</returns>
    public async Task<ApplicationUser?> GetUserByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Users
            .Where(x => x.Id.Equals(id))
            .Include(x => x.Files)
            .FirstOrDefaultAsync(cancellationToken);
    }

    /// <summary>
    /// method for updating user data in the database
    /// </summary>
    /// <param name="user">User object</param>
    /// <param name="cancellationToken">Cancellation token with default value</param>
    public async Task UpdateAsync(ApplicationUser user, CancellationToken cancellationToken = default)
    {
        _dbContext.Users.Update(user);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}