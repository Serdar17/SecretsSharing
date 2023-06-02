using Domain.Models;
using Microsoft.EntityFrameworkCore;
using SecretsSharing.Persistence.Data;
using SecretsSharing.Persistence.Repository.Interfaces;

namespace SecretsSharing.Persistence.Repository.Implementation;

/// <summary>
/// a class that implements the IFileRepository interface
/// </summary>
public class FileRepository : IFileRepository
{
    private readonly ApplicationDbContext _dbContext;

    public FileRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// method for adding file details to the database
    /// </summary>
    /// <param name="fileDetails">Object with file details</param>
    /// <param name="cancellationToken">Cancellation token with default value</param>
    /// <returns>returns the unique identifier of the file</returns>
    public async Task<Guid> AddAsync(FileDetails fileDetails, CancellationToken cancellationToken = default)
    {
        await _dbContext.AddAsync(fileDetails, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return fileDetails.Id;
    }

    /// <summary>
    /// method for file search by unique id
    /// </summary>
    /// <param name="id">Unique file id</param>
    /// <param name="cancellationToken">Cancellation token with default value</param>
    /// <returns>Returns the found file details object</returns>
    public async Task<FileDetails?> GetFileByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Files
            .Where(x => x.Id.Equals(id))
            .FirstOrDefaultAsync(cancellationToken);
    }

    /// <summary>
    /// method for deleting file details from database
    /// </summary>
    /// <param name="fileDetails">object to delete</param>
    /// <param name="cancellationToken">Cancellation token with default value</param>
    public async Task DeleteAsync(FileDetails fileDetails, CancellationToken cancellationToken = default)
    {
        _dbContext.Files.Remove(fileDetails);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}