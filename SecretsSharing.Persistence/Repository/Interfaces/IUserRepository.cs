using Domain.Models;

namespace SecretsSharing.Persistence.Repository.Interfaces;

/// <summary>
/// interface for executing database queries
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// method for adding a user to the database
    /// </summary>
    /// <param name="applicationUser">User object</param>
    /// <param name="cancellationToken">Cancellation token with default value</param>
    /// <returns></returns>
    Task AddAsync(ApplicationUser applicationUser, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// method for searching for a user by email
    /// </summary>
    /// <param name="email">User's email</param>
    /// <param name="cancellationToken">Cancellation token with default value</param>
    /// <returns>Returns the found user object</returns>
    Task<ApplicationUser?> FindByEmailAsync(string email, CancellationToken cancellationToken = default);

    /// <summary>
    /// method for searching for a user by unique user id
    /// </summary>
    /// <param name="id">Unique user id</param>
    /// <param name="cancellationToken">Cancellation token with default value</param>
    /// <returns>Returns the found user object</returns>
    Task<ApplicationUser?> GetUserByIdAsync(Guid id, CancellationToken cancellationToken = default); 
    
    /// <summary>
    /// method for updating user data in the database
    /// </summary>
    /// <param name="user">User object</param>
    /// <param name="cancellationToken">Cancellation token with default value</param>
    /// <returns></returns>
    Task UpdateAsync(ApplicationUser user, CancellationToken cancellationToken = default);
}