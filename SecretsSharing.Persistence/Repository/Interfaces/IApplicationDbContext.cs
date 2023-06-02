using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace SecretsSharing.Persistence.Repository.Interfaces;

/// <summary>
/// interface for abstraction over database
/// </summary>
public interface IApplicationDbContext
{
    /// <summary>
    /// List with all users from database
    /// </summary>
    DbSet<ApplicationUser> Users { get; set; }
    
    /// <summary>
    /// List with all files from database
    /// </summary>
    DbSet<FileDetails> Files { get; set; }
    
    /// <summary>
    /// method for saving changes to a table
    /// </summary>
    /// <param name="cancellationToken">Cancellation token with default value</param>
    /// <returns></returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}